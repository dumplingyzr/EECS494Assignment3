using UnityEngine;
using System.Collections;
using System.Xml.Serialization;

[XmlRoot]
public class LogClassWrapper
{
	[XmlArray,
	XmlArrayItem(typeof(LogClass))]
	public ArrayList logs;

	public LogClassWrapper ()
	{
		logs = new ArrayList ();
	}
}
