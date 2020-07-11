using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleJSON;

public class Table
{
    public string title;
    public string[] columnHeader;
    public Dictionary<string, string>[] rows;
}

public class FileManager : MonoBehaviour
{
    private const string TITLE = "Title";
    private const string COLUMNS = "ColumnHeaders";
    private const string DATA = "Data";

    public string fileName = "JsonChallenge.json";
    private string rawJson;
    private JSONNode parsedJson;
    private Table table;

    private string ReadFileFrom(string fullpath)
    {
        string fileData;
        StreamReader reader;
        reader = new StreamReader(fullpath);

        fileData = reader.ReadToEnd();

        reader.Close();

        return fileData;
    }

    public void LoadJson(string fileName)
    {
        object auxiliarObject;
        rawJson = ReadFileFrom(Path.Combine(Application.streamingAssetsPath, fileName));
        parsedJson = JSON.Parse(rawJson);

        if (parsedJson != null)
        {
            table = new Table();

            if (parsedJson.HasKey(TITLE))
            {
                table.title = parsedJson[TITLE];
            }

            if (parsedJson.HasKey(COLUMNS))
            {
                if (parsedJson[COLUMNS].Count > 0)
                {
                    table.columnHeader = new string[parsedJson[COLUMNS].Count];

                    for (int i = 0; i < parsedJson[COLUMNS].Count; i++)
                    {
                        table.columnHeader[i] = parsedJson[COLUMNS][i];
                    }
                }
            }

            if (parsedJson.HasKey(DATA))
            {
                if (parsedJson[DATA].Count > 0)
                {
                    table.rows = new Dictionary<string, string>[parsedJson[DATA].Count];
                    for (int i = 0; i < parsedJson[DATA].Count; i++)
                    {
                        table.rows[i] = new Dictionary<string, string>();
                        for (int j = 0; j < table.columnHeader.Length; j++)
                        {
                            table.rows[i].Add(table.columnHeader[j], parsedJson[DATA][i][table.columnHeader[j]]);
                        }
                    }
                }
            }
        }

        Debug.Log("" + table);
    }

    // Start is called before the first frame update
    void Start()
    {
        LoadJson(fileName);
    }
}
