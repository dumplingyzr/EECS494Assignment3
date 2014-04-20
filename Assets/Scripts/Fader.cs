using UnityEngine;
using System.Collections;

public class Fader : MonoBehaviour {
	private Color colorStart;
	private Color colorEnd;
	public float duration = 1.0F;

	void Start() {
		colorStart = renderer.material.color;
		colorEnd = renderer.material.color;
		colorEnd.a = 0;
	}
	void Update() {
		float lerp = Mathf.PingPong(Time.time, duration) / duration;
		renderer.material.color = Color.Lerp(colorStart, colorEnd, lerp);
	}
}
