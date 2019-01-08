using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;


public class SaveSystem{
    public static void NewGame()
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/save.owo";
        FileStream stream = new FileStream(path, FileMode.Create);

        PlayerData pd = new PlayerData();
        formatter.Serialize(stream, pd);
        stream.Close();
    }
    public static void Save(PlayerControllerSuperTwoD player)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/save.owo";
        FileStream stream = new FileStream(path, FileMode.Create);

        PlayerData pd = new PlayerData(player);
        formatter.Serialize(stream, pd);
        stream.Close();
    }
    public static PlayerData Load()
    {
        string path = Application.persistentDataPath + "/save.owo";
        if (File.Exists(path)){
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);
            PlayerData pd =  (formatter.Deserialize(stream)) as PlayerData;
            stream.Close();
            return pd;
        }
        else
        {
            Debug.LogError("There is no saving file");
            return null;
        }
    }
}
