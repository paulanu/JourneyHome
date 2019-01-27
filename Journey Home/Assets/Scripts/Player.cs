using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
	public  float highScore; 

	private double lastKeyPress; 
	private bool rightArrowLastPressed; 
	private GameObject planet; 
	private bool showPenaltyLable;
	private const double TIME_LIMIT = 2f;  
	private Vector3 initialPosition; 
	private Quaternion initialRotation;

	private bool tripped = false;

    // Start is called before the first frame update
    void Start()
    {
        lastKeyPress = double.MaxValue; 
        planet = GameObject.Find("Planet");
        initialPosition = transform.position; 
        initialRotation = transform.rotation; 
    }

    // Update is called once per frame
    void Update()
    {
    	double currentTime = Time.time; 
    	if (Input.GetKeyUp("right")) 
    	{
    		tripped = false; 
    		transform.position = initialPosition; 
    		transform.rotation = initialRotation;

    		if (!rightArrowLastPressed && (currentTime < lastKeyPress + TIME_LIMIT)) 
    		{
    			if (currentTime < lastKeyPress + TIME_LIMIT) {
    				planet.transform.Rotate(Vector3.forward * 5);
    				showPenaltyLable = false; 
    			}
    		}
    		rightArrowLastPressed = true; 
    		lastKeyPress = currentTime; 
    	}

    	else if (Input.GetKeyUp("left"))
    	{
    		tripped = false;
			transform.position = initialPosition; 
    		transform.rotation = initialRotation; 
    		if (rightArrowLastPressed)
    		{
    			if (currentTime < lastKeyPress + TIME_LIMIT) {
    				planet.transform.Rotate(Vector3.forward * 5);
    				showPenaltyLable = false; 
    			}
    		}
    		rightArrowLastPressed = false; 
    		lastKeyPress = currentTime; 
    	}
		
		//player shakes if they're about to trip
		if (currentTime > lastKeyPress + TIME_LIMIT - TIME_LIMIT/3 && tripped == false) {
			transform.position = initialPosition;
			Vector2 jitter = Random.insideUnitCircle * 1/7; 
			Vector3 newPosition = new Vector3(transform.position.x + jitter.x, transform.position.y + jitter.y, 0);
			transform.position = newPosition;
		}

		if (currentTime > lastKeyPress + TIME_LIMIT && tripped == false) {
    		enactPenalty(); 
    		StartCoroutine("Trip");
    		tripped = true;
		}
    }

    private IEnumerator Trip() {
    	float speed = 18f * Time.deltaTime; 
    	Vector3 target = new Vector3(initialPosition.x, initialPosition.y - 5f, 0);
    	//move down a little
	    while (transform.position != target)
	    {
			transform.position = Vector3.MoveTowards(transform.position, target, speed);
			yield return 0;
		}
		
		//then bounce up
		target = new Vector3(initialPosition.x, initialPosition.y + 1.5f, 0);
	    while (transform.position != target)
	    {
			transform.position = Vector3.MoveTowards(transform.position, target, speed);
			yield return 0;
		}

		//then fall down sideways a liiittle forward
		target = new Vector3(initialPosition.x + .5f, initialPosition.y, 0); 
    	transform.Rotate (Vector3.forward * 90);
		
		while (transform.position != target) {
			transform.position = Vector3.MoveTowards(transform.position, target, speed);
            // transform.rotation = Quaternion.Slerp(oldRotation, newRotation, speed);
			yield return 0;
		}

		// yield return new WaitForSeconds(waitTime)

    }

    public void enactPenalty() 
    {
    	showPenaltyLable = true; 
    }

    void OnGUI() 
    {
	    if (showPenaltyLable) 
	    {
	    	GUI.TextField(new Rect(10, 10, 100, 20), "YOU FUCKED UP LOL");
        }
    }

}
