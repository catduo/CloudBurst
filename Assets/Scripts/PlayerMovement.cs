 using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {
	
	private float moveSpeed = 5;
	private float jumpSpeed = 500;
	public Transform ground;
	public Transform clouds;
	private bool grounded = true;
	private GameObject sprite;
	private Vector2[] walk;
	private Vector2[] walkRight = new Vector2[] {new Vector2(0.2F,0.8F),new Vector2(0.4F,0.8F),new Vector2(0.6F,0.8F),new Vector2(0.8F,0.8F),new Vector2(0F,0.6F),new Vector2(0.2F,0.6F),new Vector2(0.4F,0.6F),new Vector2(0.6F,0.6F)};
	private Vector2[] walkLeft = new Vector2[] {new Vector2(0.4F,0.8F),new Vector2(0.6F,0.8F),new Vector2(0.8F,0.8F),new Vector2(1F,0.8F),new Vector2(0.2F,0.6F),new Vector2(0.4F,0.6F),new Vector2(0.6F,0.6F),new Vector2(0.8F,0.6F)};
	private Vector2[] jump;
	private Vector2[] jumpRight = new Vector2[] {new Vector2(0F,0.4F),new Vector2(0.2F,0.4F),new Vector2(0.4F,0.4F),new Vector2(0.6F,0.4F)};
	private Vector2[] jumpLeft = new Vector2[] {new Vector2(0.2F,0.4F),new Vector2(0.4F,0.4F),new Vector2(0.6F,0.4F),new Vector2(0.8F,0.4F)};
	private Vector2[] land;
	private Vector2[] landRight = new Vector2[] {new Vector2(0F,0.2F),new Vector2(0.2F,0.2F),new Vector2(0.4F,0.2F),new Vector2(0.6F,0.2F)};
	private Vector2[] landLeft = new Vector2[] {new Vector2(0.2F,0.2F),new Vector2(0.4F,0.2F),new Vector2(0.6F,0.2F),new Vector2(0.8F,0.2F)};
	private Vector2[] inAir;
	private Vector2[] inAirRight = new Vector2[] {new Vector2(0F,0F),new Vector2(0.2F,0F),new Vector2(0.4F,0F),new Vector2(0.6F,0F),new Vector2(0.8F,0F)};
	private Vector2[] inAirLeft = new Vector2[] {new Vector2(0.2F,0F),new Vector2(0.4F,0F),new Vector2(0.6F,0F),new Vector2(0.8F,0F),new Vector2(1F,0F)};
	private Vector2[] stand = new Vector2[] {new Vector2(0F,0.8F)};
	private int frame = 0;
	private string animation;
	private float animationSpeed = 0.1F;
	private float animationTime;
	
	// Use this for initialization
	void Start () {
		animationTime = Time.time;
		sprite = GameObject.Find("Sprite");
	}
	
	// Update is called once per frame
	void Update () {
		//animation based on velocity and grounded
		//if grounded then animate groudned states
		if(animation !="jumping" && animation !="landing"){
			if(grounded){
				if(Mathf.Abs (rigidbody.velocity.x) > 4){
					if(animation !="walking"){
						frame = 0;
					}
					animation = "walking";
				}
				else{
					sprite.renderer.material.mainTextureOffset = stand[0];
				}
			}
			if(!grounded){
				animation = "inAir";
			}
		}
		
		if(rigidbody.velocity.x < -4){
			walk = walkLeft;
			jump = jumpLeft;
			land = landLeft;
			inAir = inAirLeft;
			sprite.renderer.material.mainTextureScale = new Vector2(-0.2F,0.2F);
		}
		else{
			walk = walkRight;
			jump = jumpRight;
			land = landRight;
			inAir = inAirRight;
			sprite.renderer.material.mainTextureScale = new Vector2(0.2F,0.2F);
		}
		
		switch(animation){
		case "walking":
			if (animationTime + animationSpeed < Time.time){
				sprite.renderer.material.mainTextureOffset = walk[frame];
				animationTime = Time.time;
				frame++;
				if(frame == 8){
					frame = 0;
				}
			}
			break;
			
		case "jumping":
			if (animationTime + animationSpeed/3 < Time.time){
				sprite.renderer.material.mainTextureOffset = jump[frame];
				animationTime = Time.time;
				frame++;
				if(frame == 4){
					animation = "";
				}
			}
			break;
			
		case "landing":
			if (animationTime + animationSpeed/3 < Time.time){
				sprite.renderer.material.mainTextureOffset = land[frame];
				animationTime = Time.time;
				frame++;
				if(frame == 4){
					animation = "";
				}
			}
			break;
			
		case "inAir":
			if(Mathf.Abs (rigidbody.velocity.y) < 1){
				sprite.renderer.material.mainTextureOffset = inAir[2];
			}
			if(rigidbody.velocity.y <= -1 && rigidbody.velocity.y > -5){
				sprite.renderer.material.mainTextureOffset = inAir[3];
			}
			if(rigidbody.velocity.y <= -5){
				sprite.renderer.material.mainTextureOffset = inAir[4];
			}
			if(rigidbody.velocity.y >= 1 && rigidbody.velocity.y < 5){
				sprite.renderer.material.mainTextureOffset = inAir[1];
			}
			if(rigidbody.velocity.y >= 5){
				sprite.renderer.material.mainTextureOffset = inAir[0];
			}
			break;
			
		default:
			sprite.renderer.material.mainTextureOffset = stand[0];
			break;
		}
		
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
			animation = "jumping";
			frame = 0;
			Jump ();
		}
		
		//press down grow plant?
	}
	
	void Jump () {
		grounded = false;
		rigidbody.velocity = new Vector3(rigidbody.velocity.x, jumpSpeed, 0);
	}
	
	void OnCollisionEnter (Collision collision) {
		if(collision.transform == ground){
			animation = "landing";
			frame = 0;
			grounded = true;
		}
		if(collision.transform.parent == clouds) {
			rigidbody.velocity = new Vector3(rigidbody.velocity.x, jumpSpeed, 0);
		}
	}
	
	
	void OnTriggerEnter (Collider other) {
		if(other.name == "BasicCloud(Clone)" || other.name == "BasicCloud"){
			Destroy(other.gameObject);
			Jump ();
		}
		else{
		}
	}
}
