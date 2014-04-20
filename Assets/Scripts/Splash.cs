using UnityEngine;
using System.Collections;

public class Splash : MonoBehaviour {

	void OnMouseDown(){
		Application.LoadLevel ("Scene_MainMenu");
		Debug.Log ("starting game after " + Time.time);
	}
}
