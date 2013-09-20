using UnityEngine;
using System.Collections;

public class GUIControls : MonoBehaviour {
	
	private GameObject leftButton;
	private GameObject pauseButton;
	private GameObject rightButton;
	private GameObject muteButton;
	private GameObject mainCamera;
	private GameObject scoreText;
	private GameObject comboText;
	private GameObject scoreScreen;
	private GameObject FTUE;
	public GameObject player;
	static public int score;
	private bool paused;
	private bool muted;
	private int topScore;
	private bool gameOver = false;
	static public bool ftue;
	private int ftueLocation = -1;

	// Use this for initialization
	void Start () {
		PlayerPrefs.DeleteAll();
		if(PlayerPrefs.GetInt("FTUE") != 1){
			ftue = true;
			Time.timeScale = 1;
		}
		else{
			ftue = false;
		}
		topScore = PlayerPrefs.GetInt("TopScore");
		mainCamera = GameObject.Find("MainCamera");
		FTUE = GameObject.Find("FTUE");
		leftButton = GameObject.Find("LeftButton");
		rightButton = GameObject.Find("RightButton");
		pauseButton = GameObject.Find("PauseButton");
		muteButton = GameObject.Find("MuteButton");
		scoreScreen = GameObject.Find("ScoreScreen");
		scoreText = GameObject.Find("Score");
		comboText = GameObject.Find("ComboText");
		pauseButton.renderer.material.mainTextureOffset = new Vector2(0, 0.5F);
		muteButton.renderer.material.mainTextureOffset = new Vector2(0.25F, 0.5F);
		rightButton.renderer.material.mainTextureOffset = new Vector2(0.75F, 0.5F);
		FTUETap ();
	}
	
	// Update is called once per frame
	void Update () {
		//fade out the combo text
		if(comboText.GetComponent<TextMesh>().color.a > 0){
			comboText.GetComponent<TextMesh>().color = new Color(comboText.GetComponent<TextMesh>().color.r, comboText.GetComponent<TextMesh>().color.g, comboText.GetComponent<TextMesh>().color.b, comboText.GetComponent<TextMesh>().color.a - 0.05F);
		}
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
	//when pause is hit pause the game and open the menu, when hit again take down the menu
	void PauseButtonTap () {
		if(paused){
			Time.timeScale = 1;
			paused = false;
			pauseButton.renderer.material.mainTextureOffset = new Vector2(pauseButton.renderer.material.mainTextureOffset.x, 0.5F);
		}
		else{
			paused = true;
			Time.timeScale = 0;
			pauseButton.renderer.material.mainTextureOffset = new Vector2(pauseButton.renderer.material.mainTextureOffset.x, 0);
		}
	}
	//when mute is hit change the location of the camera to be too far away to hear the music, else move it back.
	void MuteButtonTap () {
		if(muted){
			mainCamera.GetComponent<AudioListener>().enabled = true;
			muted = false;
			muteButton.renderer.material.mainTextureOffset = new Vector2(muteButton.renderer.material.mainTextureOffset.x, 0.5F);
		}
		else{
			mainCamera.GetComponent<AudioListener>().enabled = false;
			muted = true;
			muteButton.renderer.material.mainTextureOffset = new Vector2(muteButton.renderer.material.mainTextureOffset.x, 0);
		}
	}
	//update the score text
	void UpdateScore () {
		scoreText.GetComponent<TextMesh>().text = "Score: " + score.ToString();
	}
	
	//when a combo happens display the combo count
	void Combo () {
		comboText.transform.position = player.transform.position;
		comboText.GetComponent<TextMesh>().color = new Color(comboText.GetComponent<TextMesh>().color.r, comboText.GetComponent<TextMesh>().color.g, comboText.GetComponent<TextMesh>().color.b, 1F);
		comboText.GetComponent<TextMesh>().fontSize = 15 + PlayerMovement.combo;
		comboText.GetComponent<TextMesh>().text = "x" + PlayerMovement.combo.ToString();
	}
	
	//when the game ends put up a menu that lets you restart
	void GameOver(){
		ftue = false;
		Time.timeScale = 0;
		if(gameOver == false){
			if(topScore < 10){
				topScore = score;
			}
			else if(score > topScore){
				topScore = score;
			}
			PlayerPrefs.SetInt("TopScore", topScore);
			GameObject.Find ("HighScore").GetComponent<TextMesh>().text = "High Score: " + topScore.ToString();
			GameObject.Find ("ThisScore").GetComponent<TextMesh>().text = "This Round: " + score.ToString();
			scoreScreen.transform.position -= new Vector3(100,0,0);
			gameOver = true;
		}
	}
	
	void ScoreScreenTap () {
		scoreScreen.transform.position += new Vector3(100,0,0);
		Time.timeScale = 1;
		score = 0;
		GameObject.Find ("Plants").SendMessage("Start");
		GameObject.Find ("Clouds").SendMessage("Reset");
		gameOver = false;
	}
	
	void FTUETap () {
		if(PlayerPrefs.GetInt("FTUE") == 1){
			Destroy (FTUE);
		}
		ftueLocation++;
		if(ftueLocation > 6){
			Destroy (FTUE);
			Time.timeScale = 1;
			PlayerPrefs.SetInt("FTUE",1);
		}
		else{
			for(int i = 0; i < 7; i++){
				if(i == ftueLocation){
					FTUE.transform.GetChild(i).renderer.enabled = true;
				}
				else{
					FTUE.transform.GetChild(i).renderer.enabled = false;
				}
			}
		}
	}
}
