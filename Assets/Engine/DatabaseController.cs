using System;
using UnityEngine;
using System.Data;
using Mono.Data.SqliteClient;
using System.IO;
using System.Text;

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
        private const string SQL_DB_LOCATION = "URI=file:M:/Desktop/CNGP.db";

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
            public const string Accounts = "Accounts";
            public const string Statistics = "Statistics";
        }

        /// <summary>
        /// String "enum" for the primary key in every table.
        /// </summary>
        private static class PrimaryKeys
        {
            /// <summary>
            /// Primary key for table Accounts.
            /// </summary>
            public const string Accounts = "Email";

            /// <summary>
            /// Primary key for table Statistics.
            /// </summary>
            public const string Statistics = "Email";
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

            if (_dbConnection.State != ConnectionState.Open)
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

            if (_dbConnection.State != ConnectionState.Open)
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

            if (_dbConnection.State != ConnectionState.Open)
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
        /// 
        /// </summary>
        /// <param name="email"></param>
        /// <param name="salt"></param>
        /// <param name="hash"></param>
        /// <param name="registeredIp"></param>
        public void AddAccountToDatabase(string email, string salt, string hash, string registeredIp)
        {
            //Should check for sql injection here.

            string[] accountInfo = { email, salt, hash, registeredIp };

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
            return GetColumnValue(PrimaryKeys.Accounts, accountEmail, "Salt");
        }

        /// <summary>
        /// Returns the hash value for a given account.
        /// </summary>
        /// <param name="accountEmail"></param>
        /// <returns></returns>
        public string GetHashForAccount(string accountEmail)
        {
            SQL_TABLE_NAME = Tables.Accounts;
            return GetColumnValue(PrimaryKeys.Accounts, accountEmail, "Hash");
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

            var numberOfColumns = 6;

            GetAllColumnsValue(PrimaryKeys.Statistics, email, numberOfColumns);
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