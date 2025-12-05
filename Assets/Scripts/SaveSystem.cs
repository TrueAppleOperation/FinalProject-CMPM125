using System.IO;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem
{
    private static string savePath = Application.persistentDataPath + "/save.data";

    public static void SaveGame(string sceneName)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        FileStream stream = new FileStream(savePath, FileMode.Create);

        SaveData data = new SaveData(sceneName);
        formatter.Serialize(stream, data);
        stream.Close();

        Debug.Log("Game saved at: " + savePath);
    }

    public static SaveData LoadGame()
    {
        if (File.Exists(savePath))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(savePath, FileMode.Open);

            SaveData data = formatter.Deserialize(stream) as SaveData;
            stream.Close();

            return data;
        }
        else
        {
            Debug.Log("Save file not found");
            return null;
        }
    }

    public static bool SaveExists()
    {
        return File.Exists(savePath);
    }

    public static void DeleteSave()
    {
        if (File.Exists(savePath))
        {
            File.Delete(savePath);
            Debug.Log("Save file deleted");
        }
    }
}

[System.Serializable]
public class SaveData
{
    public string savedSceneName;
    public string saveTimestamp;

    public SaveData(string sceneName)
    {
        savedSceneName = sceneName;
        saveTimestamp = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
    }
}