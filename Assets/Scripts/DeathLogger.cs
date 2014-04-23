using UnityEngine;
using System.Collections;
using System;

public class DeathLogger : MonoBehaviour {
	public GameObject g;
	public GameObject level;
	LogClass logger;
	public float duration;
	public string filename;

	// Use this for initialization
	void Start () {
		logger = new LogClass (duration);
	}
	
	// Update is called once per frame
	void Update () {
			logger.Add (new PosInstanceClass (level.transform.InverseTransformPoint(g.transform.position), level.transform.InverseTransformDirection(g.rigidbody.velocity)));
	}

	void OnDrawGizmos () {
		if (logger != null) {
			foreach (PosInstanceClass entry in logger.Entries()) {
				Gizmos.DrawRay (level.transform.TransformPoint (entry.position), level.transform.TransformDirection (entry.velocity * entry.duration));
			}
		}
	}

	void OnDestroy () {
		logger.tod = DateTime.Now;
		if (filename == null)
						filename = "default";
		LogScript.Append(filename, logger);
	}
}

