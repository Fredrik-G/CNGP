using System;
using System.Data;
using System.Text;
using Mono.Data.SqliteClient;
using UnityEngine;

namespace Engine
{
    /// <summary>
    /// Controlls the connection to a database and handles SQL queries.
    /// There should not be any queries outside of this class.
    /// </summary>
    public class DatabaseController
    {
        #region Data

        /// <summary>
        /// Table name and DB actual file location
        /// </summary>
        private const string SQL_DB_NAME = "CNGP";

        /// <summary>
        /// Current table name
        /// </summary>
        private string SQL_TABLE_NAME;

        /// <summary>
        /// Database location
        /// </summary>
        //private static readonly string SQL_DB_LOCATION = "URI=file:"
        // + Application.dataPath + Path.DirectorySeparatorChar
        //  + SQL_DB_NAME + ".db";

        //Använd ej denna, använd den ovanför.
        private const string SQL_DB_LOCATION = "URI=file:C:/Users/Lukas/Desktop/photon/Assets/Database/cngp.db";

        /// <summary>
        /// DB objects
        /// </summary>
        private IDbConnection _dbConnection = null;
        private IDbCommand _dbCommand = null;
        private IDataReader _reader = null;
        private string _sqlQuery;

        public bool DebugMode = true;
        #endregion

        #region Enums

        /// <summary>
        /// String "enum" for all available tables.
        /// </summary>
        private static class Tables
        {
            public const string Accounts = AccountsTable.TableName;
            public const string Statistics = StatisticsTable.TableName;
        }

        /// <summary>
        /// Static class defining the accounts table.
        /// </summary>
        private static class AccountsTable
        {
            public const string TableName = "Accounts";
            public const string PrimaryKey = Columns.Email;

            public static class Columns
            {
                public const string Email = "Email";
                public const string Hash = "Hash";
                public const string Salt = "Salt";
                public const string RegisteredIp = "RegisteredIP";
                public const string CurrentIp = "CurrentIP";
            }
        }

        /// <summary>
        /// Static class defining the statistics table.
        /// </summary>
        private static class StatisticsTable
        {
            public const string TableName = "Statistics";
            public const string PrimaryKey = Columns.Email;

            public static class Columns
            {
                public const string Email = "Email";
                public const string Wins = "Wins";
                public const string GamesPlayed = "GamesPlayed";
                public const string Rating = "Rating";
                public const string Kills = "Kills";
                public const string Deaths = "Deaths";
            }
        }
        #endregion

        /// <summary>
        /// Initilizes the database connection.
        /// </summary>
        public void Initilize()
        {
            DebugText("Opening connection to database " + SQL_DB_NAME);
            _dbConnection = new SqliteConnection(SQL_DB_LOCATION);
            _dbCommand = _dbConnection.CreateCommand();
        }

        /// <summary>
        /// Gets the primary key for the given table.
        /// </summary>
        /// <returns>The primary key</returns>
        private string GetPrimaryKeyForCurrentTable()
        {
            if (String.Equals(SQL_TABLE_NAME, AccountsTable.TableName))
            {
                return AccountsTable.PrimaryKey;
            }
            if (String.Equals(SQL_TABLE_NAME, StatisticsTable.TableName))
            {
                return StatisticsTable.PrimaryKey;
            }

            throw new Exception("Primary key was not found for table " + SQL_TABLE_NAME);
        }

        #region SQL Queries

        /// <summary>
        /// Inserts given columns to the current table.
        /// </summary>
        /// <param name="columns">The columns to be inserted</param>
        /// <param name="updateIfExists">Tells the database to update if a value already exists</param>
        private void InsertIntoCurrentTable(string[] columns, bool updateIfExists = false)
        {
            if (!updateIfExists)
            {
                _sqlQuery = "INSERT INTO " + SQL_TABLE_NAME
                            + " VALUES (";
            }
            else
            {
                _sqlQuery = "INSERT or REPLACE INTO " + SQL_TABLE_NAME
                            + " VALUES (";
            }

            for (var column = 0; column < columns.Length; column++)
            {
                if (column + 1 == columns.Length)
                {
                    _sqlQuery += "'" + columns[column];
                }
                else
                {
                    _sqlQuery += "'" + columns[column] + "',";
                }
            }
            _sqlQuery += "');";

            DebugText(_sqlQuery);
            ExecuteNonQuery(_sqlQuery);
        }

        /// <summary>
        /// Inserts given columns to the current table.
        /// </summary>
        /// <param name="primaryKey">The value of the primary key to update to</param>
        /// <param name="column">The column to insert into</param>
        /// <param name="columnValue">The value to be inserted</param>
        private void UpdateColumnInCurrentTable(string primaryKey, string column, string columnValue)
        {
            _sqlQuery = "UPDATE " + SQL_TABLE_NAME
                        + " SET "
                        + column + "='" + columnValue
                        + "' WHERE "
                        + GetPrimaryKeyForCurrentTable() + "='" + primaryKey + "'";

            DebugText(_sqlQuery);
            ExecuteNonQuery(_sqlQuery);
        }

        /// <summary>
        /// Returns the column value for a given column based on primary key.
        /// </summary>
        /// <param name="primaryKey">The primary key to read from</param>
        /// <param name="value">The value of the primary key</param>
        /// <param name="column">The column to read from</param>
        /// <returns></returns>
        private string GetColumnValue(string primaryKey, string value, string column)
        {
            if (String.IsNullOrEmpty(SQL_TABLE_NAME))
            {
                throw new Exception("Table name is not set!");
            }

            var text = "Not Found";

            if (_dbConnection.State != System.Data.ConnectionState.Open)
            {
                _dbConnection.Open();
            }

            _dbCommand.CommandText = "SELECT " + column + " FROM " + SQL_TABLE_NAME + " WHERE " + primaryKey + "='" +
                                     value + "'";
            _reader = _dbCommand.ExecuteReader();

            if (_reader.Read())
            {
                text = _reader.GetString(0);
            }
            else
            {
                DebugText("Primay key " + primaryKey + " in table " + SQL_TABLE_NAME + " was not found");
            }

            _reader.Close();
            _dbConnection.Close();

            return text;
        }

        /// <summary>
        /// Returns all column values for a given column based on primary key.
        /// </summary>
        /// <param name="primaryKey">The primary key to read from</param>
        /// <param name="value">The value of the primary key</param>
        /// <returns></returns>
        private string GetAllColumnsValue(string primaryKey, string value, int numberOfColumns)
        {
            if (String.IsNullOrEmpty(SQL_TABLE_NAME))
            {
                throw new Exception("Table name is not set!");
            }

            if (_dbConnection.State != System.Data.ConnectionState.Open)
            {
                _dbConnection.Open();
            }

            _dbCommand.CommandText = "SELECT * FROM " + SQL_TABLE_NAME + " WHERE " + primaryKey + "='" + value + "'";

            DebugText(_dbCommand.CommandText);

            _reader = _dbCommand.ExecuteReader();

            var text = new StringBuilder();
            while (_reader.Read())
            {
                for (var i = 0; i < numberOfColumns; i++)
                {
                    text.Append(_reader.GetString(i)).Append(" ");
                }
                text.AppendLine();

                DebugText(text.ToString());
            }

            _reader.Close();
            _dbConnection.Close();

            return "a";
        }

        /// <summary>
        /// Basic execute command - open, create command, execute, close
        /// </summary>
        /// <param name="commandText">SQL Query to execute</param>
        private void ExecuteNonQuery(string commandText)
        {
            if (String.IsNullOrEmpty(SQL_TABLE_NAME))
            {
                throw new Exception("Table name is not set!");
            }

            if (_dbConnection.State != System.Data.ConnectionState.Open)
            {
                _dbConnection.Open();
            }

            _dbCommand.CommandText = commandText;
            _dbCommand.ExecuteNonQuery();
            _dbConnection.Close();
        }

        #endregion

        #region Account Methods

        /// <summary>
        /// Add an account with given values to the database.
        /// </summary>
        /// <param name="email"></param>
        /// <param name="salt"></param>
        /// <param name="hash"></param>
        /// <param name="registeredIp"></param>
        /// <param name="currentIp"></param>
        public void AddAccountToDatabase(string email, string salt, string hash, string registeredIp, string currentIp)
        {
            //Should check for sql injection here.

            string[] accountInfo = { email, salt, hash, registeredIp,  currentIp};

            SQL_TABLE_NAME = Tables.Accounts;

            InsertIntoCurrentTable(accountInfo);
        }

        /// <summary>
        /// Returns the salt value for a given account.
        /// </summary>
        /// <param name="accountEmail"></param>
        /// <returns></returns>
        public string GetSaltForAccount(string accountEmail)
        {
            SQL_TABLE_NAME = Tables.Accounts;
            return GetColumnValue(AccountsTable.PrimaryKey, accountEmail, "Salt");
        }

        /// <summary>
        /// Returns the hash value for a given account.
        /// </summary>
        /// <param name="accountEmail"></param>
        /// <returns></returns>
        public string GetHashForAccount(string accountEmail)
        {
            SQL_TABLE_NAME = Tables.Accounts;
            return GetColumnValue(AccountsTable.PrimaryKey, accountEmail, "Hash");
        }

        /// <summary>
        /// Updates the current IP-adress for a given account.
        /// </summary>
        /// <param name="email">The account email</param>
        /// <param name="currentIp">The current IP to update to</param>
        public void UpdateCurrentIpForAccount(string email, string currentIp)
        {
            SQL_TABLE_NAME = Tables.Accounts;
            DebugText(SQL_TABLE_NAME);
            UpdateColumnInCurrentTable(email, AccountsTable.Columns.CurrentIp, currentIp);
        }

        #endregion

        #region Statistics Methods

        public void UpdateStatisticsForAccount(string email, int wins, int gamesPlayed, int rating,
            int kills, int deaths)
        {
            string[] statistics =
            {
                email, wins.ToString(), gamesPlayed.ToString(), rating.ToString(), kills.ToString(),
                deaths.ToString()
            };

            SQL_TABLE_NAME = Tables.Statistics;

            InsertIntoCurrentTable(statistics, true);
        }

        public void GetStatisticsForAccount(string email)
        {
            SQL_TABLE_NAME = Tables.Statistics;

            const int numberOfColumns = 6;

            GetAllColumnsValue(StatisticsTable.PrimaryKey, email, numberOfColumns);
        }

        #endregion

        /// <summary>
        /// Logs debug text if DebugMode is true.
        /// </summary>
        /// <param name="text"></param>
        private void DebugText(string text)
        {
            if (DebugMode)
                Debug.Log(text);
        }
    }
}