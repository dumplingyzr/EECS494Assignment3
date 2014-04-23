using UnityEngine;
using System;
using System.Xml.Serialization;

public class PosInstanceClass {
	[XmlAttribute]
	public Vector3 position;
	[XmlAttribute]
	public Vector3 velocity;
	[XmlAttribute]
	public DateTime timestamp;
	[XmlAttribute]
	public float duration;

	public PosInstanceClass () {
		position = Vector3.zero;
		velocity = Vector3.zero;
		duration = Time.deltaTime;
		timestamp = DateTime.Now;
	}

	public PosInstanceClass (Vector3 pos, Vector3 vel) {
		position = pos;
		velocity = vel;
		duration = Time.deltaTime;
		timestamp = DateTime.Now;
	}

	public void Merge(PosInstanceClass that) {
		this.velocity += that.velocity;
		this.duration += that.duration;
	}
}