using UnityEngine;
using System.Collections;
using System;

public class DeathLogger : MonoBehaviour {
	ArrayList log;

	// Use this for initialization
	void Start () {
		log = new ArrayList ();
	}
	
	// Update is called once per frame
	void Update () {
		log.Add(LogInstance.Log (this.gameObject));
	}
	
	void OnDestroy () {

	}
}

class LogInstance {
	Vector3 position;
	Vector3 velocity;
	DateTime timestamp;

	LogInstance (Vector3 pos, Vector3 vel) {
		position = pos;
		velocity = vel;
		DateTime timestamp = DateTime.Now;
		}

	public static LogInstance Log (GameObject g) {
		return new LogInstance (g.transform.position, g.rigidbody.velocity);
		}
}