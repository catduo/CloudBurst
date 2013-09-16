using UnityEngine;
using System.Collections;

public class Clouds : MonoBehaviour {
	
	private float scale;
	private float speed = 1F;
	private ParticleSystem precipitate;
	private ParticleSystem poof;
	private Transform player;
	private GameObject plants;
	private GameObject guiControls;
	private Material basicMaterial;
	private Material darkMaterial;
	private Material blackMaterial;
	
	// Use this for initialization
	void Start () {
		//set the materials to use
		basicMaterial = Resources.Load("Cloud1_415x230", typeof(Material)) as Material;;
		darkMaterial = Resources.Load("Cloud2_415x230", typeof(Material)) as Material;;
		blackMaterial = Resources.Load("Cloud3_415x230", typeof(Material)) as Material;;
		//move in the direction that would take you across the screen
		if(transform.position.x > 0){
			speed = -speed;
		}
		//find things that you'll be intereacting with
		player = GameObject.Find ("Spirit").transform;
		plants = GameObject.Find ("Plants");
		guiControls = GameObject.Find ("GUI");
		//prep the two particle systems
		precipitate = GetComponentInChildren<ParticleSystem>();
		poof = GetComponent<ParticleSystem>();
		// set a random scale for some variety
		scale = Random.value * 1.5F + 0.5F;
		transform.localScale = new Vector3(scale * transform.localScale.x, transform.localScale.y, scale * transform.localScale.z);
	}
	
	void Update() {
		//if the cloud has gone off the screen, delete the cloud
		if(Mathf.Abs (transform.localScale.x) > 20){
			Destroy (gameObject);
		}
	}
	
	void FixedUpdate () {
		//move steadily across the screen
		rigidbody.velocity = new Vector3(speed, 0, 0);
	}
	
	void PlayerContact () {
		Debug.Log (transform.GetChild(1).gameObject.renderer.material.name);
		Debug.Log(darkMaterial.name);
		//acid clouds
		if(transform.name == "AcidCloud"){
			plants.SendMessage("Die");
			PlayerMovement.combo = 0;
			precipitate.Play();
			poof.Play();
			Destroy(transform.GetChild(0).gameObject);
			Destroy(transform.GetChild(1).gameObject);
			Destroy(transform.GetChild(3).gameObject);
			Destroy(transform.GetChild(4).gameObject);
			Destroy(transform.GetChild(5).gameObject);
		}
		
		//lightning clouds
		else if(transform.name == "IceCloud"){
			PlayerMovement.combo = 0;
			precipitate.Play();
			poof.Play();
			Destroy(transform.GetChild(0).gameObject);
			Destroy(transform.GetChild(1).gameObject);
			Destroy(transform.GetChild(3).gameObject);
			Destroy(transform.GetChild(4).gameObject);
			Destroy(transform.GetChild(5).gameObject);
			player.SendMessage("Freeze");
		}
		
		//ice clouds
		else if(transform.GetChild(1).gameObject.renderer.material.name == "Cloud6_415x230 (Instance)"){
			transform.GetChild(1).gameObject.renderer.material = darkMaterial;
		}
		
		//black clouds when hit turn to dark clouds
		else if(transform.GetChild(1).gameObject.renderer.material.name == "Cloud3_415x230 (Instance)"){
			transform.GetChild(1).gameObject.renderer.material = darkMaterial;
		}
		
		//dark clouds when hit turn to basic clouds
		else if(transform.GetChild(1).gameObject.renderer.material.name == "Cloud2_415x230 (Instance)" || transform.GetChild(1).gameObject.renderer.material.name == "Cloud2_415x230"){
			transform.GetChild(1).gameObject.renderer.material = basicMaterial;
		}
		
		//when hit for the last time the cloud tells the plants to grow, gives score, animates the particle effects, and destroys the image and colliders.
		else{
			plants.SendMessage("Grow");
			PlayerMovement.combo++;
			if(PlayerMovement.combo > 0){
				guiControls.gameObject.SendMessage("Combo");
			}
			GUIControls.score += 10 * PlayerMovement.combo;
			guiControls.gameObject.SendMessage("UpdateScore");
			precipitate.Play();
			poof.Play();
			Destroy(transform.GetChild(0).gameObject);
			Destroy(transform.GetChild(1).gameObject);
			Destroy(transform.GetChild(3).gameObject);
			Destroy(transform.GetChild(4).gameObject);
			Destroy(transform.GetChild(5).gameObject);
		}
	}
}
