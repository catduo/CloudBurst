 using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {
	
	private float moveSpeed = 5;
	private float jumpSpeed = 8;
	public Transform ground;
	private GameObject rightButton;
	private GameObject leftButton;
	public Transform clouds;
	private bool is_frozen = false;
	static public bool grounded = true;
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
	private Vector2[] frozen = new Vector2[] {new Vector2(0.8F, 0.4F)};
	private int frame = 0;
	static public int combo = 0;
	private string animation;
	private float animationSpeed = 0.1F;
	private float animationTime;
	private GameObject camera;
	private GameObject guiInterface;
	
	// Use this for initialization
	void Start () {
		camera = GameObject.Find ("MainCamera");
		leftButton = GameObject.Find ("LeftButton");
		rightButton = GameObject.Find ("RightButton");
		guiInterface = GameObject.Find ("GUI");
		animationTime = Time.time;
		sprite = GameObject.Find("Sprite");
	}
	
	// Update is called once per frame
	void Update () {
		//follow the sprite up as it climbs
		if(transform.position.y > 4.5){
			camera.transform.position = new Vector3(0, transform.position.y -4.5F, -30);
			guiInterface.transform.position = new Vector3(0, transform.position.y -4.5F, 0);
		}
		else{
			camera.transform.position = new Vector3(0, 0, -30);
			guiInterface.transform.position = new Vector3(0, 0, 0);
		}
		//only do movement if not frozen (from ice clouds)
		if(!is_frozen){
			//animation based on velocity and grounded
			//if not jumping or landing animate the other states
			if(animation !="jumping" && animation !="landing"){
				if(grounded){
					//if moving then animate walking
					if(Mathf.Abs (rigidbody.velocity.x) > 4){
						if(animation !="walking"){
							frame = 0;
						}
						animation = "walking";
					}
					//else don't animate
					else{
						animation = "standing";
						sprite.renderer.material.mainTextureOffset = stand[0];
					}
				}
				//if not on the ground animate in air states
				if(!grounded){
					animation = "inAir";
				}
			}
			//if moving left, animate using the left facing frames
			if(rigidbody.velocity.x < -4){
				walk = walkLeft;
				jump = jumpLeft;
				land = landLeft;
				inAir = inAirLeft;
				sprite.renderer.material.mainTextureScale = new Vector2(-0.2F,0.2F);
			}
			//if moving right, animate using the right facing frames
			else{
				walk = walkRight;
				jump = jumpRight;
				land = landRight;
				inAir = inAirRight;
				sprite.renderer.material.mainTextureScale = new Vector2(0.2F,0.2F);
				leftButton.renderer.material.mainTextureOffset = new Vector2(0.5F, 0.5F);
				if(rigidbody.velocity.x < 4){
					rightButton.renderer.material.mainTextureOffset = new Vector2(0.75F, 0.5F);	
				}
			}
			//animate the correct animation
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
				
				//animate in air states based on character velocity
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
				
				//if confused stand around
			default:
				sprite.renderer.material.mainTextureOffset = stand[0];
				break;
			}
			
			//moving left and right
			
			//press left move right
			if(Input.GetAxis("Horizontal") > 0){
				MoveRight ();
			}
			//press right move left
			else if(Input.GetAxis("Horizontal") < 0){
				MoveLeft ();
			}
			//do nothing here if there are touches on the arrows
			else if (Input.GetMouseButton(0) || Input.touches.Length > 0){
				
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
		}
		//if frozen keep the player in place and put up the frozen image
		else{
			sprite.renderer.material.mainTextureOffset = frozen[0];
			if(Time.time > animationTime + animationSpeed * 20){
				is_frozen = false;
			}
		}
	}
	
	//set frozen and start the timer so that other files can reference
	void Freeze () {
		rigidbody.velocity = new Vector3(0,0,0);
		animationTime = Time.time;
		is_frozen = true;
		sprite.renderer.material.mainTextureScale = new Vector2(0.2F,0.2F);
	}
	
	//move right so that other files can call it
	void MoveRight () {
		if(!is_frozen){
			rigidbody.velocity = new Vector3(moveSpeed, rigidbody.velocity.y, 0);
			rightButton.renderer.material.mainTextureOffset = new Vector2(0.75F, 0F);
		}
	}
	
	//move left so that other files can call it
	void MoveLeft () {
		if(!is_frozen){
			rigidbody.velocity = new Vector3(-moveSpeed, rigidbody.velocity.y, 0);
			leftButton.renderer.material.mainTextureOffset = new Vector2(0.5F, 0F);
		}
	}
	
	//jump so that other files can call it
	void Jump () {
		if(!is_frozen){
			grounded = false;
			rigidbody.velocity = new Vector3(rigidbody.velocity.x, jumpSpeed, 0);
		}
	}
	
	//when hitting the ground play the landing animation
	void OnCollisionEnter (Collision collision) {
		if(collision.transform == ground){
			animation = "landing";
			frame = 0;
			grounded = true;
			//reset the combo when the player hits the ground
			combo = 0;
		}
	}
	
	//when hitting a cloud, jump and let the cloud know you hit it
	void OnTriggerEnter (Collider other) {
		if(other.transform.parent.transform.parent.name == "Clouds"){
			other.transform.parent.SendMessage("PlayerContact", SendMessageOptions.DontRequireReceiver);
		}
	}
}
