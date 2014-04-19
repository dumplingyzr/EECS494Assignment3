using UnityEngine;
using System.Collections;

public class Buttons : MonoBehaviour {
	public Sprite Main_Menu_H;
	public Sprite Replay_H;
	public Sprite Next_Level_H;
	public Sprite Original;
	// Use this for initialization
	void Start () {
		Original = GetComponent<SpriteRenderer> ().sprite;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	void OnMouseEnter(){
		if(gameObject.tag=="Main_Menu")
			GetComponent<SpriteRenderer> ().sprite = Main_Menu_H;
		if(gameObject.tag=="Replay")
			GetComponent<SpriteRenderer> ().sprite = Replay_H;
		if(gameObject.tag=="Next_Level")
			GetComponent<SpriteRenderer> ().sprite = Next_Level_H;
	}
	void OnMouseExit(){
		GetComponent<SpriteRenderer> ().sprite = Original;
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
