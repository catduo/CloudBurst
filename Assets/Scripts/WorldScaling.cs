using UnityEngine;
using System.Collections;

public class WorldScaling : MonoBehaviour {
	
	public bool scaleOnX;
	public float baseXYratio;
	public Camera mainCamera;
	
	// Use this for initialization
	void Start () {
		if(scaleOnX){
			transform.position -= new Vector3(0, (mainCamera.ScreenToWorldPoint(new Vector3(0, mainCamera.pixelHeight, 0)).y - mainCamera.ScreenToWorldPoint(new Vector3(0, 0, 0)).y ) * (mainCamera.aspect / baseXYratio * transform.localScale.y), 0);
			transform.localScale = new Vector3(mainCamera.aspect / baseXYratio * transform.localScale.x, mainCamera.aspect / baseXYratio * transform.localScale.y, transform.localScale.z);
		}
		else{
			transform.localScale = new Vector3(mainCamera.aspect / baseXYratio * transform.localScale.x, mainCamera.aspect / baseXYratio * transform.localScale.y, transform.localScale.z);
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
