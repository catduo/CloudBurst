using UnityEngine;
using System.Collections;

public class Growth : MonoBehaviour {
	
	private int numPlants = 50;
	private Transform[] plants;
	private int growthState = 5;
	private int deathState = 0;
	
	// Use this for initialization
	void Start () {
		plants = new Transform[numPlants];
		for(int i = 0; i < numPlants; i++){
			plants[i] = transform.GetChild(i);
			if(plants[i].transform.localPosition.z == 15){
				plants[i].renderer.material.mainTextureOffset = new Vector2(Mathf.Floor(Random.value*3)*0.2F, 0F);
			}
			else{
				plants[i].renderer.material.mainTextureOffset = new Vector2(Mathf.Floor(Random.value*2)*0.2F+0.6F, 0F);
			}
		}
		for(int i = 0; i < growthState; i++){
			Grow();
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnTriggerEnter (Collider other) {
		if(other.name == "BasicCloud(Clone)"){
		}
		Destroy(other.gameObject);
	}
	
	void Grow () {
		int selectedPlant = Mathf.FloorToInt(Random.value * numPlants);
		if(plants[selectedPlant].renderer.material.mainTextureOffset.y == 0F){
			plants[selectedPlant].renderer.material.mainTextureOffset = new Vector2(plants[selectedPlant].renderer.material.mainTextureOffset.x, 0.75F);
		}
		else if(plants[selectedPlant].renderer.material.mainTextureOffset.y == 0.75F){
			plants[selectedPlant].renderer.material.mainTextureOffset = new Vector2(plants[selectedPlant].renderer.material.mainTextureOffset.x, 0.5F);
		}
		else{
			Grow ();
		}
	}
	
	void Die() {
		int selectedPlant = Mathf.FloorToInt(Random.value * numPlants);
		if(plants[selectedPlant].renderer.material.mainTextureOffset.y == 0.75F){
			plants[selectedPlant].renderer.material.mainTextureOffset = new Vector2(plants[selectedPlant].renderer.material.mainTextureOffset.x, 0.25F);
		}
		else if(plants[selectedPlant].renderer.material.mainTextureOffset.y == 0.5F){
			plants[selectedPlant].renderer.material.mainTextureOffset = new Vector2(plants[selectedPlant].renderer.material.mainTextureOffset.x, 0.25F);
		}
		else{
			Die ();
		}
	}
}
