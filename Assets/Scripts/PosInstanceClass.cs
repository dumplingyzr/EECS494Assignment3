using UnityEngine;
using System;
using System.Xml.Serialization;

public class PosInstanceClass {
	[XmlAttribute]
	public float position_x;
	[XmlAttribute]
	public float position_y;
	[XmlAttribute]
	public float position_z;
	[XmlAttribute]
	public float velocity_x;
	[XmlAttribute]
	public float velocity_y;
	[XmlAttribute]
	public float velocity_z;
	[XmlAttribute]
	public DateTime timestamp;
	[XmlAttribute]
	public float duration;

	public PosInstanceClass () {
		position_x = 0f;
		position_y = 0f;
		position_z = 0f;
		velocity_x = 0f;
		velocity_y = 0f;
		velocity_z = 0f;
		duration = Time.deltaTime;
		timestamp = DateTime.Now;
	}

	public PosInstanceClass (Vector3 pos, Vector3 vel) {
		position_x = pos.x;
		position_y = pos.y;
		position_z = pos.z;
		velocity_x = vel.x;
		velocity_y = vel.y;
		velocity_z = vel.z;
		duration = Time.deltaTime;
		timestamp = DateTime.Now;
	}
}