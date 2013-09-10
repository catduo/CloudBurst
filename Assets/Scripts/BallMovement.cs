using UnityEngine;
using System.Collections;

public class BallMovement : MonoBehaviour {
	
	private float ballSpeed = 3;
	private bool upwards = false;
	public Transform player;
	public Transform cloudParent;
	public Transform ground;
	public Transform top;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(upwards){
			rigidbody.velocity = new Vector3(rigidbody.velocity.x, ballSpeed, 0);
		}
		else{
			rigidbody.velocity = new Vector3(rigidbody.velocity.x, -ballSpeed, 0);
		}
	}
	
	void OnCollisionEnter (Collision collision) {
		//when hitting the player go up
		if(collision.transform == player){
			upwards = true;
		}
		//when hitting any cloud go down
		if(collision.transform.parent == cloudParent){
			upwards = false;
		}
		//when hitting the ground dissapear
		if(collision.transform.parent == ground){
			Destroy (gameObject);
		}
	}
	void OnTriggerEnter (Collider other) {
		if(other.transform == top) {
			upwards = false;
		}
	}
}
