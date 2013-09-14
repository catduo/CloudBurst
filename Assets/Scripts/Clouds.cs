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
	
	// Use this for initialization
	void Start () {
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
		//when hit the cloud tells the plants to grow, gives score, animates the particle effects, and destroys the image and colliders.
		plants.SendMessage("Grow");
		GUIControls.score += 100;
		guiControls.SendMessage("UpdateScore");
		precipitate.Play();
		poof.Play();
		Destroy(transform.GetChild(0).gameObject);
		Destroy(transform.GetChild(1).gameObject);
		Destroy(transform.GetChild(3).gameObject);
		Destroy(transform.GetChild(4).gameObject);
		Destroy(transform.GetChild(5).gameObject);
	}
}
