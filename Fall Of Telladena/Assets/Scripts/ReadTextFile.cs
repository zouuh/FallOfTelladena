using UnityEngine;
using UnityEditor;
using System.IO;

public class ReadTextFile : MonoBehaviour
{
    string[] dialogue;
    public void Start() {
        dialogue = ReadString();
    }
    static string[] ReadString()
    {
        string path = "Assets/Documents/Aïki.txt";

        //Read the text from directly from the test.txt file
        StreamReader reader = new StreamReader(path);
        Debug.Log(reader.ReadLine());
        int nbDialogue = int.Parse(reader.ReadLine());
        string[] dialogue = new string[nbDialogue];
        for (int i=0; i<nbDialogue; i++) {
            string newLine = "";
            newLine += reader.ReadLine();
            dialogue[i] = newLine;
        }
        reader.Close();
        return dialogue;
    }

}
