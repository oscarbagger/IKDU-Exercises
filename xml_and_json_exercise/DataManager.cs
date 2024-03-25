using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
using System.Xml;
using System.Xml.Serialization;
using System.Text;

public class DataManager : MonoBehaviour
{
    private string dataPath;
    private string textFile;
    private string xmlFilePath;
    private string jsonFilePath;

    List<Person> savedMembers= new List<Person>();

    List<Person> groupMember = new List<Person> 
        { 
            new Person("Oscar",28,"green"),
            new Person("Alexander",23,"green"),
            new Person("Tristan",23,"green"),
            new Person("Marcus",20,"black"),
            new Person("Gilah",19,"all of them")
    };

    void Awake()
    {
        dataPath = Application.persistentDataPath + "/Player_Data/";
        xmlFilePath = dataPath + "data.xml";
        jsonFilePath = dataPath + "data.json";
        Debug.Log(dataPath);
        //

    }
    void Start()
    {
        Initialize();
    }
    public void Initialize()
    {
        //FilesystemInfo();
        DeleteDirectory();
        NewDirectory();
        //WriteToXML(xmlFilePath);
        SerializeXML();
        SerializeJSON();
        DeserializeXML();
    }
    public void FilesystemInfo()
    {
        Debug.LogFormat("Path separator character: {0}", Path.PathSeparator);
        Debug.LogFormat("Directory separator character: {0}", Path.DirectorySeparatorChar);
        Debug.LogFormat("Current directory: {0}", Directory.GetCurrentDirectory());
        Debug.LogFormat("Temporary path: {0}", Path.GetTempPath());
    }
    public void NewDirectory()
    {
        if (Directory.Exists(dataPath))
        {
            Debug.Log("Directory already exists...");
            return;
        }

        Directory.CreateDirectory(dataPath);
        Debug.Log("New directory created!");
    }

    public void DeleteDirectory()
    {
        if (!Directory.Exists(dataPath))
        {
            Debug.Log("Directory doesn't exist or has already been deleted...");
            return;
        }

        Directory.Delete(dataPath, true);
        Debug.Log("Directory successfully deleted!");
    }

    public void WriteToXML(string filename)
    {
        if (!File.Exists(filename))
        {
            FileStream xmlStream = File.Create(filename);
            XmlWriter xmlWriter = XmlWriter.Create(xmlStream);

            xmlWriter.WriteStartDocument();
            xmlWriter.WriteStartElement("data");

            for (int i=0;i<groupMember.Count;i++)
                {
                xmlWriter.WriteStartElement("person");
                xmlWriter.WriteElementString("name", groupMember[i].name);
                xmlWriter.WriteElementString("year", groupMember[i].year.ToString());
                xmlWriter.WriteElementString("color", groupMember[i].color);
                xmlWriter.WriteEndElement();
                }
            xmlWriter.WriteEndElement();


            xmlWriter.Close();
            xmlStream.Close();
        } 
    }
    public void SerializeXML()
    {
        var xmlSerializer = new XmlSerializer(typeof(List<Person>));

        using (FileStream stream = File.Create(xmlFilePath))
        {
            xmlSerializer.Serialize(stream, groupMember);
        }
    }
    public void SerializeJSON()
    {
            string jsonString = JsonUtility.ToJson(groupMember[0], true);

        using (StreamWriter stream = File.CreateText(jsonFilePath))
        {
            stream.WriteLine(jsonString);
        }
    }
    public void DeserializeXML()
    {
        if (File.Exists(xmlFilePath))
        {
            var xmlSerializer = new XmlSerializer(typeof(List<Person>));

            using (FileStream stream = File.OpenRead(xmlFilePath))
            {
                var person = (List<Person>)xmlSerializer.Deserialize(stream);

                foreach (var p in person)
                {
                    savedMembers.Add(p);
                }
                Debug.Log(savedMembers.Count);
            }
        }
    }
}

