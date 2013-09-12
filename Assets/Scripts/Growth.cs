using UnityEngine;
using System.Collections;

public class Growth : MonoBehaviour {
	
	private Transform plant;
	private Transform plant;
	private Transform plant;
	private Transform plant;
	private Transform plant;
	private Transform plant;
	private Transform plant;
	private Transform plant;
	private Transform plant;
	private Transform plant;
	private Transform plant;
	private Transform plant;
	private Transform plant;
	private Transform plant;
	private Transform plant;
	private Transform plant;
	private Transform plant;
	private Transform plant;
	private Transform plant;
	private Transform plant;
	private Transform plant;
	private Transform plant;
	private Transform plant;
	private Transform plant;
	private Transform plant;
	private Transform plant;
	private Transform plant;
	private Transform plant;
	private Transform plant;
	private Transform plant;
	private Transform plant;
	private Transform plant;
	private Transform plant;
	private Transform plant;
	private Transform plant;
	private Transform plant;
	private Transform plant;
	private Transform plant;
	private Transform plant;
	private Transform plant;
	private Transform plant;
	private Transform plant;
	private Transform plant;
	private Transform plant;
	private Transform plant;
	private Transform plant;
	private Transform plant;
	private Transform plant;
	private Transform plant;
	private Transform plant;
	private Transform plant;
	
	// Use this for initialization
	void Start () {
		plant = transform.GetChild(0);
		plant = transform.GetChild(0);
		plant = transform.GetChild(0);
		plant = transform.GetChild(0);
		plant = transform.GetChild(0);
		plant = transform.GetChild(0);
		plant = transform.GetChild(0);
		plant = transform.GetChild(0);
		plant = transform.GetChild(0);
		plant = transform.GetChild(0);
		plant = transform.GetChild(0);
		plant = transform.GetChild(0);
		plant = transform.GetChild(0);
		plant = transform.GetChild(0);
		plant = transform.GetChild(0);
		plant = transform.GetChild(0);
		plant = transform.GetChild(0);
		plant = transform.GetChild(0);
		plant = transform.GetChild(0);
		plant = transform.GetChild(0);
		plant = transform.GetChild(0);
		plant = transform.GetChild(0);
		plant = transform.GetChild(0);
		plant = transform.GetChild(0);
		plant = transform.GetChild(0);
		plant = transform.GetChild(0);
		plant = transform.GetChild(0);
		plant = transform.GetChild(0);
		plant = transform.GetChild(0);
		plant = transform.GetChild(0);
		plant = transform.GetChild(0);
		plant = transform.GetChild(0);
		plant = transform.GetChild(0);
		plant = transform.GetChild(0);
		plant = transform.GetChild(0);
		plant = transform.GetChild(0);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnTriggerEnter (Collider other) {
		if(other.name == "BasicCloud(Clone)"){
			plant.position += new Vector3(0F, 0.3F, 0F);
		}
		Destroy(other.gameObject);
	}
}
