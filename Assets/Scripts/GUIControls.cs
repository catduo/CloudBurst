using UnityEngine;
using System.Collections;

public class GUIControls : MonoBehaviour {
	
	private GameObject leftButton;
	private GameObject pauseButton;
	private GameObject rightButton;
	private GameObject scoreText;
	public GameObject player;
	static public int score;

	// Use this for initialization
	void Start () {
		leftButton = GameObject.Find("LeftButton");
		rightButton = GameObject.Find("RightButton");
		pauseButton = GameObject.Find("PauseButton");
		scoreText = GameObject.Find("Score");
	}
	
	// Update is called once per frame
	void Update () {
		//find everything that is being touched and let it know
		foreach (Touch touch in Input.touches) {
            Ray ray = Camera.main.ScreenPointToRay(touch.position);
            RaycastHit objectTouched ;
            if (Physics.Raycast (ray, out objectTouched)) {
                 objectTouched.transform.SendMessage(objectTouched.transform.name, SendMessageOptions.DontRequireReceiver);
            }
	    }
		//find everything that has a touch initiated on it and let it know
		foreach (Touch touch in Input.touches) {
			if(touch.phase == TouchPhase.Began){
	            Ray ray = Camera.main.ScreenPointToRay(touch.position);
	            RaycastHit objectTouched ;
	            if (Physics.Raycast (ray, out objectTouched)) {
	                 objectTouched.transform.SendMessage(objectTouched.transform.name + "Tap", SendMessageOptions.DontRequireReceiver);
	            }
			}
	    }
		//find everything that has the mouse down on it and let it know
		if(Input.GetMouseButton(0)){
			Vector3 simTouch = Input.mousePosition;
	        Ray simRay = Camera.main.ScreenPointToRay(simTouch);
	        RaycastHit objectTouchedSim ;
	        if (Physics.Raycast (simRay, out objectTouchedSim)) {
	             objectTouchedSim.transform.parent.SendMessage(objectTouchedSim.transform.name, SendMessageOptions.DontRequireReceiver);
	        }
		}
		//find everything that has the mouse click it and let it know
		if(Input.GetMouseButtonDown(0)){
			Vector3 simTouch = Input.mousePosition;
	        Ray simRay = Camera.main.ScreenPointToRay(simTouch);
	        RaycastHit objectTouchedSim ;
	        if (Physics.Raycast (simRay, out objectTouchedSim)) {
	             objectTouchedSim.transform.parent.SendMessage(objectTouchedSim.transform.name + "Tap", SendMessageOptions.DontRequireReceiver);
	        }
		}
	}
	
	//when left is tapped, the character jumps
	void LeftButtonTap () {
		if(PlayerMovement.grounded){
			player.SendMessage("Jump");
		}
	}
	//when right is tapped the character jumps
	void RightButtonTap () {
		if(PlayerMovement.grounded){
			player.SendMessage("Jump");
		}
	}
	//when left is held the character goes left
	void LeftButton () {
		player.SendMessage("MoveLeft");
	}
	//when right is held the character goes right
	void RightButton () {
		player.SendMessage("MoveRight");
	}
	//when paus is hit pause the game and open the menu, when hit again take down the menu
	void PauseButtonTap () {
		Debug.Log ("Pause");
	}
	//update the score text
	void UpdateScore () {
		scoreText.guiText.text = "Score: " + score.ToString();
	}
}
