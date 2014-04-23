using UnityEngine;
using System.Collections;
using System;
using System.Xml.Serialization;

public class LogClass {
	[XmlArray,
	 XmlArrayItem(typeof(PosInstanceClass))]
	public ArrayList log;
	[XmlElement]
	public DateTime tod;
	[XmlElement]
	public DateTime tob;
	[XmlElement]
	public float duration;

	public LogClass () {
		duration = 2;
		tob = DateTime.Now;
		log = null;
	}

	public LogClass (float d) {
		duration = d;
		tob = DateTime.Now;
		log = new ArrayList ();
	}

	public void Add (PosInstanceClass p) {
		log.Add (p);
	}

	public ArrayList Entries () {
		return log;
	}
}