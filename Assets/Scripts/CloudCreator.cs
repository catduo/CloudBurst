using UnityEngine;
using System.Collections;

public class CloudCreator : MonoBehaviour {
	
	public GameObject basicCloud;
	public GameObject snowCloud;
	public GameObject lightningCloud;
	public GameObject iceCloud;
	public GameObject acidCloud;
	private GameObject cloud;
	private GameObject createdCloud;
	private int count = 5;
	private float timer;
	private float delayTime = 20;
	private float cloudSpacing = 10;
	
	// Use this for initialization
	void Start () {
		timer = Time.time;
		for(float zIndex = 0; zIndex < 3; zIndex++){
			for(float i = 0; i < count; i++){
				switch(Mathf.FloorToInt(Random.value * count - 4)){
				case 0:
					cloud = basicCloud;
					break;
				case 1:
					cloud = acidCloud;
					break;
				case 2:
					cloud = snowCloud;
					break;
				case 3:
					cloud = iceCloud;
					break;
				case 4:
					cloud = lightningCloud;
					break;
				default:
					cloud = basicCloud;
					break;
				};
				createdCloud = (GameObject) GameObject.Instantiate(cloud, new Vector3(Random.value * 18F - 9F, Random.value * 5F + 6F, zIndex * cloudSpacing), Quaternion.identity);
				createdCloud.transform.eulerAngles = new Vector3(270, 90, 0);
				createdCloud.transform.parent = transform;
			}
			count++;
		}
	}
	
	// Update is called once per frame
	void Update () {
		if(Time.time > timer + delayTime){
			for(int i = 0; i < transform.childCount; i++){
				if(transform.GetChild(i).position.z > 0) {
					Transform thisCloud = transform.GetChild(i);
					thisCloud.position = new Vector3(thisCloud.position.x, thisCloud.position.y, thisCloud.position.z - cloudSpacing);
				}
			}
			for(float i = 0; i < count; i++){
				switch(Mathf.FloorToInt(Random.value * count)){
				case 0:
					cloud = basicCloud;
					break;
				case 1:
					cloud = acidCloud;
					break;
				case 2:
					cloud = snowCloud;
					break;
				case 3:
					cloud = iceCloud;
					break;
				case 4:
					cloud = lightningCloud;
					break;
				default:
					cloud = basicCloud;
					break;
				};
				createdCloud = (GameObject) GameObject.Instantiate(cloud, new Vector3(Random.value * 18F - 9F, Random.value * 5F + 6F, 3 * cloudSpacing), Quaternion.identity);
				createdCloud.transform.eulerAngles = new Vector3(270, 90, 0);
				createdCloud.transform.parent = transform;
			}
			count++;
			timer = Time.time;
		}
	}
}
