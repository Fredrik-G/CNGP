using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using Engine;
using Mono.Data.SqliteClient;
using UnityEngine;

public class LoginScreen : MonoBehaviour
{
    #region Data

    #region Menus Data

    /// <summary>
    /// All available menus.
    /// </summary>
    private enum Menus
    {
        Login,
        CreateAccount
    }

    /// <summary>
    /// The currently selected menu
    /// </summary>
    private Menus _currentMenu = Menus.Login;

    #endregion

    #region Account Data

    /// <summary>
    /// Account information
    /// </summary>

    private readonly User _user = new User(String.Empty, String.Empty, String.Empty);

    #endregion

    #region Hash Data

    private string _hash = string.Empty;
    private string _salt = string.Empty;
    private static readonly RNGCryptoServiceProvider RngCrypto = new RNGCryptoServiceProvider();

    #endregion

    #region Info Messages

    /// <summary>
    /// Warning message to be shown
    /// </summary>
    private string _warningMessage = String.Empty;

    /// <summary>
    /// Info message to be shown
    /// </summary>
    private string _infoMessage = String.Empty;

    #endregion

    #region GUI Stuff

    /// <summary>
    /// Rect used for menu/left/right-buttons.
    /// </summary>
    private Rect _menuRect;

    private Rect _leftButtonRect;
    private Rect _rightButtonRect;

    /// <summary>
    /// GUIStyles used for button & inputfields.
    /// Style values set via Unity Editor.
    /// </summary>
    public GUIStyle ButtonGuiStyle;
    public GUIStyle InputFieldGuiStyle;
    public GUIStyle BoxGuiStyle;

    #endregion

    /// <summary>
    /// Database controller that contains a connection to a database
    /// and SQL queries using SQLite.
    /// </summary>
    private DatabaseController _database;

    private const string UserNamePlayerPref = "NamePickUserName";

    #endregion

    #region MonoBehaviour Events

    public void Start()
    {
        _menuRect = new Rect(Screen.width / 2 - Screen.width / 5.8f, Screen.height / 2 - Screen.height / 7.5f, Screen.width / 3,
            Screen.height / 3);

        _leftButtonRect = new Rect(Screen.width / 2 - Screen.width / 7.5f, Screen.height / 2 + Screen.height / 10,
            Screen.width / 8,
            Screen.height / 20);

        _rightButtonRect = new Rect(Screen.width / 2 + Screen.width / 80, Screen.height / 2 + Screen.height / 10, Screen.width / 8,
            Screen.height / 20);

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
        //if (GUI.Button(new Rect(0, 0, 300, 100), "TEST:Lägg till statistics"))
        //{
        //    _database.UpdateStatisticsForAccount("asd", 10, 10, 10, 20, 10);
        //}

        //if (GUI.Button(new Rect(320, 0, 300, 100), "TEST:Läs statistics"))
        //{
        //    _database.GetStatisticsForAccount("asd");
        //}

        GUI.Box(_menuRect, "Login Screen", BoxGuiStyle);

        DisplayMessage();

        if (GUI.Button(_leftButtonRect, "Login", ButtonGuiStyle))
        {
            Authenticate();
        }
        if (GUI.Button(_rightButtonRect, "Create Account", ButtonGuiStyle))
        {
            ClearMessagesAndPassword();
            _currentMenu = Menus.CreateAccount;
        }

        if ((Event.current.type == EventType.KeyDown &&
             (Event.current.keyCode == KeyCode.KeypadEnter || Event.current.keyCode == KeyCode.Return)) &&
            !String.IsNullOrEmpty(_user.EMail) && !String.IsNullOrEmpty(_user.Password))
        {
            Authenticate();
        }
        else
        {
            _user.EMail = GUI.TextField(UIFormat.CreateCenteredRect(0), _user.EMail, InputFieldGuiStyle);
            if (GUI.GetNameOfFocusedControl().Equals(""))
                if (String.IsNullOrEmpty(_user.EMail))
                {
                    GUI.Label(UIFormat.CreateCenteredRect(0), "Account Email", InputFieldGuiStyle);
                }
        }

        _user.Password = GUI.PasswordField(UIFormat.CreateCenteredRect(-40), _user.Password, '*', InputFieldGuiStyle);
        if (String.IsNullOrEmpty(_user.Password))
        {
            GUI.Label(UIFormat.CreateCenteredRect(-40), "Account Password", InputFieldGuiStyle);
        }
    }

    /// <summary>
    /// Account creation menu.
    /// </summary>
    private void CreateAccountGUI()
    {
        GUI.Box(_menuRect, "Create New Account", BoxGuiStyle);

        DisplayMessage();

        if (GUI.Button(_leftButtonRect, "Create Account", ButtonGuiStyle))
        {
            CreateAccount();
        }
        if (GUI.Button(_rightButtonRect, "Back", ButtonGuiStyle))
        {
            ClearMessagesAndPassword();
            _currentMenu = Menus.Login;
        }

        if ((Event.current.type == EventType.KeyDown &&
             (Event.current.keyCode == KeyCode.KeypadEnter || Event.current.keyCode == KeyCode.Return)) &&
            !String.IsNullOrEmpty(_user.EMail) && !String.IsNullOrEmpty(_user.Password))
        {
            CreateAccount();
        }
        else
        {
            _user.EMail = GUI.TextField(UIFormat.CreateCenteredRect(0), _user.EMail, InputFieldGuiStyle);
            if (String.IsNullOrEmpty(_user.EMail))
            {
                GUI.Label(UIFormat.CreateCenteredRect(0), "Account Email", InputFieldGuiStyle);
            }
        }
        _user.Password = GUI.PasswordField(UIFormat.CreateCenteredRect(-40), _user.Password, '*', InputFieldGuiStyle);

        if (String.IsNullOrEmpty(_user.Password))
        {
            GUI.Label(UIFormat.CreateCenteredRect(-40), "Account Password", InputFieldGuiStyle);
        }
    }

    #endregion

    #region Account related Methods

    /// <summary>
    /// Attempts to create a new account with input from the textboxes.
    /// </summary>
    private void CreateAccount()
    {
        if (String.IsNullOrEmpty(_user.EMail) || String.IsNullOrEmpty(_user.Password))
        {
            _warningMessage = "Email or password can not be empty.";
            return;
        }

        _salt = CalculateSalt();
        _hash = CalculateHash(_salt, _user.Password);
        _user.RegisteredIp = GetUserPublicIp();
        _user.CurrentIp = _user.RegisteredIp;


        try
        {
            Debug.Log("Adding to database");
            _database.AddAccountToDatabase(_user.EMail, _salt, _hash, _user.RegisteredIp, _user.CurrentIp);
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
        var salt = _database.GetSaltForAccount(_user.EMail);
        var correctHash = _database.GetHashForAccount(_user.EMail);
        var inputHash = CalculateHash(salt, _user.Password);

        if (correctHash == inputHash)
        {
            Debug.Log("Correct login");
            _infoMessage = "Correct login!";

            _user.CurrentIp = GetUserPublicIp();
            _database.UpdateCurrentIpForAccount(_user.EMail, _user.CurrentIp);

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
        PlayerPrefs.SetString(UserNamePlayerPref, _user.EMail);
        Application.LoadLevel("MainScreen2");
    }

    /// <summary>
    /// Shows a centered text label with various information.
    /// </summary>
    private void DisplayMessage()
    {
        if (!String.IsNullOrEmpty(_warningMessage))
        {
            var guiStyle = UIFormat.FormatGuiStyle(TextAnchor.UpperLeft, UIFormat.FontSize.Small, Color.red);
            var rect = UIFormat.CreateCenteredRect(-Screen.height / 5);

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
    /// Clears warning and info messages and password.
    /// </summary>
    private void ClearMessagesAndPassword()
    {
        _warningMessage = String.Empty;
        _infoMessage = String.Empty;
        _user.Password = String.Empty;
    }

    /// <summary>
    /// Returns the public IP for the user.
    /// </summary>
    /// <returns></returns>
    private static string GetUserPublicIp()
    {
        const string url = "http://checkip.dyndns.org";
        //var webRequest = WebRequest.Create(url);
        //var webResponse = webRequest.GetResponse();
        //var streamReader = new System.IO.StreamReader(webResponse.GetResponseStream());

        //var response = streamReader.ReadToEnd().Trim();
        return "";

        //var ipHost = Dns.GetHostEntry(Dns.GetHostName());
        //return ipHost.AddressList[0].ToString();
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
