using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;
using System.IO;

public class XMLFile : MonoBehaviour
{
    // Reference to the player behavior script.
    private PlayerBehavior pB;

    void Start()
    {
        // Set reference to player behavior script attached to the player object.
        pB = GameObject.FindWithTag("Player").GetComponent<PlayerBehavior>();

        // Function that writes an xml file.
        WriteXMLFile();
    }

    void WriteXMLFile()
    {
        // Make a string variable that stores the path to where the file is located.
        string fileLocation = Application.persistentDataPath + "_playerClasses.xml";

        // Create the xml file at the fileLocation variable defined above.
        FileStream xmlStream = File.Create(fileLocation);

        // Create a writer for the document that can be referenced below when adding info to the document.
        XmlWriter xmlWriter = XmlWriter.Create(xmlStream);

        // If the file exists, write in it.
        if (File.Exists(fileLocation))
        {
            // Begin writing in the document.
            xmlWriter.WriteStartDocument();

            // Write the start element.
            xmlWriter.WriteStartElement("Player_Classes");

            // For each entry in the playerClassName list from the player behavior script, write a string in the xml file with the specified element name.
            foreach (string playerClass in pB.playerClassName)
            {
                xmlWriter.WriteElementString("Class-Name", "Class: " + playerClass + "\n");
            }

            // Write the end element and close the writer and stream to save it and remove it from memory.
            xmlWriter.WriteEndElement();
            xmlWriter.Close();
            xmlStream.Close();
        }

    }
}