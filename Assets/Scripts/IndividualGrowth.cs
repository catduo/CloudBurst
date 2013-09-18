using UnityEngine;
using System.Collections;

public class IndividualGrowth : MonoBehaviour {
	
	private float startScale = 0.2F;
	private float endScale;
	private float growthRate = 1.005F;
	
	// Use this for initialization
	void Start () {
		//when starting out give the plant a somewhat randome end scale
		endScale = Random.value * 0.4F + 0.4F;
	}
	
	// Update is called once per frame
	void Update () {
		//grow until reaching the end scale
		if(transform.localScale.z < endScale){
			transform.localPosition += new Vector3(0,(transform.localScale.y * growthRate - transform.localScale.y), 0);
			transform.localScale *= growthRate;	
		}	
	}
	
	//when getting the seedling growth state start growing
	void SeedlingGrowth () {
		transform.localScale = new Vector3(startScale / 3.5F, 1, startScale);
		transform.localPosition = new Vector3(transform.localPosition.x,-6.7F,transform.localPosition.z);
	}
	
	//when getting the full growth state start growing
	void FullGrowth () {
		transform.localScale = new Vector3(startScale / 3.5F, 1, startScale);
		transform.localPosition = new Vector3(transform.localPosition.x,-6.7F,transform.localPosition.z);
	}
}
