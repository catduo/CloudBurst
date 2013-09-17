using UnityEngine;
using System.Collections;

public class Lightning : MonoBehaviour {
	
	private MeshRenderer meshRenderer;
	private double flashTime;
	private double flashDuration = 0.1F;
	
	// Use this for initialization
	void Start () {
		meshRenderer = GetComponent<MeshRenderer>();
	}
	
	// Update is called once per frame
	void Update () {
		if(Time.time > flashTime + flashDuration) {
			meshRenderer.enabled = false;
		}
	}
	
	void Flash () {
		meshRenderer.enabled = true;
		flashTime = Time.time;
	}
}
