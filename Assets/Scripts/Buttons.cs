using UnityEngine;
using System.Collections;

public class Buttons : MonoBehaviour {
	public Sprite Main_Menu_H;
	public Sprite Replay_H;
	public Sprite Next_Level_H;
	public Sprite Original;
	// Use this for initialization
	
	private Color colorStart;
	private Color colorEnd;
	private float duration = 0.2F;

	void Start () {
		
		colorStart = renderer.material.color;
		colorEnd = renderer.material.color;
		colorEnd.a = 0.2f;	
	}
	
	void OnMouseOver(){
		
		float lerp = Mathf.PingPong(Time.time, duration) / duration;
		renderer.material.color = Color.Lerp(colorStart, colorEnd, lerp);
	}
	void OnMouseExit(){
		renderer.material.color = colorStart;
	}
	void OnMouseDown(){
		if (gameObject.tag == "Main_Menu")
			Application.LoadLevel ("Scene_MainMenu");//Need Main Menu
		if (gameObject.tag == "Replay")
			Application.LoadLevel (Character.Curr_Level);
		if (gameObject.tag=="Next_Level")
			Application.LoadLevel (Character.Next_Level);

	}
}
