using UnityEngine;
using System.Collections;

public class Coin : MonoBehaviour {

	// Use this for initialization
	void Start () {
		InvokeRepeating ("Spinning", 0, 0.01f);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void Spinning(){
		transform.Rotate (0, 2, 0, Space.Self);
	}
}
