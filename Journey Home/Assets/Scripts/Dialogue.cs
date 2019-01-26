using UnityEngine;
using System.Collections;
using System;
using System.IO;

public class Dialogue : MonoBehaviour
{
	protected FileInfo dialogueText = null;
    protected StreamReader reader;
    protected string text = " "; // assigned to allow first line to be read below

    // Start is called before the first frame update
    void Start()
    {
        dialogueText = new FileInfo("dialogue.txt");
        reader = theSourceFile.OpenText();
    }

    // Update is called once per frame
    void Update() {
        if((line = reader.ReadLine()) != null) 
        {
            text = reader.ReadLine();
            //Console.WriteLine(text);
            print (text);
        }        
    }
}
