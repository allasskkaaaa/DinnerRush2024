using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public static class SaveSystem
{
   public static void SavePlayer(GameManager gameManager)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/player.dr";
        FileStream stream = new FileStream (path,FileMode.Create);

        PlayerData data = new PlayerData(gameManager);

        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static PlayerData loadPlayer()
    {
        string path = Application.persistentDataPath + "/player.dr";

        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path,FileMode.Open);

            PlayerData data = formatter.Deserialize(stream) as PlayerData;

            stream.Close();
            return data;
        }
        else
        {
            Debug.Log("SaveFile not found in "+ path);
            return null;
        }
    }

    public static void ResetPlayerData()
    {
        string path = Application.persistentDataPath + "/player.dr";

        if (File.Exists(path))
        {
            File.Delete(path);
            Debug.Log("Player data reset.");
        }
        else
        {
            Debug.Log("No save file found to reset.");
        }
    }
}
