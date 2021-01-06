using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem {
    public static void SavePlayer (Player player, string name) {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/" + name + ".sol";
        FileStream stream = new FileStream(path, FileMode.Create);

        PlayerData data = new PlayerData(player);

        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static PlayerData LoadPlayer (string name) {
        string path = Application.persistentDataPath + "/" + name + ".sol";
        //Debug.Log(path);
        if (File.Exists(path)) {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            PlayerData data= formatter.Deserialize(stream) as PlayerData;
            stream.Close();

            return data;
        }
        else {
            Debug.LogError("Save file not found in" + path);
            return null;
        }
    }

    public static void SaveCharacter(Character character, string name) {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/" + name + ".sol";
        FileStream stream = new FileStream(path, FileMode.Create);

        CharacterData data = new CharacterData(character);

        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static CharacterData LoadCharacter(string name) {
        string path = Application.persistentDataPath + "/" + name + ".sol";
        //Debug.Log(path);
        if (File.Exists(path)) {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            CharacterData data= formatter.Deserialize(stream) as CharacterData;
            stream.Close();

            return data;
        }
        else {
            Debug.LogError("Save file not found in" + path);
            return null;
        }
    }
}
