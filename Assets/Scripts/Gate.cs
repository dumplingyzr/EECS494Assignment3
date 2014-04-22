using UnityEngine;
using System.Collections;

public class Gate : MonoBehaviour {
	public Transform left;
	public Transform right;
	private bool triggered = false;
	private int counter = 0;

	AudioSource power;
	// Use this for initialization
	void Start () {
		AudioSource[] audios = GetComponents<AudioSource>();
		//bg = audios[0];
		power = audios[0];
		//flip = audios[2];
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
			power.Play();
		}
	}

	void GateOpen(){
		Vector3 offset = new Vector3 (0, 0, 0.2f);
		if (counter < 10/offset.z) {
			left.localPosition = left.localPosition + offset;
			right.localPosition = right.localPosition - offset;
			counter++;
		} 
		else {
			CancelInvoke ("GateOpen");			
		}
	}
}
