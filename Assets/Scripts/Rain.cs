using UnityEngine;
using System.Collections;

public class Rain : MonoBehaviour {
	public Transform character;
	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {
		this.transform.localPosition = character.localPosition + new Vector3 (0, 300, 0);
	}
}