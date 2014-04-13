using UnityEngine;
using System.Collections;

public class ColorTile : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider other){
		if (other.gameObject.tag == "Player" ||
		     other.gameObject.tag == "Player_G") {
			other.gameObject.GetComponent<MeshRenderer>().material = 
			 this.gameObject.GetComponent<MeshRenderer>().material;
		}
	}
}
