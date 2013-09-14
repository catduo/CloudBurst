using UnityEngine;
using System.Collections;

public class CloudCreator : MonoBehaviour {
	
	public GameObject basicCloud;
	public GameObject darkCloud;
	public GameObject blackCloud;
	public GameObject lightningCloud;
	public GameObject iceCloud;
	public GameObject acidCloud;
	private GameObject cloud;
	private GameObject createdCloud;
	private int count = 5;
	private float timer;
	private float delayTime = 2;
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		//time the creation of clouds
		if(Time.time > timer + delayTime){
			//determine which type of cloud to make
			switch(Mathf.FloorToInt(Random.value * count - 4)){
			case 0:
				cloud = basicCloud;
				break;
			case 1:
				cloud = darkCloud;
				break;
			default:
				cloud = blackCloud;
				break;
			};
			//create the clouds with some randomness around location
			createdCloud = (GameObject) GameObject.Instantiate(cloud, new Vector3(((Mathf.Floor(Random.value * 2) * 2) -1) * 15, Random.value * 14F - 5F, 5), Quaternion.identity);
			//set proper rotation and set parent to the clouds transform
			createdCloud.transform.eulerAngles = new Vector3(270, 0, 0);
			createdCloud.transform.parent = transform;
			count++;
			//reset the timer
			timer = Time.time;
		}
	}
}
