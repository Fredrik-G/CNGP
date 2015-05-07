using System;
using UnityEngine;
using System.Collections;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using Engine;
using Mono.Data.SqliteClient;

public class LoginScreen : MonoBehaviour
{
    #region Data

    private const string UserNamePlayerPref = "NamePickUserName";

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

    /// !!!Borde använda klassen User!!!
    private string _accountEmail = String.Empty;
    private string _accountPassword = String.Empty;
    private string _registeredIp = String.Empty;
    private string _currentIp = String.Empty;

    private string _hash = string.Empty;
    private string _salt = string.Empty;
    private static readonly RNGCryptoServiceProvider RngCrypto = new RNGCryptoServiceProvider();

    /// <summary>
    /// The currently selected menu
    /// </summary>
    private Menus _currentMenu = Menus.Login;

    /// <summary>
    /// Warning message to be shown
    /// </summary>
    private string _warningMessage = String.Empty;

    /// <summary>
    /// Info message to be shown
    /// </summary>
    private string _infoMessage = String.Empty;

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

    #endregion

    #region GUI Menus

    /// <summary>
    /// Login menu.
    /// </summary>
    private void LoginGUI()
    {
        if (GUI.Button(new Rect(0, 0, 300, 100), "TEST:Lägg till statistics"))
        {
            _database.UpdateStatisticsForAccount("asd", 10, 10, 10, 20, 10);
        }

        if (GUI.Button(new Rect(320, 0, 300, 100), "TEST:Läs statistics"))
        {
            _database.GetStatisticsForAccount("asd");
        }

        GUI.Box(_menuRect, "Login Screen");

        DisplayMessage();

        if (GUI.Button(_leftButtonRect, "Login"))
        {
            Authenticate();
        }
        if (GUI.Button(_rightButtonRect, "Create Account"))
        {
            ClearMessagesAndPassword();
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
    /// Clears warning and info messages and password.
    /// </summary>
    private void ClearMessagesAndPassword()
    {
        _warningMessage = String.Empty;
        _infoMessage = String.Empty;
        _accountPassword = String.Empty;
    }

    /// <summary>
    /// Account creation menu.
    /// </summary>
    private void CreateAccountGUI()
    {
        GUI.Box(_menuRect, "Create New Account");

        DisplayMessage();

        if (GUI.Button(_leftButtonRect, "Create Account"))
        {
            CreateAccount();
        }
        if (GUI.Button(_rightButtonRect, "Back"))
        {
            ClearMessagesAndPassword();
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
            _warningMessage = "Email or password can not be empty.";
            return;
        }

        _salt = CalculateSalt();
        _hash = CalculateHash(_salt, _accountPassword);
        _registeredIp = GetUserPublicIp();
        _currentIp = _registeredIp;

        try
        {
            Debug.Log("Adding to database");
            _database.AddAccountToDatabase(_accountEmail, _salt, _hash, _registeredIp, _currentIp);
            _warningMessage = String.Empty;
            _infoMessage = "Account was successfully created!";
        }
        catch (SqliteExecutionException)
        {
            _warningMessage = "An account with that email already exists.";
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
            _infoMessage = "Correct login!";

            _currentIp = GetUserPublicIp();
            _database.UpdateCurrentIpForAccount(_accountEmail, _currentIp);

            LoadLobby();
        }
        else
        {
            Debug.Log("Incorrect login");
            _warningMessage = "Invalid email or password.";
        }
    }

    #endregion

    /// <summary>
    /// Loads lobby scene.
    /// </summary>
    private void LoadLobby()
    {
        PlayerPrefs.SetString(UserNamePlayerPref, _accountEmail);
        Application.LoadLevel("MainScreen");
    }


    /// <summary>
    /// Shows a centered text label with various information.
    /// </summary>
    private void DisplayMessage()
    {
        if (!String.IsNullOrEmpty(_warningMessage))
        {
            var guiStyle = UIFormat.FormatGuiStyle(TextAnchor.UpperLeft, UIFormat.FontSize.Small, Color.red);
            var rect = UIFormat.CreateCenteredRect(-Screen.height/5);

            GUI.Label(rect, _warningMessage, guiStyle);
        }
        else
        {
            var guiStyle = UIFormat.FormatGuiStyle(TextAnchor.UpperLeft, UIFormat.FontSize.Small, Color.green);
            var rect = UIFormat.CreateCenteredRect(-Screen.height / 5);

            GUI.Label(rect, _infoMessage, guiStyle);
        }
    }

    /// <summary>
    /// Returns the public IP for the user.
    /// </summary>
    /// <returns></returns>
    private static string GetUserPublicIp()
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
        RngCrypto.GetNonZeroBytes(saltBytes);

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
