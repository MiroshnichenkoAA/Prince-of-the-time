
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class Save_System
{
    public static void SavePlayer(Hero hero)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/hero.txt";
        FileStream stream = new FileStream(path, FileMode.Create);

        Hero_Data data = new Hero_Data(hero);

        formatter.Serialize(stream, data);
        stream.Close();
    }
    public static Hero_Data LoadHero()
    {
        string path = Application.persistentDataPath + "/hero.txt";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            Hero_Data data = formatter.Deserialize(stream) as Hero_Data;
            stream.Close();
            return data;
        }
        else
        {
            Debug.LogError("Save file not fount in" + path);

            return null;
        }
    }
}
