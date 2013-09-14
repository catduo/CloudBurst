using UnityEngine;
using System.Collections;

public class Growth : MonoBehaviour {
	
	private int numPlants = 50;
	private Transform[] plants;
	private int growthState = 0;
	private int initialGrowth = 5;
	
	// Use this for initialization
	void Start () {
		//setup the plants so that you have all of them in one big array.
		plants = new Transform[numPlants];
		for(int i = 0; i < numPlants; i++){
			plants[i] = transform.GetChild(i);
			if(plants[i].transform.localPosition.z == 15){
				//if it is in the back row make it a random tree
				plants[i].renderer.material.mainTextureOffset = new Vector2(Mathf.Floor(Random.value*3)*0.2F, 0F);
			}
			else{
				// if it is in the front row make it a random bush
				plants[i].renderer.material.mainTextureOffset = new Vector2(Mathf.Floor(Random.value*2)*0.2F+0.6F, 0F);
			}
		}
		for(int i = 0; i < initialGrowth; i++){
			//start out with some initial growth for the player
			Grow();
		}
	}
	
	// Update is called once per frame
	void Update () {
		// if the total growth of plants is 0 the player loses
		if (growthState <= 0){
			Debug.Log ("Death!!!!!!!!!!!!!!!");
		}
	}
	
	void Grow () {
		//only grow if there is room to grow
		if(growthState < 100){
			//select a random plant
			int selectedPlant = Mathf.FloorToInt(Random.value * numPlants);
			// if it is ungrown, turn it to sapling
			if(plants[selectedPlant].renderer.material.mainTextureOffset.y == 0F){
				growthState +=1;
				plants[selectedPlant].gameObject.SendMessage("SeedlingGrowth");
				plants[selectedPlant].renderer.material.mainTextureOffset = new Vector2(plants[selectedPlant].renderer.material.mainTextureOffset.x, 0.75F);
			}
			//if it is sapling, make it adult
			else if(plants[selectedPlant].renderer.material.mainTextureOffset.y == 0.75F){
				growthState +=1;
				plants[selectedPlant].gameObject.SendMessage("FullGrowth");
				plants[selectedPlant].renderer.material.mainTextureOffset = new Vector2(plants[selectedPlant].renderer.material.mainTextureOffset.x, 0.5F);
			}
			//if it is dead, make it a sapling
			else if(plants[selectedPlant].renderer.material.mainTextureOffset.y == 0.25F){
				growthState += 1;
				plants[selectedPlant].gameObject.SendMessage("SeedlingGrowth");
				plants[selectedPlant].renderer.material.mainTextureOffset = new Vector2(plants[selectedPlant].renderer.material.mainTextureOffset.x, 0.75F);
			}
			//if it was fully grown try to find another plant
			else{
				Grow ();
			}
		}
	}
	
	void Die() {
		//if something is hit find a random plant
		int selectedPlant = Mathf.FloorToInt(Random.value * numPlants);
		//if it was a sapling, kill it
		if(plants[selectedPlant].renderer.material.mainTextureOffset.y == 0.75F){
			growthState -= 1;
			plants[selectedPlant].renderer.material.mainTextureOffset = new Vector2(plants[selectedPlant].renderer.material.mainTextureOffset.x, 0.25F);
		}
		//if it was a fully grown tree, kill it
		else if(plants[selectedPlant].renderer.material.mainTextureOffset.y == 0.5F){
			growthState -= 2;
			plants[selectedPlant].renderer.material.mainTextureOffset = new Vector2(plants[selectedPlant].renderer.material.mainTextureOffset.x, 0.25F);
		}
		//if it wasn't able to die, find something else to kill
		else{
			Die ();
		}
	}
}
