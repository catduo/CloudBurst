using UnityEngine;
using System.Collections;

public class FTUE : MonoBehaviour {
	
	private string[] ftueText;
	private string[] ftueTitle;
	public TextMesh ftueTextMesh;
	public TextMesh ftueTitleTextMesh;
	public TextMesh ftueCTA;
	public Transform ftueBackground;
	public Transform ftueCollider;
	public Transform mainCamera;
	public Transform ftueImages;
	private int ftueLocation = 0;
	
	// Use this for initialization
	void Start () {
		string ftue1 = " I'm the Forest Spirit and \nI'll be teaching you \nhow to play.";
		string ftue2 = " To grow your forest, jump to \nthe clouds and busrt them,\neach time you burst a cloud \nyou will get a boost.";
		string ftue3 = " The darker clouds will take \nextra hits to burst.";
		string ftue4 = " Yellow lightning clouds will \nset the forest ablaze.";
		string ftue5 = " Purple acid clouds will \ndestroy living trees.";
		string ftue6 = " Blue ice clouds will \nfreeze you.";
		string ftue7 = " Jump from cloud to cloud to \nget a combo multiplier.  \nColorful clouds will break \nyour combo.";
		string ftue8 = " To win get to 80 forest level \nindicated by the green numbers \nat the bottom of the screen";
		string ftue9 = " Your forest level will carry \nover slightly from round to \nround, so keep trying!";
		string ftue1t = "Welcome to CloudBurst!";
		string ftue2t = "Burst the Clouds!";
		string ftue3t = "Brighten Dark Clouds!";
		string ftue4t = "Avoid Colorful Clouds!";
		string ftue5t = "Avoid Colorful Clouds!";
		string ftue6t = "Avoid Colorful Clouds!";
		string ftue7t = "Combo your Bursts!";
		string ftue8t = "Grow to Win!";
		string ftue9t = "Keep it Growing!";
		ftueText = new string[] {ftue1, ftue2, ftue3, ftue4, ftue5, ftue6, ftue7, ftue8, ftue9};
		ftueTitle = new string[] {ftue1t, ftue2t, ftue3t, ftue4t, ftue5t, ftue6t, ftue7t, ftue8t, ftue9t};
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	public void ShowFTUE() {
		Debug.Log (ftueImages.FindChild("FtueImages" + (ftueLocation + 1).ToString()).childCount);
		if(ftueLocation > 0){
			for(int i = 0; i < ftueImages.FindChild("FtueImages" + (ftueLocation).ToString()).childCount; i++){
				ftueImages.FindChild("FtueImages" + (ftueLocation).ToString()).GetChild(i).renderer.enabled = false;
			}
		}
		for(int i = 0; i < ftueImages.FindChild("FtueImages" + (ftueLocation + 1).ToString()).childCount; i++){
			Debug.Log ("run1");
			ftueImages.FindChild("FtueImages" + (ftueLocation + 1).ToString()).GetChild(i).renderer.enabled = true;
		}
		ftueCollider.position = new Vector3(0,0,mainCamera.position.z + 3F);
		ftueBackground.renderer.enabled = true;
		ftueTextMesh.renderer.enabled = true;
		ftueCTA.renderer.enabled = true;
		ftueTitleTextMesh.renderer.enabled = true;
		ftueTextMesh.text = ftueText[ftueLocation];
		ftueTitleTextMesh.text = ftueTitle[ftueLocation];
		ftueLocation ++;
	}
	public void HideFTUE() {
		if(ftueLocation > 0){
			for(int i = 0; i < ftueImages.FindChild("FtueImages" + (ftueLocation).ToString()).childCount; i++){
			Debug.Log ("run1");
				ftueImages.FindChild("FtueImages" + (ftueLocation).ToString()).GetChild(i).renderer.enabled = false;
			}
		}
		ftueCollider.position = new Vector3(0,0,mainCamera.position.z - 5F);
		ftueBackground.renderer.enabled = false;
		ftueTextMesh.renderer.enabled = false;
		ftueCTA.renderer.enabled = false;
		ftueTitleTextMesh.renderer.enabled = false;
		ftueCollider.collider.enabled = false;
	}
}
