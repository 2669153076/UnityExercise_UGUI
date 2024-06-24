using LitJson;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public enum E_JsonType
{
    JsonUtility,
    LitJson
}

public class JsonFileMgr
{
    private static JsonFileMgr instance=new JsonFileMgr();
    public static JsonFileMgr Instance=>instance;

    private JsonFileMgr() { }

    public void SaveData(object data,string fileName,E_JsonType e_JsonType)
    {
        string path = Application.persistentDataPath + "/" + fileName + ".json";
        string jsonStr = "";
        switch (e_JsonType)
        {
            case E_JsonType.JsonUtility:
                jsonStr = JsonUtility.ToJson(data);
                break;
            case E_JsonType.LitJson:
                jsonStr = JsonMapper.ToJson(data);
                break;
        }

        File.WriteAllText(path, jsonStr);
    }
    
    public T LoadData<T>(string fileName,E_JsonType e_JsonType = E_JsonType.LitJson) where T:new()
    {
        string path = Application.streamingAssetsPath + "/" + fileName + ".json";
        if (!File.Exists(path))
        {
            path = Application.persistentDataPath + "/" + fileName + ".json";

            if (!File.Exists(path))
            {
                return new T();
            }
        }

        string jsonStr = File.ReadAllText(path);
        T data = default(T);

        switch (e_JsonType)
        {
            case E_JsonType.JsonUtility:
                data = JsonUtility.FromJson<T>(jsonStr);
                break;
            case E_JsonType.LitJson:
                data = JsonMapper.ToObject<T>(jsonStr);
                break;
        }
        return data;

    }
    
}
