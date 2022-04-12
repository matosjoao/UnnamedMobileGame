using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

public static class JsonSaving 
{
    public static void SaveData<T> (List<T> playerData, string filename)
    {
        string content = JsonHelper.ToJson<T>(playerData.ToArray());
        WriteFile(GetPaths(filename), content);
    }

    private static string GetPaths(string filename)
    {
        //string path = Application.dataPath + Path.AltDirectorySeparatorChar + filename;
        //TODO:: Change to persistentPath in production
        string persistentPath = Application.persistentDataPath + Path.AltDirectorySeparatorChar + filename;
        return persistentPath;
    }

    
    public static  List<T> LoadData<T>(string filename)
    {
        string content = ReadFile(GetPaths(filename));

        if(string.IsNullOrEmpty(content) || content == "{}")
        {
            return new List<T>();
        }
        List<T> result = JsonHelper.FromJson<T>(content).ToList();

        return result;
    }

    private static void WriteFile(string path, string content)
    {
        FileStream fileStream = new FileStream(path, FileMode.Create);
        using(StreamWriter writer = new StreamWriter(fileStream))
        {
            writer.Write(content);
        }
    }

    private static string ReadFile(string path)
    {
        if (!File.Exists(path))
            return "";

        using (StreamReader reader = new StreamReader(path))
        {
            string content = reader.ReadToEnd();
            return content;
        }
    }
}

public static class JsonHelper
{
    public static T[] FromJson<T>(string json)
    {
        Wrapper<T> wrapper = JsonUtility.FromJson<Wrapper<T>>(json);
        return wrapper.Items;
    }

    public static string ToJson<T>(T[] array)
    {
        Wrapper<T> wrapper = new Wrapper<T>();
        wrapper.Items = array;
        return JsonUtility.ToJson(wrapper);
    }

    public static string ToJson<T>(T[] array, bool prettyPrint)
    {
        Wrapper<T> wrapper = new Wrapper<T>();
        wrapper.Items = array;
        return JsonUtility.ToJson(wrapper, prettyPrint);
    }

    [Serializable]
    private class Wrapper<T>
    {
        public T[] Items;
    }
}
