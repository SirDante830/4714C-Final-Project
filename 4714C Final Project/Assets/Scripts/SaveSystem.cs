using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem
{
    public static void SavePlayer(PlayerBehavior player)
    {
        // Create a new binary formatter.
        BinaryFormatter formatter = new BinaryFormatter();

        // Set the path where the file will be saved.
        // Since we are creating our own type of file, we can set the file type to whatever we want.
        string savePath = Application.persistentDataPath + "/player.sekiro";

        // Create file stream.
        FileStream stream = new FileStream(savePath, FileMode.Create);

        // Create the data that will be written in the file.
        PlayerData data = new PlayerData(player);

        // Write the data to the file and close the stream.
        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static PlayerData LoadPlayer()
    {
        // Set the path where the file will be loaded from.
        string savePath = Application.persistentDataPath + "/player.sekiro";

        // Check to see if a file exists at the path.
        if (File.Exists(savePath))
        {
            // Create a binary formatter and stream, but this time the stream uses FileMode.Open instead of .Create.
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(savePath, FileMode.Open);

            // Deserialize the data from our stream to turn it back into a readable format and store it in a variable.
            // Tell it what kind of data you want, so you say that you want it to be as PlayerData format.
            PlayerData data = formatter.Deserialize(stream) as PlayerData;
            
            // Close the stream, very important and always a must!
            stream.Close();

            return data;
        }
        else
        {
            //Debug.LogWarning("Save file not found in " + savePath);
            return null;
        }
    }
}
