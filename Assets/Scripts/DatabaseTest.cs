using System.Data;
using Mono.Data.Sqlite;
using UnityEngine;

public class DatabaseTest : MonoBehaviour
{
    public void Start()
    {
        //var conn = "URI=file:" + Application.dataPath + "/CNGP.db"; //Path to database.
        var conn = "URI=file:M:/Desktop/CNGP.db";
        IDbConnection dbconn;
        dbconn = (IDbConnection)new SqliteConnection(conn);
        dbconn.Open(); //Open connection to the database.
        var dbcmd = dbconn.CreateCommand();

        var sqlQuery = "insert into tableName values (6)";
        dbcmd.CommandText = sqlQuery;
        dbcmd.ExecuteNonQuery();
    }

}