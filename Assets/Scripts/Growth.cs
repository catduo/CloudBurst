using UnityEngine;
using System.Collections;

public class Growth : MonoBehaviour {
	
	private int numPlants = 50;
	private Transform[] plants;
	private int growthState = 0;
	private int initialGrowth = 5;
	private GameObject sparks;
	public Material tree1;
	public Material tree2;
	public Material bush1;
	public Material bush2;
	private int animationDelay;
	private bool onFire;
	
	// Use this for initialization
	void Start () {
		//set the lightning sparks and fire objects
		onFire = false;
		sparks = GameObject.Find ("Sparks");
		//setup the plants so that you have all of them in one big array.
		plants = new Transform[numPlants];
		for(int i = 0; i < numPlants; i++){
			plants[i] = transform.GetChild(i);
			if(plants[i].transform.localPosition.z == 15){
				//if it is in the back row make it a random tree
				if(Random.value < 0.5f){
					plants[i].renderer.material = tree1;
				}
				else{
					plants[i].renderer.material = tree2;
				}
				plants[i].renderer.material.mainTextureScale = new Vector2(0.123F, 0.49F);
				plants[i].renderer.material.mainTextureOffset = new Vector2(0.5F, 0.5F);
			}
			else{
				// if it is in the front row make it a random bush
				if(Random.value < 0.5f){
					plants[i].renderer.material = bush1;
				}
				else{
					plants[i].renderer.material = bush2;
				}
				plants[i].renderer.material.mainTextureScale = new Vector2(0.125F, 0.49F);
				plants[i].renderer.material.mainTextureOffset = new Vector2(0.5F, 0.5F);
			}
		}
		if(!GUIControls.ftue){
			for(int i = 0; i < initialGrowth; i++){
				//start out with some initial growth for the player
				Grow();
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
		//if something is on fire it animates and catches other things on fire
		if(animationDelay == 5){
			for(int selectedPlant = 0; selectedPlant < numPlants; selectedPlant++){
				if(plants[selectedPlant].renderer.material.mainTextureOffset.y == 0F){
					onFire = true;
					if(plants[selectedPlant].renderer.material.mainTextureOffset.x == 0.875F){
						plants[selectedPlant].renderer.material.mainTextureOffset = new Vector2(0F, 0F);
					}
					else{
						plants[selectedPlant].renderer.material.mainTextureOffset += new Vector2(0.125F, 0F);
					}
				}
			}
			animationDelay = 0;
			if(Random.value > 0.95  && onFire){
				Fire ();
			}
		}
		
		// if the total growth of plants is 0 the player loses
		if (growthState <= 0){
			Lose ();
		}
		animationDelay ++;
	}
	
	void Grow () {
		//only grow if there is room to grow
		if(growthState < 100){
			//select a random plant
			int selectedPlant = Mathf.FloorToInt(Random.value * numPlants);
			// if it isn't an adult or sapling grow to be a sapling
			if(plants[selectedPlant].renderer.material.mainTextureOffset.x > 0.125F){
				growthState +=1;
				plants[selectedPlant].gameObject.SendMessage("SeedlingGrowth");
				plants[selectedPlant].renderer.material.mainTextureOffset = new Vector2(0F, 0.5F);
			}
			//if it is sapling, make it adult
			else if(plants[selectedPlant].renderer.material.mainTextureOffset.x == 0F){
				growthState +=1;
				plants[selectedPlant].gameObject.SendMessage("FullGrowth");
				plants[selectedPlant].renderer.material.mainTextureOffset = new Vector2(0.125F, 0.5F);
			}
			//if it was fully grown try to find another plant
			else{
				Grow ();
			}
		}
	}
	
	void Die() {
		//if there is anything left to kill
		if(growthState>0){
			//if something is hit start going through the plants
			for(int selectedPlant = 0; selectedPlant < numPlants; selectedPlant ++){
				//if burning it is already dead so don't kill it again
				if(plants[selectedPlant].renderer.material.mainTextureOffset.y == 0.5F){
					//if it was a sapling, kill it
					if(plants[selectedPlant].renderer.material.mainTextureOffset.x == 0F){
						growthState -= 1;
						plants[selectedPlant].renderer.material.mainTextureOffset = new Vector2(0.25F, 0.5F);
						break;
					}
					//if it was a fully grown tree, kill it
					else if(plants[selectedPlant].renderer.material.mainTextureOffset.x == 0.125F){
						growthState -= 2;
						plants[selectedPlant].renderer.material.mainTextureOffset = new Vector2(0.25F, 0.5F);
						break;
					}
				}
			}
		}
		else{
			Lose ();
		}
	}
	
	void Fire () {
		Fire (-1);
	}
	
	void Fire (int selectedPlant) {
		onFire = true;
		//if there is anything left to kill
		if(growthState>0){
			//if something is hit find a random plant
			if(selectedPlant == -1){
				selectedPlant = 0;
			}
			//if not burning set it on fire
			if(plants[selectedPlant].renderer.material.mainTextureOffset.y == 0.5F && plants[selectedPlant].renderer.material.mainTextureOffset.x < 0.5F){
				//if it was a sapling, kill it
				if(plants[selectedPlant].renderer.material.mainTextureOffset.x == 0F){
					growthState -= 1;
					plants[selectedPlant].renderer.material.mainTextureOffset = new Vector2(0.25F, 0F);
				}
				//if it was a fully grown tree, kill it
				else if(plants[selectedPlant].renderer.material.mainTextureOffset.x == 0.125F){
					growthState -= 2;
					plants[selectedPlant].renderer.material.mainTextureOffset = new Vector2(0.25F, 0F);
				}
				//dead trees can catch on fire
				else{
					plants[selectedPlant].renderer.material.mainTextureOffset = new Vector2(0.25F, 0F);
				}
			}
			else{
				if(selectedPlant < numPlants-1){
					selectedPlant++;
					Fire (selectedPlant);
				}
			}
		}
		else{
			Lose ();
		}
	}
	
	void QuenchFire () {
		onFire = false;
		for(int selectedPlant = 0; selectedPlant < numPlants; selectedPlant++){
			if(plants[selectedPlant].renderer.material.mainTextureOffset.y == 0F){
				plants[selectedPlant].renderer.material.mainTextureOffset = new Vector2(0.375F, 0.5F);
				break;
			}
		}
	}
	
	void Lightning () {
		//if there is anything left to kill
		if(growthState>0){
			// go through plants until you find one to kill
			for(int selectedPlant = 0; selectedPlant < numPlants; selectedPlant ++){
				if(plants[selectedPlant].renderer.material.mainTextureOffset.y == 0.5F && plants[selectedPlant].renderer.material.mainTextureOffset.x < 0.625F){
					//if it was a sapling, kill it
					if(plants[selectedPlant].renderer.material.mainTextureOffset.x == 0F){
						sparks.transform.position = plants[selectedPlant].transform.position;
						sparks.GetComponent<ParticleSystem>().Play();
						growthState -= 1;
						Fire (selectedPlant);
						break;
					}
					//if it was a fully grown tree, kill it
					else if(plants[selectedPlant].renderer.material.mainTextureOffset.x == 0.125F){
						sparks.transform.position = plants[selectedPlant].transform.position;
						sparks.GetComponent<ParticleSystem>().Play();
						growthState -= 2;
						Fire (selectedPlant);
						break;
					}
				}
			}
		}
		else{
			Lose ();
		}
	}
	
	void Lose () {
		onFire = false;
		GameObject.Find("GUI").SendMessage("GameOver");
	}
}
