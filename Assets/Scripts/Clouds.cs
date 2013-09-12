using UnityEngine;
using System.Collections;

public class Clouds : MonoBehaviour {
	
	private Transform ball;
	private Transform ground;
	private float scale;
	private float downSpeed = 0.1F;
	private ParticleSystem precipitate;
	private bool hit = false;
	private Light flash1;
	private Light flash2;
	private Light flash3;
	private Light flash4;
	private Light flash5;
	
	// Use this for initialization
	void Start () {
		if(transform.name == "LightningCloud(Clone)"){
			flash1 = transform.GetChild (0).transform.GetComponent<Light>();
			flash2 = transform.GetChild (1).transform.GetComponent<Light>();
			flash3 = transform.GetChild (2).transform.GetComponent<Light>();
			flash4 = transform.GetChild (3).transform.GetComponent<Light>();
			flash5 = transform.GetChild (4).transform.GetComponent<Light>();
		}
		ball = GameObject.Find ("Ball").transform;
		ground = GameObject.Find ("Ground").transform;
		precipitate = GetComponent<ParticleSystem>();
		scale = Random.value * 2F + 0.5F;
		transform.localScale = new Vector3(transform.localScale.x, scale * transform.localScale.y, scale * transform.localScale.z);
	}
	
	void Update() {
		if(transform.name == "LightningCloud(Clone)"){
			flash1.range += (Random.value -0.5F)/4;
			if(flash1.range > 2){
				flash1.range = 0;
			}
			flash2.range += (Random.value -0.5F)/4;
			if(flash2.range > 2){
				flash2.range = 0;
			}
			flash3.range += (Random.value -0.5F)/4;
			if(flash3.range > 2){
				flash3.range = 0;
			}
			flash4.range += (Random.value -0.5F)/4;
			if(flash4.range > 2){
				flash4.range = 0;
			}
			flash5.range += (Random.value -0.5F)/4;
			if(flash5.range > 2){
				flash5.range = 0;
			}
		}
	}
	
	void FixedUpdate () {
		if(!hit){
			rigidbody.velocity = new Vector3(Mathf.Cos (Time.time * 2 + 20 * scale) / 2, Mathf.Cos (Time.time * 3 + 15 * scale) / 4, 0);
		}
	}
	
	void OnCollisionEnter (Collision collision) {
		if(collision.transform == ball) {
			precipitate.Play();
			collider.isTrigger = true;
			rigidbody.useGravity = true;
			renderer.enabled = false;
			hit = true;
		}
	}
}
