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
	public GameObject player;
	static public int score;
	private bool paused;
	private bool muted;
	public Rect windowRect = new Rect(20, 20, 120, 90);
	private int topScore;
	private bool gameOver = false;
	static public bool ftue;
	private int ftueLocation = 0;
	private string[] ftueTitle = new string[] {"Cloud Burst: How to Play", "Movement", "Jumping", "Bursting Clouds", "Bad Clouds"};
	private string[] ftueText = new string[] {"You are a forest spirit gathering water from clouds by touching them to make them burst.", "To move, use the arrows in the bottom right corner of the screen, or use the arrow keys.", "To jump, tap either of the arrow buttons, or press the up key.", "The white clouds will make rain.  Change darker clouds into light clouds by touching them.", "The colorful clouds will make it harder for you to burst clouds and will destroy your forest."};
	private string[] ftueButton = new string[] {"Next","Next","Next","Next","Start"};

	// Use this for initialization
	void Start () {
		PlayerPrefs.DeleteAll();
		if(PlayerPrefs.GetInt("FTUE") != 1){
			ftue = true;
			Time.timeScale = 0;
		}
		else{
			ftue = false;
		}
		topScore = PlayerPrefs.GetInt("TopScore");
		mainCamera = GameObject.Find("MainCamera");
		leftButton = GameObject.Find("LeftButton");
		rightButton = GameObject.Find("RightButton");
		pauseButton = GameObject.Find("PauseButton");
		muteButton = GameObject.Find("MuteButton");
		scoreText = GameObject.Find("Score");
		comboText = GameObject.Find("ComboText");
		pauseButton.renderer.material.mainTextureOffset = new Vector2(0, 0.5F);
		muteButton.renderer.material.mainTextureOffset = new Vector2(0.25F, 0.5F);
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
		leftButton.renderer.material.mainTextureOffset = new Vector2(leftButton.renderer.material.mainTextureOffset.x, 0);
		rightButton.renderer.material.mainTextureOffset = new Vector2(rightButton.renderer.material.mainTextureOffset.x, 0.5F);
	}
	//when right is held the character goes right
	void RightButton () {
		player.SendMessage("MoveRight");
		rightButton.renderer.material.mainTextureOffset = new Vector2(rightButton.renderer.material.mainTextureOffset.x, 0);
		leftButton.renderer.material.mainTextureOffset = new Vector2(leftButton.renderer.material.mainTextureOffset.x, 0.5F);
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
			mainCamera.transform.position += new Vector3(0, 0, 100F);
			muted = false;
			muteButton.renderer.material.mainTextureOffset = new Vector2(muteButton.renderer.material.mainTextureOffset.x, 0.5F);
		}
		else{
			mainCamera.transform.position += new Vector3(0, 0, -100F);
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
		gameOver = true;
		Time.timeScale = 0;
		if(topScore < 10){
			topScore = score;
		}
		else if(score > topScore){
			topScore = score;
		}
		PlayerPrefs.SetInt("TopScore", topScore);
	}
	
	void Restart () {
		Time.timeScale = 1;
		score = 0;
		GameObject.Find ("Plants").SendMessage("Start");
		GameObject.Find ("Clouds").SendMessage("Reset");
		gameOver = false;
	}
	
	void OnGUI () {
        GUI.skin.button.wordWrap = true;
		if(gameOver){
        	windowRect = GUI.Window(0, new Rect(20, 20, 140, 90), DoMyWindow, "GameOver");
		}
		if(ftue){
        	windowRect = GUI.Window(0, new Rect(20, 20, 300, 100), FTUEWindow, ftueTitle[ftueLocation]);
		}
    }
    void DoMyWindow(int windowID) {
		GUI.TextField(new Rect(10, 20, 120, 20), "This Score: " + score.ToString());
		GUI.TextField(new Rect(10, 40, 120, 20), "Your Best: " + topScore.ToString());
        if (GUI.Button(new Rect(10, 60, 120, 20), "Play Again")){
            Restart ();
		}
	}
    void FTUEWindow(int windowID) {
		GUI.TextArea(new Rect(10, 20, 280, 50), ftueText[ftueLocation]);
        if (GUI.Button(new Rect(10, 70, 280, 20), ftueButton[ftueLocation])){
			if(ftueLocation < ftueButton.Length - 1){
			ftueLocation++;
			}
			else{
				ftue = false;
				PlayerPrefs.SetInt("FTUE",1);
				Time.timeScale = 1;
				player.SendMessage("Jump");
				GameObject.Find ("Plants").SendMessage("Start");
			}
		}
	}
}
