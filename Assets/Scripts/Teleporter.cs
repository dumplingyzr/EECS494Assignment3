using UnityEngine;
using System.Collections;

public class Teleporter : MonoBehaviour {

	public GameObject exit;
	public int TP_Type = 2;//1 for one-way and 2 for two-way;
	private Vector3 offset = new Vector3(0,0,0);
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnCollisionEnter (Collision other){
		if (other.gameObject.tag == "Player" ||
		    other.gameObject.tag == "Player_G") {
			exit.GetComponent<BoxCollider>().isTrigger = true;
			offset.y = 5;
			other.gameObject.transform.position = exit.transform.position + offset;
		}
	}

	void OnTriggerExit (Collider other){
		if(TP_Type==2)
			this.GetComponent<BoxCollider>().isTrigger = false;
	}
}
