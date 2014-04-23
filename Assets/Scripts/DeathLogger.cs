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
								Gizmos.DrawRay (level.transform.TransformPoint (new Vector3 (entry.position_x, entry.position_y, entry.position_z)), level.transform.TransformDirection (new Vector3 (entry.velocity_x, entry.velocity_y, entry.velocity_z) * entry.duration));
						}
				}
				LogClassWrapper temp = LogScript.GetLog (filename);
		if (temp != null) {
				foreach (LogClass log in temp.logs) {
						foreach (PosInstanceClass entry in log.Entries ()) {
								Gizmos.DrawRay (level.transform.TransformPoint (new Vector3 (entry.position_x, entry.position_y, entry.position_z)), level.transform.TransformDirection (new Vector3 (entry.velocity_x, entry.velocity_y, entry.velocity_z) * entry.duration));
						}
				}
		}
	}

	void OnDestroy () {
		logger.tod = DateTime.Now;
		LogScript.Append(filename, logger);
	}
}

