using UnityEngine;
using System.Collections;
using System;
using System.IO;

public class Dialogue : MonoBehaviour
{
	private FileInfo dialogueText = null;
    private StreamReader reader;
    private string line = " "; // assigned to allow first line to be read below

    // Start is called before the first frame update
    void Start()
    {
        dialogueText = new FileInfo("Assets/Scripts/dialogue.txt");
        reader = dialogueText.OpenText();
    }
    //bla

    // Update is called once per frame
    void Update() {
        if((line = reader.ReadLine()) != null) 
        {
			string[] output = line.Split('[', ']');
			string lineText = string.Join(",", output);
			Debug.Log(lineText);

        }        
    }
}
