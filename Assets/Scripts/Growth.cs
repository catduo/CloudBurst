using UnityEngine;
using System.Collections;

public class Growth : MonoBehaviour {
	
	private MeshRenderer[] plants = new MeshRenderer[100];
	
	// Use this for initialization
	void Start () {
		for(int i = 0; i < 100; i++){
			plants[i] = transform.GetChild(i).GetComponent<MeshRenderer>();
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
}
