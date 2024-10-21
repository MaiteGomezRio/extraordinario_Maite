using System.Collections;
using System.Collections.Generic;
using System.Data;
using System;
using System.Data.Common;
using Mono.Data.Sqlite;
using System.Drawing;
using Unity.VisualScripting.Dependencies.Sqlite;
using UnityEngine;

public class DBManager : MonoBehaviour
{
    public static DBManager Instance { get; private set; }
    private string dbUri = "URI=file:mydb.sqlite";
    public string SQL_COUNT_ELEMENTS = "SELECT count(*) FROM Posiciones;";
    public string SQL_CREATE_POSICIONES = "CREATE TABLE IF NOT EXIST Posiciones (PlayerID INTEGER UNIQUE NOT NULL PRIMARY KEY AUTOINCREMENT, name TEXT NOT NULL, timestamp FLOAT NOT NULL, position FLOAT REFERENCES Coordenadas); ";
    public string SQL_CREATE_COORDENADAS = "CREATE TABLE IF NOT EXIST Coordenadas (ID INTEGER UNIQUE NOT NULL PRIMARY KEY AUTOINCREMENT, x FLOAT NOT NULL, y FLOAT NOT NULL, z FLOAT NOT NULL); "; 
    public string SQL_CREATE_NOMBRESUNIT= "CREATE TABLE IF NOT EXIST NombresUnidad (PlayerID INTEGER UNIQUE NOT NULL PRIMARY KEY AUTOINCREMENT, name TEXT NOT NULL, timestamp FLOAT NOT NULL); ";


    private IDbConnection dbConnection;

    // Start is called before the first frame update
    void Start()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }

        OpenDatabase();
        //InitializeDB();
    }

    private void OpenDatabase()
    {
        dbConnection = new SqliteConnection(dbUri);
        dbConnection.Open();
        IDbCommand dbCommand = dbConnection.CreateCommand();
        dbCommand.CommandText = "PRAGMA foreign_keys = ON";
        dbCommand.ExecuteNonQuery();
    }

    private void InitializeDB()
    {
        IDbCommand dbCmd = dbConnection.CreateCommand();
        dbCmd.CommandText = SQL_CREATE_POSICIONES;
        dbCmd.ExecuteReader();
    }

    private void newInitializeDB()
    {
        IDbCommand dbCmd = dbConnection.CreateCommand();
        dbCmd.CommandText = SQL_CREATE_NOMBRESUNIT;
        dbCmd.ExecuteReader();
    }

    public void SavePosition(string name, float timestamp, CharacterPosition position)
    {
        string command = "INSERT INTO Posiciones (name, timestamp, position) VALUES ('{name}','{timestamp}', '{position}');";
        IDbCommand dbCommand = dbConnection.CreateCommand();
        dbCommand.CommandText = command;
        dbCommand.ExecuteNonQuery();
    }

    private void OnDestroy()
    {
        dbConnection.Close();
    }
}
