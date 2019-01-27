using UnityEngine;
using System.Collections;
using System;
using System.IO;

public class Dialogue : MonoBehaviour
{
	private FileInfo dialogueText = null;
    private StreamReader reader;
    
    private string line = " "; // assigned to allow first line to be read below
    private string keyWord; 
    private string preceding; 
    private string succeeding;

	private Rect p1 = new Rect(10, 10, 260, 20);
	private Rect p2 = new Rect(500, 10, 260, 20);

    private GameObject planet; 
	private Quaternion planetInitialRotation;
	private float degreesForEachDialogue; 
	private float currentDegree = 0; 

    // Start is called before the first frame update
    void Start()
    {
        dialogueText = new FileInfo("Assets/Scripts/dialogue.txt");
        reader = dialogueText.OpenText();
        planet = GameObject.Find("Planet");

       	int numLines = Int32.Parse(reader.ReadLine()); 
       	degreesForEachDialogue = 360 / numLines; 

       	line = reader.ReadLine(); //initialize; 
       	initializeKeyword();
    }
    //bla

    // Update is called once per frame
    void Update() {

    	//read next line if you've reached next "part"
    	if (planet.transform.localEulerAngles.z > currentDegree + degreesForEachDialogue) {
    		
    		line = reader.ReadLine();

    		//check that you're not at end of file, then read parts of line
			currentDegree += degreesForEachDialogue;

			initializeKeyword(); 
    	}
		
        //check if you're pressing the right key
        if (keyWord.Length > 0 && Input.GetKeyDown("" + keyWord[0]))  {
        	preceding += keyWord[0]; 
        	keyWord = keyWord.Substring(1);
        }
    }

    private void initializeKeyword() {
    	if(!String.IsNullOrEmpty(line)) 
        {
			string[] output = line.Split('[', ']');
			Debug.Log(line + "\n");
			preceding = output[0];
			keyWord = output[1]; //keyWord always in middle of text
			succeeding = output[2];
			string lineText = string.Join(",", output);
        }
	}

    void OnGUI() 
    {
		GUIStyle style = new GUIStyle();
		// style.skin.label.wordwrap = true; 
		GUI.skin.GetStyle("label").wordWrap = true; 
		style.wordWrap = true;
		style.richText = true;
		GUI.Label(p2,preceding + "<color=silver>" + keyWord + "</color>" + succeeding,style);
    }

}
