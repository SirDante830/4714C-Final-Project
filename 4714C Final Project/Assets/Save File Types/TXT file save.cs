using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using UnityEngine;

public class TXTfilesave : MonoBehaviour
{
    private string _dataPath;
    private string _textFile;
    /*private PlayerBehavior pB;*/
    void Awake()
    {
        //Creates a path for the file
        _dataPath = Application.persistentDataPath + "/Player_Data/";
        Debug.Log(_dataPath);
        //Checks if a directory exists for the data, if not it will be created
        if (!Directory.Exists(_dataPath))
        {
            Directory.CreateDirectory(_dataPath);
        }
        //creates the text file name
        _textFile = _dataPath + "Save_Data.txt";

        //Checks if file name exists
        if (File.Exists(_textFile))
        {
            Debug.Log("File already exists...");
            /*return;*/
        }
        File.WriteAllText(_textFile, "<SAVE DATA>\n");
        Debug.Log("New file  created!");
        /*File.Delete(_textFile);*/

        //Starts the first line of the text file
        StreamWriter newStream = File.CreateText(_textFile);
        newStream.WriteLine("<Save Data> for MY GAME \n");
        newStream.Close();

        //Adds the class names to the text file
        StreamWriter streamWriter = File.AppendText(_textFile);
        streamWriter.WriteLine("Classes:\n archer\n blueberry\n wizard");
        streamWriter.Close();

        //All the lines are read
        StreamReader streamReader = new StreamReader(_textFile);
        Debug.Log(streamReader.ReadToEnd());
        Debug.Log(File.ReadAllText(_textFile));
        
       

    }


    // Start is called before the first frame update
    void Start()
    {
       /* StreamWriter newStream = File.CreateText(filename);
        newStream.WriteLine("<Save Data> for MY GAME \n");
        newStream.Close();*/
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void createText()
    {
        string path = Application.dataPath + "log.txt";
    }
}
