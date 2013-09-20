using UnityEngine;
using System.Collections;

public class CloudCreator : MonoBehaviour {
	
	public GameObject basicCloud1;
	public GameObject darkCloud1;
	public GameObject blackCloud1;
	public GameObject lightningCloud1;
	public GameObject iceCloud1;
	public GameObject acidCloud1;
	public GameObject basicCloud2;
	public GameObject darkCloud2;
	public GameObject blackCloud2;
	public GameObject lightningCloud2;
	public GameObject iceCloud2;
	public GameObject acidCloud2;
	public GameObject basicCloud3;
	public GameObject darkCloud3;
	public GameObject blackCloud3;
	public GameObject lightningCloud3;
	public GameObject iceCloud3;
	public GameObject acidCloud3;
	private GameObject cloud;
	private GameObject createdCloud;
	private float count = 0;
	private float timer;
	private float delayTime = 1.5F;
	private float obstacleCount = 0;
	private float obstacleTimer;
	private float obstacleDelayTime = 3F;
	
	// Use this for initialization
	void Start () {
		obstacleTimer = Time.time;
		timer = Time.time;
	}
	
	// Update is called once per frame
	void Update () {
		//time the creation of clouds
		if(Time.time > timer + delayTime){
			//determine which type of cloud to make
			count += Random.value;
			if(count >= 3){
				count -= 3;
			}
			switch(Mathf.FloorToInt(count)){
			case 0:
				cloud = basicCloud1;
				break;
			case 1:
				cloud = darkCloud1;
				break;
			default:
				cloud = blackCloud1;
				break;
			};
			//create the clouds with some randomness around location
			createdCloud = (GameObject) GameObject.Instantiate(cloud, new Vector3(((Mathf.Floor(Random.value * 2) * 2) -1) * 15, Random.value * Random.value * 15F - 5F, 5), Quaternion.identity);
			//set proper rotation and set parent to the clouds transform
			createdCloud.transform.eulerAngles = new Vector3(270, 0, 0);
			createdCloud.transform.parent = transform;
			count++;
			//reset the timer
			timer = Time.time;
		}
		//time the creation of obstacle clouds
		if(Time.time > obstacleTimer + obstacleDelayTime){
			//determine which type of cloud to make
			obstacleCount += Random.value;
			if(obstacleCount >= 3){
				obstacleCount -= 3;
			}
			switch(Mathf.FloorToInt(obstacleCount)){
			case 0:
				cloud = acidCloud1;
				break;
			case 1:
				cloud = lightningCloud1;
				break;
			default:
				cloud = iceCloud1;
				break;
			};
			//create the clouds with some randomness around location
			createdCloud = (GameObject) GameObject.Instantiate(cloud, new Vector3(((Mathf.Floor(Random.value * 2) * 2) -1) * 15, Mathf.Sqrt(Random.value) * 15F - 5F, 5), Quaternion.identity);
			//set proper rotation and set parent to the clouds transform
			createdCloud.transform.eulerAngles = new Vector3(270, 0, 0);
			createdCloud.transform.parent = transform;
			obstacleCount++;
			//reset the timer
			obstacleTimer = Time.time;
			obstacleDelayTime -= 1/15 * obstacleDelayTime;
		}
	}
	
	void Reset () {
		for(int i = 0; i < transform.childCount; i++){
			Destroy(transform.GetChild(i).gameObject);
		}
		count = 5;
		obstacleCount = 5;
		Start ();
	}
}
