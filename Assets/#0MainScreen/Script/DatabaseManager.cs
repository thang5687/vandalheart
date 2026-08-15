using Mono.Data.Sqlite;
using System.Data;
using UnityEngine;

public class DatabaseManager : MonoBehaviour
{
    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        /*string[] data = new string[4];
        data[0] = "100";
        data[1] = "100";
        data[2] = "100";
        data[3] = "100";
        AddData(tableName[0], data);
        DeleteData(tableName[0], FetchAllFieldName(tableName[0])[0], "100");*/
        //UpdateData(tableName[0], FetchAllFieldName(tableName[0])[0], "6", FetchAllFieldName(tableName[0])[0], "7");
    }
    #region attribute
    public string conn = "URI=file:" + Application.streamingAssetsPath + "/UserTestDatabase.db";
    public string[] tableName;
    #endregion
    #region components
    public static DatabaseManager instance;
    #endregion
    #region functions
    public string[] FetchAllFieldName(string tableN)
    {
        string[] fields = new string[0];
        IDbConnection dbconn;
        dbconn = (IDbConnection)new SqliteConnection(conn);
        dbconn.Open(); //Open connection to the database.
        IDbCommand dbcmd = dbconn.CreateCommand();
        string sqlQuery = "SELECT group_concat(name, ',') FROM pragma_table_info('" + tableN + "')";
        //Debug.Log(sqlQuery);
        dbcmd.CommandText = sqlQuery;
        IDataReader reader = dbcmd.ExecuteReader();
        while (reader.Read())
        {
            fields= reader.GetValue(0).ToString().Split(",");
        }
        reader.Close();
        reader = null;
        dbcmd.Dispose();
        dbcmd = null;
        dbconn.Close();
        dbconn = null;
        /*foreach (string field in fields)
        {
            Debug.Log(field);
        }*/
        return fields;
    }
    public void AddData(string tableN, string[] data)
    {
        string ValuesStr = "";
        foreach (string value in data)
        {
            ValuesStr += "'"+ value + "',";
        }
        ValuesStr=ValuesStr.Substring(0,ValuesStr.Length-1);
        Debug.Log(ValuesStr);
        IDbConnection dbconn;
        dbconn = (IDbConnection)new SqliteConnection(conn);
        dbconn.Open(); //Open connection to the database.
        IDbCommand dbcmd = dbconn.CreateCommand();
        string sqlQuery = "INSERT INTO "+ tableN +" VALUES (" + ValuesStr + ")";
        dbcmd.CommandText = sqlQuery;
        IDataReader reader = dbcmd.ExecuteReader();

        Debug.Log(sqlQuery);

        reader.Close();
        reader = null;
        dbcmd.Dispose();
        dbcmd = null;
        dbconn.Close();
        dbconn = null;
    }
    public void AddAField(string tableN, string fieldName, string data)
    {
        IDbConnection dbconn;
        dbconn = (IDbConnection)new SqliteConnection(conn);
        dbconn.Open(); //Open connection to the database.
        IDbCommand dbcmd = dbconn.CreateCommand();
        string sqlQuery = "INSERT INTO " + tableN + " ("+ fieldName + ") VALUES ('" + data + "')";
        dbcmd.CommandText = sqlQuery;
        IDataReader reader = dbcmd.ExecuteReader();

        Debug.Log(sqlQuery);

        reader.Close();
        reader = null;
        dbcmd.Dispose();
        dbcmd = null;
        dbconn.Close();
        dbconn = null;
    }
    public void DeleteData(string tableN, string fieldName, string Condition)
    {
        IDbConnection dbconn;
        dbconn = (IDbConnection)new SqliteConnection(conn);
        dbconn.Open(); //Open connection to the database.
        IDbCommand dbcmd = dbconn.CreateCommand();
        string sqlQuery = "DELETE FROM "+ tableN +" WHERE "+ fieldName+"='"+ Condition+"'";
        dbcmd.CommandText = sqlQuery;
        IDataReader reader = dbcmd.ExecuteReader();

        Debug.Log(sqlQuery);

        reader.Close();
        reader = null;
        dbcmd.Dispose();
        dbcmd = null;
        dbconn.Close();
        dbconn = null;
    }
    public void UpdateData(string tableN, string fieldLookup, string Condition, string fieldUpdate, string valueUpdate)
    {
        IDbConnection dbconn;
        dbconn = (IDbConnection)new SqliteConnection(conn);
        dbconn.Open(); //Open connection to the database.
        IDbCommand dbcmd = dbconn.CreateCommand();
        string sqlQuery = "UPDATE "+ tableN + " SET "+ fieldUpdate + " = '" + valueUpdate + "' WHERE "+fieldLookup+" ='" + Condition+"'";
        dbcmd.CommandText = sqlQuery;
        IDataReader reader = dbcmd.ExecuteReader();

        //Debug.Log(sqlQuery);

        reader.Close();
        reader = null;
        dbcmd.Dispose();
        dbcmd = null;
        dbconn.Close();
    }
    public bool CheckIfExit(string tableN, string fieldName, string Condition)
    {
        bool result=false;
        IDbConnection dbconn;
        dbconn = (IDbConnection)new SqliteConnection(conn);
        dbconn.Open(); //Open connection to the database.
        IDbCommand dbcmd = dbconn.CreateCommand();
        string sqlQuery = "SELECT * FROM " + tableN + " WHERE " + fieldName + "='" + Condition + "'";
        dbcmd.CommandText = sqlQuery;
        IDataReader reader = dbcmd.ExecuteReader();
        if (reader.GetValue(0).ToString() != "") result = true;
        Debug.Log(sqlQuery);

        reader.Close();
        reader = null;
        dbcmd.Dispose();
        dbcmd = null;
        dbconn.Close();
        dbconn = null;
        //Debug.Log(result);
        return result;
    }
    public string GetData(string tableN, string fieldName, string Condition)
    {
        string result = "";
        IDbConnection dbconn;
        dbconn = (IDbConnection)new SqliteConnection(conn);
        dbconn.Open(); //Open connection to the database.
        IDbCommand dbcmd = dbconn.CreateCommand();
        string sqlQuery = "SELECT * FROM " + tableN + " WHERE " + fieldName + "='" + Condition + "'";
        dbcmd.CommandText = sqlQuery;
        IDataReader reader = dbcmd.ExecuteReader();
        //Debug.Log(reader.FieldCount);
        while (reader.Read())
        {
            for (int i = 0; i < reader.FieldCount; i++)
            {
                result += reader.GetValue(i).ToString()+"#";
            }
            result = result.Substring(0, result.Length - 1);
        }
        reader.Close();
        reader = null;
        dbcmd.Dispose();
        dbcmd = null;
        dbconn.Close();
        dbconn = null;
        Debug.Log(result);
        return result;
    }
    #endregion
}
