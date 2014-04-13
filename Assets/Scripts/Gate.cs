using UnityEngine;
using System.Collections;

public class Gate : MonoBehaviour {
	public Transform left;
	public Transform right;
	private bool triggered = false;
	private int counter = 0;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider other){
		if ((other.gameObject.tag == "Player" ||
			other.gameObject.tag == "Player_G") &&
		    !triggered) {
			triggered = true;
			InvokeRepeating("GateOpen",0,0.05f);
		}
	}

	void GateOpen(){
		Vector3 offset = new Vector3 (0, 0, 0.2f);
		if (counter < 50) {
			left.position = left.position + offset;
			right.position = right.position - offset;
			counter++;
		} 
		else {
			CancelInvoke ("GateOpen");			
		}
	}
}
