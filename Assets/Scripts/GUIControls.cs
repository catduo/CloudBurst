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
	public TextMesh gameOverText;
	static public int score;
	private bool paused;
	private int topScore;
	private bool gameOver = false;
	static public bool ftue;
	private int ftueLocation = 0;
	static public float timeModifier;

	// Use this for initialization
	void Start () {
		Application.targetFrameRate = 24;
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
		pauseButton.transform.position = new Vector3 (mainCamera.transform.position.x - mainCamera.camera.ScreenToWorldPoint(new Vector3(mainCamera.camera.pixelWidth, 0, 0)).x + 1, pauseButton.transform.position.y, -25);
		muteButton.transform.position = new Vector3 (mainCamera.transform.position.x - mainCamera.camera.ScreenToWorldPoint(new Vector3(mainCamera.camera.pixelWidth, 0, 0)).x + 3, muteButton.transform.position.y, -25);
		rightButton.transform.position = new Vector3 (mainCamera.transform.position.x + mainCamera.camera.ScreenToWorldPoint(new Vector3(mainCamera.camera.pixelWidth, 0, 0)).x - 1, rightButton.transform.position.y, -25);
		leftButton.transform.position = new Vector3 (mainCamera.transform.position.x + mainCamera.camera.ScreenToWorldPoint(new Vector3(mainCamera.camera.pixelWidth, 0, 0)).x - 3, leftButton.transform.position.y, -25);
		FTUETap ();
		timeModifier = Time.time;
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
			FTUE.GetComponent<FTUE>().HideFTUE();
			pauseButton.renderer.material.mainTextureOffset = new Vector2(pauseButton.renderer.material.mainTextureOffset.x, 0.5F);
		}
		else{
			FTUE.GetComponent<FTUE>().ftueLocation = 0;
			paused = true;
			Time.timeScale = 0;
			pauseButton.renderer.material.mainTextureOffset = new Vector2(pauseButton.renderer.material.mainTextureOffset.x, 0);
			ftueLocation = 0;
			pauseButton.transform.position = new Vector3(pauseButton.transform.position.x,pauseButton.transform.position.y,mainCamera.transform.position.z + 2);
			FTUE.GetComponent<FTUE>().ShowFTUE();
		}
	}
	//when mute is hit change the enabled state of the audio listener
	void MuteButtonTap () {
		if(AudioListener.volume == 0){
			AudioListener.volume = 1;
			muteButton.renderer.material.mainTextureOffset = new Vector2(muteButton.renderer.material.mainTextureOffset.x, 0.5F);
		}
		else{
			AudioListener.volume = 0;
			muteButton.renderer.material.mainTextureOffset = new Vector2(muteButton.renderer.material.mainTextureOffset.x, 0);
		}
	}
	//update the score text
	void UpdateScore () {
		scoreText.GetComponent<TextMesh>().text = score.ToString();
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
			if(Growth.growthState >= 80){
				gameOverText.text = "Victory!";
				Growth.initialGrowth = 5;
			}
			else{
				Growth.initialGrowth += Mathf.RoundToInt(score / 1000) + 1;
				gameOverText.text = "Game Over";
			}
			PlayerPrefs.SetInt("Growth", Growth.initialGrowth);
		}
	}
	
	void ScoreScreenTap () {
		scoreScreen.transform.position += new Vector3(100,0,0);
		Time.timeScale = 1;
		score = 0;
		UpdateScore();
		timeModifier = Time.time;
		GameObject.Find ("Plants").SendMessage("Reset");
		GameObject.Find ("Clouds").SendMessage("Reset");
		gameOver = false;
	}
	
	void FTUETap () {
		if(PlayerPrefs.GetInt("FTUE") == 1 && !paused){
			FTUE.GetComponent<FTUE>().HideFTUE();
		}
		else{
			if(ftueLocation > 8 &!paused){
				FTUE.GetComponent<FTUE>().HideFTUE();
				Time.timeScale = 1;
				PlayerPrefs.SetInt("FTUE",1);
			}
			else{
				FTUE.GetComponent<FTUE>().ShowFTUE();
			}
			if(ftueLocation > 7){
				ftueLocation=0;
			}
			else{
				ftueLocation++;
			}
		}
	}
}
