using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using UnityEngine;

public static class DataHandler
{
    private const string _xmlPath = "Resources/";
    private const string _xmlExtension = ".xml";

    public static void WriteXml<T>(T toWrite, string fileName)
    {
        XmlSerializer xml = new XmlSerializer(typeof(T));
        using(FileStream stream = new(_xmlPath + fileName + _xmlExtension, FileMode.Create, FileAccess.Write))
        {
            xml.Serialize(stream, toWrite);
        }
    }

    public static T ReadXml<T>(string fileName)
    {
        XmlSerializer xml = new XmlSerializer(typeof(T));
        using (FileStream stream = new(_xmlPath + fileName + _xmlExtension, FileMode.Open, FileAccess.Read))
        {
            return (T)xml.Deserialize(stream);
        }
    }

    public static void WriteJson<T>(T toWrite, string fileName)
    {
        string json = JsonUtility.ToJson(toWrite);
        File.WriteAllText("Resources/" + fileName + ".json", json);
    }

    public static T ReadJson<T>(string fileName)
    {
        return JsonUtility.FromJson<T>(File.ReadAllText("Resources/" + fileName + ".json"));
    }
}
