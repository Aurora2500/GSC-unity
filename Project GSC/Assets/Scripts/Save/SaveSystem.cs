using System.IO;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

using GSC;

public static class SaveSystem
{
    public static void SaveGame(GameInstance game, string saveName)
    {
        BinaryFormatter formatter = new BinaryFormatter();

        string path = Application.persistentDataPath + $"/saves/{saveName}.gsc";
        FileStream stream = new FileStream(path, FileMode.Create);

        GameData data = new GameData(game);

        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static GameData LoadGame(string saveName)
    {
        string path = $"{Application.persistentDataPath}/saves/{saveName}.gsc";
        BinaryFormatter formatter = new BinaryFormatter();

        FileStream stream = new FileStream(path, FileMode.Open);

        GameData data = formatter.Deserialize(stream) as GameData;
        stream.Close();

        return data;
    }

    public static void DeleteGame(string saveName)
    {
        File.Delete($"{Application.persistentDataPath}/saves/{saveName}.gsc");
    }

    public static  FileInfo[] GetSaves()
    {
        DirectoryInfo saveDirectory = new DirectoryInfo($"{Application.persistentDataPath}/saves");
        FileInfo[] saves = saveDirectory.GetFiles();
        return saves;
    }
}
