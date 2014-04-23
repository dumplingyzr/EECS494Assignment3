using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;
using System.IO;
using System;

[ExecuteInEditMode]
public static class LogScript {
	static Dictionary <string, LogClassWrapper> logs;

	static LogScript () {
		logs = new Dictionary<string, LogClassWrapper> ();
		foreach (string filename in Directory.GetFiles (Application.persistentDataPath)) {
			XmlSerializer serializer = new XmlSerializer (typeof (LogClassWrapper));
			FileStream reader = File.Open (filename, FileMode.OpenOrCreate);
			LogClassWrapper temp = serializer.Deserialize (reader) as LogClassWrapper;
			logs.Add (Path.GetFileName(filename), temp);
			reader.Close ();
		}
	}

	public static void Append (string filename, LogClass log) {
		LogClassWrapper temp;
		if (logs.ContainsKey (filename)) {
			logs.TryGetValue (filename, out temp);
			temp.logs.Add (log);
			File.Create("filename");
		} else {
			temp = new LogClassWrapper ();
			temp.logs.Add (log);
			logs.Add (filename, temp);
		}

		XmlSerializer serializer = new XmlSerializer (typeof (LogClassWrapper));
		FileStream writer = File.Open (Path.Combine (Application.persistentDataPath, filename).ToString (), FileMode.Create);
		serializer.Serialize (writer, temp);
		writer.Close ();
	}
}