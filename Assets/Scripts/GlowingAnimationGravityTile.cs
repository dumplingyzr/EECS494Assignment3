using UnityEngine;
using System.Collections;

public class GlowingAnimationGravityTile : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		RenderSettings.haloStrength = Random.Range(0.0f, 1.0f);
	}
}
