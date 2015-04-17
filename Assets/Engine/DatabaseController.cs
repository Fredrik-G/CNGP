using System.Collections.Generic;
using System.Data;
using System.IO;
using Mono.Data.Sqlite;
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
        private static readonly string SQL_DB_LOCATION = "URI=file:M:/Desktop/CNGP.db";

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
        }

        /// <summary>
        /// String "enum" for the primary key in every table.
        /// </summary>
        private static class PrimaryKeys
        {
            public const string Accounts = "Email";
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
        private void InsertIntoCurrentTable(string[] columns)
        {
            _sqlQuery = "INSERT INTO " + SQL_TABLE_NAME
                        + " VALUES (";

            for(var column = 0; column < columns.Length; column++)
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
        /// 
        /// </summary>
        /// <param name="primaryKey"></param>
        /// <param name="column"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        private string GetColumnValue(string primaryKey, string column, string value)
        {
            var text = "Not Found";

            if (_dbConnection.State != ConnectionState.Open)
                _dbConnection.Open();

            _dbCommand.CommandText = "SELECT " + column + " FROM " + SQL_TABLE_NAME + " WHERE " + primaryKey + "='" +
                                     value + "'";
            _reader = _dbCommand.ExecuteReader();

            if (_reader.Read())
                text = _reader.GetString(0);
            else
                Debug.Log("Nothing to read...");

            _reader.Close();
            _dbConnection.Close();

            return text;
        }

        /// <summary>
        /// Basic execute command - open, create command, execute, close
        /// </summary>
        /// <param name="commandText">SQL Query to execute</param>
        private void ExecuteNonQuery(string commandText)
        {
            if (_dbConnection.State != ConnectionState.Open)
                _dbConnection.Open();

            _dbCommand.CommandText = commandText;
            _dbCommand.ExecuteNonQuery();
            _dbConnection.Close();
        }

        #endregion

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
            return GetColumnValue(PrimaryKeys.Accounts, "Salt", accountEmail);
        }

        /// <summary>
        /// Returns the hash value for a given account.
        /// </summary>
        /// <param name="accountEmail"></param>
        /// <returns></returns>
        public string GetHashForAccount(string accountEmail)
        {
            SQL_TABLE_NAME = Tables.Accounts;
            return GetColumnValue(PrimaryKeys.Accounts, "Hash", accountEmail);
        }

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