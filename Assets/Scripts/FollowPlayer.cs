using UnityEngine;
using System.Collections;

public class FollowPlayer : MonoBehaviour {
	public Transform character;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		this.transform.position = character.position;
	}
}
