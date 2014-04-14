using UnityEngine;
using System.Collections;
using System;

public class DeathLogger : MonoBehaviour {
	ArrayList log;
	public GameObject g;
	public GameObject level;
	bool died;
	DateTime tod;
	public float duration;

	// Use this for initialization
	void Start () {
		DontDestroyOnLoad (this);
		log = new ArrayList ();
		died = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (g) {
			log.Add (new LogInstance (g));
		} else if (!died) {
			tod = DateTime.Now;
			died = true;
		}
	}

	void OnDrawGizmos () {
		foreach (LogInstance entry in log) {
			if(!died || (tod - entry.timestamp).TotalSeconds < duration)
				Gizmos.DrawRay (entry.position, entry.velocity);
		}
	}
}

class LogInstance {
	public Vector3 position;
	public Vector3 velocity;
	public DateTime timestamp;

	public LogInstance (Vector3 pos, Vector3 vel) {
		position = pos;
		velocity = vel * Time.deltaTime;
		timestamp = DateTime.Now;
		}

	public LogInstance (GameObject g) :
		this (g.transform.position, g.rigidbody.velocity)
	{
	}
}