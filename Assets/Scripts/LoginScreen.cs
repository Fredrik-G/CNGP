using System;
using UnityEngine;
using System.Collections;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using Engine;
using Mono.Data.Sqlite;

public class LoginScreen : MonoBehaviour
{
    #region Data

    /// <summary>
    /// All available menus.
    /// </summary>
    private enum Menus
    {
        Login,
        CreateAccount
    }

    /// <summary>
    /// Account information
    /// </summary>
    private string _accountEmail = String.Empty;
    private string _accountPassword = String.Empty;
    private string _registeredIp = String.Empty;

    private string _hash = string.Empty;
    private string _salt = string.Empty;
    private static RNGCryptoServiceProvider rngCrypto = new RNGCryptoServiceProvider();

    /// <summary>
    /// The currently selected menu
    /// </summary>
    private Menus _currentMenu = Menus.Login;

    /// <summary>
    /// Info message to be shown
    /// </summary>
    private string _message = String.Empty;

    /// <summary>
    /// Rect used for menu/left/right-buttons.
    /// </summary>
    private Rect _menuRect;
    private Rect _leftButtonRect;
    private Rect _rightButtonRect;

    /// <summary>
    /// Database controller that contains a connection to a database
    /// and SQL queries using SQLite.
    /// </summary>
    private DatabaseController _database;

    #endregion

    #region MonoBehaviour Events

    public void Start()
    {
        _menuRect = new Rect(Screen.width/2 - Screen.width/5.8f, Screen.height/2 - Screen.height/7.5f, Screen.width/3,
            Screen.height/3);

        _leftButtonRect = new Rect(Screen.width/2 - Screen.width/7.5f, Screen.height/2 + Screen.height/10,
            Screen.width/8,
            Screen.height/20);

        _rightButtonRect = new Rect(Screen.width/2 + Screen.width/80, Screen.height/2 + Screen.height/10, Screen.width/8,
            Screen.height/20);

        _database = new DatabaseController();
        _database.Initilize();
    }

    #endregion

    #region GUI Menus

    public void OnGUI()
    {
        switch (_currentMenu)
        {
            case Menus.Login:
                LoginGUI();
                break;
            case Menus.CreateAccount:
                CreateAccountGUI();
                break;
        }
    }

    /// <summary>
    /// Login menu.
    /// </summary>
    private void LoginGUI()
    {
        GUI.Box(_menuRect, "Login Screen");

        ShowInfoText();

        if (GUI.Button(_leftButtonRect, "Login"))
        {
            Authenticate();
        }
        if (GUI.Button(_rightButtonRect, "Create Account"))
        {
            _message = String.Empty;
            _currentMenu = Menus.CreateAccount;
        }

        if (String.IsNullOrEmpty(_accountEmail))
        {
            GUI.Label(UIFormat.CreateCenteredRect(0), "Account Email");
        }
        _accountEmail = GUI.TextField(UIFormat.CreateCenteredRect(0), _accountEmail);

        if (String.IsNullOrEmpty(_accountPassword))
        {
            GUI.Label(UIFormat.CreateCenteredRect(-40), "Account Password");
        }
        _accountPassword = GUI.PasswordField(UIFormat.CreateCenteredRect(-40), _accountPassword, '*');

    }

    /// <summary>
    /// Account creation menu.
    /// </summary>
    private void CreateAccountGUI()
    {
        // _accountPassword = String.Empty;

        GUI.Box(_menuRect, "Create New Account");

        ShowInfoText();

        if (GUI.Button(_leftButtonRect, "Create Account"))
        {
            CreateAccount();
        }
        if (GUI.Button(_rightButtonRect, "Back"))
        {
            _message = String.Empty;
            _currentMenu = Menus.Login;
        }

        if (String.IsNullOrEmpty(_accountEmail))
        {
            GUI.Label(UIFormat.CreateCenteredRect(0), "Account Email");
        }
        _accountEmail = GUI.TextField(UIFormat.CreateCenteredRect(0), _accountEmail);

        if (String.IsNullOrEmpty(_accountPassword))
        {
            GUI.Label(UIFormat.CreateCenteredRect(-40), "Account Password");
        }
        _accountPassword = GUI.PasswordField(UIFormat.CreateCenteredRect(-40), _accountPassword, '*');
    }

    #endregion

    #region Account related Methods

    /// <summary>
    /// Attempts to create a new account with input from the textboxes.
    /// </summary>
    private void CreateAccount()
    {
        if (String.IsNullOrEmpty(_accountEmail) || String.IsNullOrEmpty(_accountPassword))
        {
            return;
        }

        _salt = CalculateSalt();
        _hash = CalculateHash(_salt, _accountPassword);
        _registeredIp = GetUserPublicIp();

        try
        {
            _database.AddAccountToDatabase(_accountEmail, _salt, _hash, _registeredIp);
            _message = String.Empty;
        }
        catch (SqliteException)
        {
            _message = "An account with that email already exists.";
        }
    }

    /// <summary>
    /// Attempts to authenticate the user.
    /// Gets the salt and correct hash for the inputed email
    /// and calculates a new hash using the input password and the returned salt.
    /// Then compares the correct hash with the newly calculates hash
    /// and logs in if they are equal (input password is correct).
    /// </summary>
    private void Authenticate()
    {
        var salt = _database.GetSaltForAccount(_accountEmail);
        var correctHash = _database.GetHashForAccount(_accountEmail);
        var inputHash = CalculateHash(salt, _accountPassword);

        if (correctHash == inputHash)
        {
            Debug.Log("Correct login");
            _message = String.Empty;
        }
        else
        {
            Debug.Log("Incorrect login");
            _message = "Invalid email or password.";
        }
    }

    #endregion

    /// <summary>
    /// Shows a centered text label with various information.
    /// </summary>
    private void ShowInfoText()
    {
        var guiStyle = UIFormat.FormatGuiStyle(TextAnchor.UpperLeft, UIFormat.FontSize.Small, Color.red);
        var rect = UIFormat.CreateCenteredRect(-Screen.height/5);

        GUI.Label(rect, _message, guiStyle);
    }

    /// <summary>
    /// Returns the public IP for the user.
    /// </summary>
    /// <returns></returns>
    private string GetUserPublicIp()
    {
        var ipHost = Dns.GetHostEntry(Dns.GetHostName());
        return ipHost.AddressList[0].ToString();
    }

    #region Hash & Salt

    /// <summary>
    /// Calculates a pseudo-random salt used for authentication.
    /// </summary>
    private string CalculateSalt()
    {
        const int SALT_SIZE = 5;

        var saltBytes = new byte[SALT_SIZE];
        rngCrypto.GetNonZeroBytes(saltBytes);

        return saltBytes.Aggregate("", (current, x) => current + String.Format("{0:x2}", x));
    }

    /// <summary>
    /// Calculates a SHA256 hash from password + salt. 
    /// CalculateSalt has to be run before this function.
    /// </summary>
    private string CalculateHash(string salt, string password)
    {
        var stringToHash = password + salt;
        var crypt = new SHA256Managed();
        var tempString = String.Empty;
        var crypto = crypt.ComputeHash(Encoding.UTF8.GetBytes(stringToHash), 0,
            Encoding.UTF8.GetByteCount(stringToHash));

        return crypto.Aggregate(tempString, (current, bit) => current + bit.ToString("x2"));
        // Same as foreach(var bit in crypto) tempstring += bit.ToString("x2));
    }

    #endregion

}
