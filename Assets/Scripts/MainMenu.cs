using UnityEngine;
using System.Collections;

public class MainMenu : MonoBehaviour {
	public static bool levelVinayak = false;
	public static bool levelAbhinav = false;
	public static bool levelBen = false;
	public static bool levelGeorge = false;
	public static bool levelEvan = false;
	public static bool levelCG = false;

	
	public Texture2D iconTutorial;
	public Texture2D iconVinayak;
	public Texture2D iconAbhinav;
	public Texture2D iconCG;
	public Texture2D iconGeorge;
	public Texture2D iconEvan;

	public GUISkin skin;
	
	void OnGUI () {
		float xPos = Screen.width;
		float yPos = Screen.height;
		GUI.skin = skin;

		//Tutorial Level
		if (GUI.Button (new Rect (xPos*0.1f, yPos*0.3f, 200, 120), iconTutorial)) {
			Application.LoadLevel("Scene_Tutorial");
		}
		GUI.Label (new Rect (xPos*0.1f+ 50, yPos*0.3f+120, 100, 20), "TUTORIAL");

		//Vinayak's Level
		if (levelVinayak) {
			if (GUI.Button (new Rect (xPos*0.4f, yPos*0.3f, 200, 120), iconVinayak)) {
				Application.LoadLevel ("Level1_VT");
			}
			GUI.Label (new Rect (xPos*0.4f, yPos*0.3f+120, 100, 20), "Vinty");
		}
		else {
			GUI.Box (new Rect (xPos*0.4f, yPos*0.3f, 200, 120), new GUIContent(iconVinayak));
			GUI.Label (new Rect (xPos*0.4f+30, yPos*0.3f+120, 200, 20), "Level Locked");
		}

		//Abhinav's Level
		if (levelAbhinav) {
			if (GUI.Button (new Rect (xPos*0.7f, yPos*0.3f, 200, 120), iconAbhinav)) {
				Application.LoadLevel ("Level1_AJ");
			}
			GUI.Label (new Rect (xPos*0.7f, yPos*0.3f+120, 100, 20), "Rajvi");
		}
		else {
			GUI.Box (new Rect (xPos*0.7f, yPos*0.3f, 200, 120), new GUIContent(iconAbhinav));
			GUI.Label (new Rect (xPos*0.7f+30, yPos*0.3f+120, 200, 20), "Level Locked");
		}

		//Ben's Level
		if (levelCG) {
			if (GUI.Button (new Rect (xPos*0.1f, yPos*0.6f, 200, 120), iconCG)) {
				Application.LoadLevel ("Scene_CG");
			}
			GUI.Label (new Rect (xPos*0.1f, yPos*0.6f+120, 100, 20), "Color & Gravity");
		}
		else {
			GUI.Box (new Rect (xPos*0.1f, yPos*0.6f, 200, 120), new GUIContent(iconCG));
			GUI.Label (new Rect (xPos*0.1f+30, yPos*0.6f+120, 200, 20), "Level Locked");
		}

		//George's Level
		if (levelGeorge) {
			if (GUI.Button (new Rect (xPos*0.4f, yPos*0.6f, 200, 120), iconGeorge)) {
				Application.LoadLevel ("Level1_ZY");
			}
			GUI.Label (new Rect (xPos*0.4f+50, yPos*0.6f+120, 100, 20), "Shaabi");
		}
		else {
			GUI.Box (new Rect (xPos*0.4f, yPos*0.6f, 200, 120), new GUIContent(iconGeorge));
			GUI.Label (new Rect (xPos*0.4f+30, yPos*0.6f+120, 200, 20), "Level Locked");
		}

		//Evan's Level
		if (levelEvan) {
			if (GUI.Button (new Rect (xPos*0.7f, yPos*0.6f, 200, 120), iconEvan)) {
				Application.LoadLevel ("Level1_EP");
			}
			GUI.Label (new Rect (xPos*0.7f, yPos*0.6f+120, 100, 20), "Even Pots");
		}
		else {
			GUI.Box (new Rect (xPos*0.7f, yPos*0.6f, 200, 120), new GUIContent(iconEvan));
			GUI.Label (new Rect (xPos*0.7f+30, yPos*0.6f+120, 200, 20), "Level Locked");
		}
	}
	
}