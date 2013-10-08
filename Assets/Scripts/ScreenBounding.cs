using UnityEngine;
using System.Collections;

public class ScreenBounding : MonoBehaviour {
	
	public Transform leftBound;
	public Transform rightBound;
	public Transform upperBound;
	public Camera mainCamera;
	
	// Use this for initialization
	void Start () {
		if(leftBound != null){
			leftBound.position = new Vector3(mainCamera.transform.position.x - mainCamera.ScreenToWorldPoint(new Vector3(mainCamera.pixelWidth, 0, 0)).x, leftBound.position.y, leftBound.position.z);
		}
		if(rightBound != null){
			rightBound.position = new Vector3(mainCamera.transform.position.x + mainCamera.ScreenToWorldPoint(new Vector3(mainCamera.pixelWidth, 0, 0)).x, rightBound.position.y, rightBound.position.z);
		}
		if(upperBound != null){
			upperBound.position = new Vector3(upperBound.position.x, mainCamera.transform.position.y - mainCamera.ScreenToWorldPoint(new Vector3(mainCamera.pixelHeight, 0, 0)).y, upperBound.position.z);
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
