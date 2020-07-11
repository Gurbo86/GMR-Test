using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleJSON;

public class FileManager : MonoBehaviour
{
    private static FileManager _instance;

    public static FileManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<FileManager>();

                if (_instance == null)
                {
                    GameObject go = new GameObject();
                    go.name = typeof(FileManager).Name;
                    _instance = go.AddComponent<FileManager>();
                    DontDestroyOnLoad(go);
                }
            }
            return _instance;
        }
    }

    public string fileName = "JsonChallenge.json";

    private string rawJson;
    private JSONNode parsedJson;

    private string ReadFileFrom(string fullpath)
    {
        string fileData;
        StreamReader reader;
        reader = new StreamReader(fullpath);

        fileData = reader.ReadToEnd();

        reader.Close();

        return fileData;
    }
    
    public JSONNode GetJson()
    {
        rawJson = ReadFileFrom(Path.Combine(Application.streamingAssetsPath, fileName));
        return JSON.Parse(rawJson);
    }
}
