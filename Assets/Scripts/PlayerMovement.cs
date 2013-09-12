 using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {
	
	private float moveSpeed = 5;
	private float jumpSpeed = 500;
	public Transform ground;
	private bool grounded = true;
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		
		//moving left and right
		
		//press left move left
		if(Input.GetAxis("Horizontal") > 0){
			rigidbody.velocity = new Vector3(moveSpeed, rigidbody.velocity.y, 0);
		}
		//press right move right
		else if(Input.GetAxis("Horizontal") < 0){
			rigidbody.velocity = new Vector3(-moveSpeed, rigidbody.velocity.y, 0);
		}
		//if not pressing a direction then don't move
		else{
			rigidbody.velocity = new Vector3(0, rigidbody.velocity.y, 0);
		}
		
		
		//press up jump
		
		//the player can only jump if on the ground
		if(grounded && Input.GetAxis("Vertical") > 0){
			grounded = false;
			rigidbody.AddForce(new Vector3(rigidbody.velocity.x, jumpSpeed, 0));
		}
		
		//press down grow plant
	}
	
	void OnCollisionEnter (Collision collision) {
		if(collision.transform.parent == ground){
			grounded = true;
		}
	}
	
	
	void OnTriggerEnter (Collider other) {
		if(other.name == "BasicCloud(Clone)"){
		}
		else{
			Destroy(other.gameObject);
		}
	}
}
