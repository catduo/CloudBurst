using UnityEngine;
using System.Collections;

public class Clouds : MonoBehaviour {
	
	private float scale;
	private float speed = 1.3F;
	private ParticleSystem precipitate;
	private ParticleSystem poof;
	private Transform player;
	private GameObject plants;
	private GameObject guiControls;
	private GameObject image;
	private float hitTimer = 0.5F;
	private float hitTime = 0F;
	
	// Use this for initialization
	void Start () {
		//move in the direction that would take you across the screen
		if(transform.position.x > 0){
			speed = -speed;
		}
		//find things that you'll be intereacting with
		player = GameObject.Find ("Spirit").transform;
		plants = GameObject.Find ("Plants");
		image = transform.GetChild(1).gameObject;
		guiControls = GameObject.Find ("GUI");
		//prep the two particle systems
		precipitate = GetComponentInChildren<ParticleSystem>();
		poof = GetComponent<ParticleSystem>();
		// set a random scale for some variety
		scale = Random.value * 1F + 0.5F;
		speed *=scale;
		transform.localScale = new Vector3(scale * transform.localScale.x, transform.localScale.y, scale * transform.localScale.z);
		image.renderer.material.mainTextureScale = new Vector2(0.5F, 0.25F);
		switch(transform.name){
		case "BasicCloud1(Clone)":
			image.renderer.material.mainTextureOffset = new Vector2(0.5F, 0.75F);
			break;
		case "BasicCloud2(Clone)":
			image.renderer.material.mainTextureOffset = new Vector2(0.5F, 0.75F);
			break;
		case "BasicCloud3(Clone)":
			image.renderer.material.mainTextureOffset = new Vector2(0.5F, 0.75F);
			break;
			
		
		case "DarkCloud1(Clone)":
			image.renderer.material.mainTextureOffset = new Vector2(0.5F, 0.5F);
			break;
		case "DarkCloud2(Clone)":
			image.renderer.material.mainTextureOffset = new Vector2(0.5F, 0.5F);
			break;
		case "DarkCloud3(Clone)":
			image.renderer.material.mainTextureOffset = new Vector2(0.5F, 0.5F);
			break;
			
		
		case "BlackCloud1(Clone)":
			image.renderer.material.mainTextureOffset = new Vector2(0.5F, 0.25F);
			break;
		case "BlackCloud2(Clone)":
			image.renderer.material.mainTextureOffset = new Vector2(0.5F, 0.25F);
			break;
		case "BlackCloud3(Clone)":
			image.renderer.material.mainTextureOffset = new Vector2(0.5F, 0.25F);
			break;
			
		
		case "AcidCloud1(Clone)":
			image.renderer.material.mainTextureOffset = new Vector2(0F, 0.75F);
			break;
		case "AcidCloud2(Clone)":
			image.renderer.material.mainTextureOffset = new Vector2(0F, 0.75F);
			break;
		case "AcidCloud3(Clone)":
			image.renderer.material.mainTextureOffset = new Vector2(0F, 0.75F);
			break;
			
			
		case "LightningCloud1(Clone)":
			image.renderer.material.mainTextureOffset = new Vector2(0F, 0.25F);
			break;
		case "LightningCloud2(Clone)":
			image.renderer.material.mainTextureOffset = new Vector2(0F, 0.25F);
			break;
		case "LightningCloud3(Clone)":
			image.renderer.material.mainTextureOffset = new Vector2(0F, 0.25F);
			break;
			
		
		case "IceCloud1(Clone)":
			image.renderer.material.mainTextureOffset = new Vector2(0F, 0.5F);
			break;
		case "IceCloud2(Clone)":
			image.renderer.material.mainTextureOffset = new Vector2(0F, 0.5F);
			break;
		case "IceCloud3(Clone)":
			image.renderer.material.mainTextureOffset = new Vector2(0F, 0.5F);
			break;
		
			
		default:
			image.renderer.material.mainTextureOffset = new Vector2(0.5F, 0.75F);
			break;
		}
	}
	
	void Update() {
		//if the cloud has gone off the screen, delete the cloud
		if(Mathf.Abs (transform.position.x) > 20){
			Destroy (gameObject);
		}
	}
	
	void FixedUpdate () {
		//move steadily across the screen
		rigidbody.velocity = new Vector3(speed, 0, 0);
	}
	
	void PlayerContact () {
		//don't let the player pop it with multiple quick hits, full bounces are required
		if(hitTime+hitTimer < Time.time){
			hitTime = Time.time;
			//acid clouds
			if(image.renderer.material.mainTextureOffset == new Vector2(0F, 0.75F)){
				plants.SendMessage("Die");
				plants.SendMessage("Die");
				plants.SendMessage("Die");
				plants.SendMessage("Die");
				PlayerMovement.combo = 0;
				precipitate.Play();
				poof.Play();
				Destroy(transform.GetChild(0).gameObject);
				Destroy(transform.GetChild(1).gameObject);
				Destroy(transform.GetChild(3).gameObject);
				Destroy(transform.GetChild(4).gameObject);
				Destroy(transform.GetChild(5).gameObject);
				player.SendMessage("Jump");
			}
			
			//ice clouds
			else if(image.renderer.material.mainTextureOffset == new Vector2(0F, 0.5F)){
				plants.SendMessage("Die");
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
			
			//lightning clouds
			else if(image.renderer.material.mainTextureOffset == new Vector2(0F, 0.25F)){
				PlayerMovement.combo = 0;
				GameObject flash = GameObject.Find ("LightningFlash");
				flash.SendMessage("Flash");
				GameObject plants = GameObject.Find ("Plants");
				plants.SendMessage("Lightning");
				poof.Play();
				Destroy(transform.GetChild(0).gameObject);
				Destroy(transform.GetChild(1).gameObject);
				Destroy(transform.GetChild(3).gameObject);
				Destroy(transform.GetChild(4).gameObject);
				Destroy(transform.GetChild(5).gameObject);
				player.SendMessage("Jump");
			}
			
			//black clouds when hit turn to dark clouds
			else if(image.renderer.material.mainTextureOffset == new Vector2(0.5F, 0.25F)){
				image.renderer.material.mainTextureOffset = new Vector2(0.5F, 0.5F);
				player.SendMessage("Jump");
			}
			
			//dark clouds when hit turn to basic clouds
			else if(image.renderer.material.mainTextureOffset == new Vector2(0.5F, 0.5F)){
				image.renderer.material.mainTextureOffset = new Vector2(0.5F, 0.75F);
				player.SendMessage("Jump");
			}
			
			//when hit for the last time the cloud tells the plants to grow, gives score, animates the particle effects, and destroys the image and colliders.
			else{
				plants.SendMessage("Grow");
				player.SendMessage("Jump");
				PlayerMovement.combo++;
				if(PlayerMovement.combo > 0){
					guiControls.gameObject.SendMessage("Combo");
				}
				GUIControls.score += 10 * PlayerMovement.combo;
				guiControls.gameObject.SendMessage("UpdateScore");
				precipitate.Play();
				poof.Play();
				plants.SendMessage("QuenchFire");
				Destroy(transform.GetChild(0).gameObject);
				Destroy(transform.GetChild(1).gameObject);
				Destroy(transform.GetChild(3).gameObject);
				Destroy(transform.GetChild(4).gameObject);
				Destroy(transform.GetChild(5).gameObject);
			}
		}
	}
}
