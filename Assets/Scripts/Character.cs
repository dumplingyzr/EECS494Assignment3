using UnityEngine;
using System.Collections;

public class Character : MonoBehaviour {
	public float speed = 30;
	public int direction = 1;
	public Transform platform1;
	//public Transform platform2;
	public Vector3 point = new Vector3 (25,0,0);
	public Vector3 z_axis = new Vector3 (0,0,1);
	public Vector3 x_axis = new Vector3 (1,0,0);
	public Vector3 y_axis = new Vector3 (0,1,0);
	public float angle = 0;
	public static bool freeze = false;

	public float jumpSpeed = 50;
	public bool aboutToJump = false;
	public bool jumping = false;
	public float rotateVel = 0.2f;
	public bool rotating = false;
	
	private float timeSinceExit = 0.0f;
	private float timeSinceEnter = 0.0f;
	
	private bool tutOne = true;
	private bool tutTwo = false;
	private bool tutThree = false;
	private bool tutFour = false;
	private bool tutFive = false;
	private bool tutSix = false;
	private bool tutOneDone = false;
	private bool tutTwoDone = false;
	private bool tutThreeDone = false;
	private bool tutFourDone = false;
	private bool tutFiveDone = false;
	private bool tutSixDone = false;

	private float gravity_value = -500;

	private int gameTimer;
	private bool timerOn;
	
	public GameObject enemy;

	public static int Curr_Level;
	public static int Next_Level;

	public GUISkin skin;
	public Font f;

	AudioSource bg;
	AudioSource power;
	AudioSource flip;
	AudioSource getitem;
	// Use this for initialization
	
	void Start () {
		Physics.gravity = new Vector3 (0, gravity_value, 0);
		Curr_Level = Application.loadedLevel;
		Next_Level = Curr_Level + 1;
		enemy = GameObject.FindGameObjectWithTag ("Enemy");
		AudioSource[] audios = GetComponents<AudioSource>();
		bg = audios[0];
		power = audios[1];
		flip = audios[2];
		getitem = audios [3];
		freeze = false;
		timerOn = false;
		if (Application.loadedLevelName == "Scene_Tutorial") {
			gameTimer = 60;
			timerOn = true;
		} else if (Application.loadedLevelName == "Level1_ZY") {
			gameTimer = 200;
			timerOn = true;
		} else if (Application.loadedLevelName == "Level1_AJ") {
			gameTimer = 120;
			timerOn = true;
		} else if (Application.loadedLevelName == "Level1_VT") {
			gameTimer = 90;
			timerOn = true;
		} else if (Application.loadedLevelName == "Level1_EP") {
			gameTimer = 180;
			timerOn = true;
		}
		if (timerOn) {
			InvokeRepeating ("Countdown", 1, 1);
		}
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 vel = rigidbody.velocity;

		if (!freeze) {
			if ((Input.GetKeyDown (KeyCode.RightArrow) ||
			     Input.GetKeyDown (KeyCode.D)) && !rotating)
			{
				freeze = true;
				InvokeRepeating ("Rot_Y_Pos", 0.1f, 0.02f);
				direction ++;
				vel.x = 0;
				vel.z = 0;
				rotating = true;
				Invoke ("rotate_toggle", 0.11f);
				if(direction == 5)
					direction = 1;
			}
			else if (Input.GetKeyDown (KeyCode.LeftArrow) ||
			         Input.GetKeyDown (KeyCode.A))
			{
				freeze = true;
				InvokeRepeating ("Rot_Y_Neg", 0.1f, 0.02f);
				direction --;
				vel.x = 0;
				vel.z = 0;
				rotating = true;
				Invoke ("rotate_toggle", 0.11f);
				if(direction == 0)
					direction = 4;
			}
			else if (Input.GetKey (KeyCode.UpArrow) ||
			         Input.GetKey (KeyCode.W))
				vel = Move (vel);
			else {
				vel.x = vel.x * 0.8f;
				vel.z = vel.z * 0.8f;
			}
		}
		//Check if its jumping
		if (aboutToJump) {
			jumping = true;
			aboutToJump = false;
		}
		if (Input.GetKeyDown (KeyCode.Space) && !jumping) {
			vel.y = jumpSpeed;
			aboutToJump = true;
		}
		rigidbody.velocity = vel;
		
		//Tutorial Messages
		float curX = transform.position.x;
		if (curX > -130 && !tutOneDone) {
			tutOne = false;
			tutOneDone = true;
		}
		if (curX > -125 && !tutTwoDone) {
			tutTwo = true;
			tutTwoDone = true;
		}
		if (curX > -45) {
			tutTwo = false;
		}
		if (curX > -40 && !tutThreeDone) {
			tutThree = true;
			tutThreeDone = true;
		}
		if (curX > -16) {
			tutThree = false;
		}
		if (curX > -15 && !tutFourDone) {
			tutFour = true;
			tutFourDone = true;
		}
		if (curX > 40) {
			tutFour = false;
		}
		if (curX > 45 && !tutFiveDone) {
			tutFive = true;
			tutFiveDone = true;
		}
		if (curX > 70 || curX < 45) {
			tutFive = false;
		}
		if (curX > 80 && !tutSixDone) {
			tutSix = true;
			tutSixDone = true;
		}
		if (curX > 100) {
			tutSix = false;
		}
		
		if (rigidbody.velocity.y < -1)
			Time.timeScale = 0.8f;
		else
			Time.timeScale = 1.0f;
		if (vel.y < -300)
			Application.LoadLevel (Application.loadedLevel);
		
	}
	
	Vector3 Move(Vector3 vel){
		switch (direction) {
		case 1: //facing forward
			vel.x = vel.x * 0.3f + speed * 0.6f;
			if(vel.x >= speed) vel.x = speed;
			return vel;
		case 3: //facing backward
			vel.x = -speed;
			return vel;
		case 4: //facing right
			vel.z = speed;
			return vel;
		case 2: //facing left
			vel.z = -speed;
			return vel;
		default: return vel;
		}
	}
	void rotate_toggle() {
		rotating = !rotating;
	}
	void Rot_Y_Pos(){
		if (angle < 90) {
			this.transform.RotateAround (transform.position, y_axis, 10);
			//platform2.RotateAround (transform.position, z_axis, 10);
			angle += 10;
			//Rotate_displace();
			
		} else {
			CancelInvoke ("Rot_Y_Pos");
			//Physics.gravity = new Vector3 (0, -100, 0);
			angle = 0;
			freeze = false;
			
		}
	}
	
	void Rot_Y_Neg(){
		if (angle < 90) {
			this.transform.RotateAround (transform.position, y_axis, -10);
			//platform2.RotateAround (transform.position, z_axis, 10);
			angle += 10;
			//Rotate_displace();
			
		} else {
			CancelInvoke ("Rot_Y_Neg");
			//Physics.gravity = new Vector3 (0, -100, 0);
			angle = 0;
			freeze = false;
		}
	}
	
	void Rot_Z_Pos(){
		if (angle < 90) {
			Enemy.enable = false;
			platform1.RotateAround (transform.position, z_axis, 10);
			
			//platform2.RotateAround (transform.position, z_axis, 10);
			angle += 10;
			//Rotate_displace();
			
		} else {
			CancelInvoke ("Rot_Z_Pos");
			//enemy.transform.Rotate(0,0,90);
			Enemy.enable = true;
			angle = 0;
			//freeze = false;
		}
	}
	
	void Rot_Z_Neg(){
		if (angle <90) {
			Enemy.enable = false;
			platform1.RotateAround (transform.position, z_axis, -10);
			
			//platform2.RotateAround (transform.position, z_axis, -10);
			//Physics.gravity = new Vector3 (0, -100, 0);
			angle += 10;
			//Rotate_displace();
			
		} else {
			CancelInvoke ("Rot_Z_Neg");
			Enemy.enable = true;
			//Physics.gravity = new Vector3 (0, -100, 0);
			angle = 0;
			//freeze = false;
		}
	}
	
	void Rot_X_Pos(){
		if (angle < 90) {
			Enemy.enable = false;
			platform1.RotateAround (transform.position, x_axis, 10);
			
			//platform2.RotateAround (transform.position, x_axis, 10);
			angle += 10;
			//Rotate_displace();
			
		} else {
			CancelInvoke ("Rot_X_Pos");
			Enemy.enable = true;
			//Physics.gravity = new Vector3 (0, -100, 0);
			angle = 0;
			//freeze = false;
		}
	}
	
	void Rot_X_Neg(){
		if (angle <90) {
			Enemy.enable = false;
			platform1.RotateAround (transform.position, x_axis, -10);
			
			//platform2.RotateAround (transform.position, x_axis, -10);
			angle += 10;
			//Physics.gravity = new Vector3 (0, -100, 0);
			//Rotate_displace();
			
		} else {
			CancelInvoke ("Rot_X_Neg");
			Enemy.enable = true;
			//Physics.gravity = new Vector3 (0, -100, 0);
			angle = 0;
			//freeze = false;
		}
	}
	
	void OnCollisionStay(Collision other)
	{
		if (jumping && (other.gameObject.tag == "Platform" || other.gameObject.tag == "GravityTile")) {
			jumping = false;
		}
	}
	
	void MoveForward() {
		Vector3 pos = transform.position;
		pos += transform.forward*1.0f;
		transform.position = pos;
		Debug.Log ("forward at " + Time.time);
		
	}
	void MoveUp() {
		Vector3 pos = transform.position;
		pos -= Vector3.up*0.02f;
		transform.position = pos;
		Debug.Log ("up at " + Time.time);
		
	}
	void Unfreeze() {
		freeze = false;
	}
	void Rotate_displace() {
		Debug.Log ("displacing at " + Time.time);
		
		
		for(int i = 0; i< 10; i++) 
			Invoke ("MoveForward", 0.02f);
		
		for(int i = 0; i< 5; i++) 
			Invoke ("MoveUp", 0.002f);
		
		Physics.gravity = new Vector3 (0, gravity_value, 0);
		
		Invoke ("Unfreeze", 0.35f);
	}
	void OnCollisionExit(Collision other)
	{
		if (rigidbody.velocity.y < -1 
		    && 
		    ((other.gameObject.tag == "Platform" && this.gameObject.tag == "Player_G") || 
		 (other.gameObject.tag == "GravityTile") ||
		 (other.gameObject.GetComponent<MeshRenderer>().material.color == 
		 this.gameObject.GetComponent<MeshRenderer>().material.color && other.gameObject.tag == "Platform"))
		    && (timeSinceEnter - Time.time) < -0.2f
		    && !freeze) 
		{
			Debug.Log("Switching because of exit");
			Debug.Log("Leaving gravity tile: " + (other.gameObject.tag == "GravityTile"));
			Physics.gravity = new Vector3 (0, 0, 0);
			freeze = true;
			Invoke ("Rotate_displace", 0.2f);
			rigidbody.velocity = new Vector3(0,0,0);
			Physics.gravity = new Vector3(0,0,0);
			Debug.Log ("collisionexit at " + Time.time);
			
			
			switch (direction) {
			case 1:{InvokeRepeating ("Rot_Z_Pos", 0.1f, 0.02f);break;}
			case 2:{InvokeRepeating ("Rot_X_Pos", 0.1f, 0.02f);break;}
			case 3:{InvokeRepeating ("Rot_Z_Neg", 0.1f, 0.02f);break;}
			case 4:{InvokeRepeating ("Rot_X_Neg", 0.1f, 0.02f);break;}
			default: break;
			}
			//InvokeRepeating ("Rotate_displace", 0.2f, 0.02f);
			freeze = true;
			flip.Play ();
		}
		
		timeSinceExit = Time.time;
	}
	
	void OnTriggerEnter(Collider other)
	{
		timeSinceEnter = Time.time;

		if (other.tag == "Finish") {
			//if (Application.loadedLevelName == "Scene_Tutorial") {
			//	MainMenu.levelGeorge = true;
			//}
			Debug.Log("Finished" + Application.loadedLevelName);
			switch(Application.loadedLevelName)//custom level starting at 5, might need modification if the build setting is changed
			{
			case "Scene_Tutorial": MainMenu.levelVinayak = true; break;
			case "Level1_VT": MainMenu.levelCG = true; break;
			case "Level1_CG": MainMenu.levelEvan = true; break;
			case "Level1_EP": MainMenu.levelGeorge = true; break;
			case "Level1_ZY": MainMenu.levelAbhinav = true; break;
			//case "Level1_AJ": MainMenu.levelAbhinav = true; break;
				//case 8: MainMenu.levelBen = true; break;
				//case 9: MainMenu.levelEvan = true; break;
			default: break;
			}
			
			Application.LoadLevel ("Scene_End_of_Level");
		}
		if(other.tag == "Enemy")
			Application.LoadLevel (Application.loadedLevel);
		
		if (rigidbody.velocity.y > -0.5f && rigidbody.velocity.y < 0.5f 
		    && 
		    ((other.gameObject.tag == "Platform" && this.gameObject.tag == "Player_G") || 
		 (other.gameObject.tag == "GravityTile") || 
		 (other.gameObject.GetComponent<MeshRenderer>().material.color == 
		 this.gameObject.GetComponent<MeshRenderer>().material.color && other.gameObject.tag == "Platform"))
		    && (timeSinceExit - Time.time) < -0.2f
		    && !freeze) {
			
			Physics.gravity = new Vector3 (0, 0, 0);
			freeze = true;
			Invoke ("Rotate_displace", 0.2f);
			rigidbody.velocity = new Vector3(0,0,0);
			Physics.gravity = new Vector3(0,0,0);
			Debug.Log ("collisionexit at " + Time.time);
			
			switch (direction) {
			case 1:{InvokeRepeating ("Rot_Z_Neg", 0.1f, 0.02f);break;}
			case 2:{InvokeRepeating ("Rot_X_Neg", 0.1f, 0.02f);break;}
			case 3:{InvokeRepeating ("Rot_Z_Pos", 0.1f, 0.02f);break;}
			case 4:{InvokeRepeating ("Rot_X_Pos", 0.1f, 0.02f);break;}
			default: break;
				Debug.Log ("triggerenter at " + Time.time);
				
			}
			freeze = true;
			flip.Play ();
		}

		if (other.gameObject.tag == "Gravity") {
			Destroy (other.gameObject);
			this.gameObject.tag = "Player_G";
			getitem.Play();
			bg.Stop();
			power.Play();
			Invoke("removePower", 5.0f);
		}
		if (other.gameObject.tag == "Item") {
			Destroy (other.gameObject);
		}
		if (other.gameObject.tag == "CoinExtraLife") {
			MainCamera.numLives++;
			Destroy (other.gameObject);
			getitem.Play();
		}
		if (other.gameObject.tag == "CoinExtraScore") {
			MainCamera.gameScore += 100;
			Destroy (other.gameObject);
			getitem.Play();
		}
	}
	void removePower() {
		gameObject.tag = "Player";
		bg.Play();
		power.Stop();
	}
	void OnCollisionEnter(Collision other)
	{
		timeSinceEnter = Time.time;

		if (other.gameObject.tag == "Finish") {
			//if (Application.loadedLevelName == "Scene_Tutorial") {
			//	MainMenu.levelGeorge = true;
			//}
			Debug.Log("Finished" + Application.loadedLevelName);
			switch(Application.loadedLevelName)//custom level starting at 5, might need modification if the build setting is changed
			{
			case "Scene_Tutorial": MainMenu.levelVinayak = true; break;
			case "Level1_VT": MainMenu.levelCG = true; break;
			case "Level1_CG": MainMenu.levelEvan = true; break;
			case "Level1_EP": MainMenu.levelGeorge = true; break;
			case "Level1_ZY": MainMenu.levelAbhinav = true; break;;
			//case 8: MainMenu.levelBen = true; break;
			//case 9: MainMenu.levelEvan = true; break;
			default: break;
			}

			Application.LoadLevel ("Scene_End_of_Level");

		}
		
		if (other.gameObject.tag == "Enemy")
			Application.LoadLevel (Application.loadedLevel);
		
		if (other.gameObject.tag == "Gravity") {
			Destroy (other.gameObject);
			getitem.Play();
			bg.Stop();
			power.Play();
			Invoke("removePower", 5.0f);
			this.gameObject.tag = "Player_G";
		}
		if (other.gameObject.tag == "Item") {
			Destroy (other.gameObject);
		}
		if (other.gameObject.tag == "CoinExtraLife") {
			MainCamera.numLives++;
			Destroy (other.gameObject);
			getitem.Play();
		}
		if (other.gameObject.tag == "CoinExtraScore") {
			MainCamera.gameScore += 100;
			Destroy (other.gameObject);
			getitem.Play();
		}
	}
	
	void DoWindow1(int windowID) {
		GUI.Label (new Rect (10, 15, 150, 25), "Use up arrow to move");
		GUI.Label (new Rect (10, 30, 250, 25), "Right/Left arrows to change directions");
		GUI.Label (new Rect (10, 45, 150, 25), "Space to jump");
	}
	
	void DoWindow2(int windowID) {
		GUI.Label (new Rect (10, 15, 250, 25), "Pink Blocks let you switch gravity");
		GUI.Label (new Rect (10, 30, 150, 25), "Try walking around it");
	}
	
	void DoWindow3(int windowID) {
		GUI.Label (new Rect (10, 15, 250, 25), "Red Gems let you switch gravity");
		GUI.Label (new Rect (10, 30, 250, 25), "on any block");
		GUI.Label (new Rect (10, 45, 200, 25), "But only for a few seconds");
	}
	
	void DoWindow4(int windowID) {
		GUI.Label (new Rect (10, 15, 250, 25), "Aim of each puzzle is to get to");
		GUI.Label (new Rect (10, 30, 250, 25), "the white tile");
	}
	
	void DoWindow5(int windowID) {
		GUI.Label (new Rect (10, 15, 250, 25), "But before you finish");
		GUI.Label (new Rect (10, 30, 250, 25), "Find the hidden item");
		GUI.Label (new Rect (10, 45, 250, 25), "Hint: It could be under you");
	}
	
	void DoWindow6(int windowID) {
		GUI.Label (new Rect (10, 15, 250, 25), "Watch out for the enemy ahead!");
	}
	void DoWindow7(int windowID) {
		GUI.Label (new Rect (10, 15, 250, 25), "Try and change your color");
		GUI.Label (new Rect (10, 30, 250, 25), " to match the platform.");
		GUI.Label (new Rect (10, 45, 250, 25), "Matching colors allow you to");
		GUI.Label (new Rect (10, 60, 200, 40), "switch gravity around the platforms.");

	}
	
	void OnGUI() {
		GUI.skin = skin;
		float xpos = Screen.width;
		if (Application.loadedLevelName == "Scene_Tutorial") {
			if (tutOne)
				GUI.Window (0, new Rect (110, 10, 250, 80), DoWindow1, "Moving");
			if (tutTwo) 
				GUI.Window (0, new Rect (110, 10, 250, 60), DoWindow2, "Gravity");
			if (tutThree) 
				GUI.Window (0, new Rect (110, 10, 220, 80), DoWindow3, "Powerups");
			if (tutFour) 
				GUI.Window (0, new Rect (110, 10, 220, 60), DoWindow4, "Mission");
			if (tutFive) 
				GUI.Window (0, new Rect (110, 10, 200, 80), DoWindow5, "Hidden Items");
			if (tutSix) 
				GUI.Window(0, new Rect(110, 10, 220, 50), DoWindow6, "Enemies");
		}
		else if (Application.loadedLevelName == "Level1_VT") {
			if(transform.position.x < -20 && transform.position.z > 0) 
				GUI.Window(0, new Rect(110, 10, 220, 120), DoWindow7, "Colors");
		}
		if (timerOn) {
			int orignalSize = GUI.skin.label.fontSize;
			Font orignalFont = GUI.skin.label.font;
			GUI.skin.label.fontSize = 50;
			GUI.skin.label.font = f;
			//GUI.Label (new Rect (xpos * 0.45f, 10, 200, 30), "TIME REMAINING");
			GUI.Label (new Rect (xpos * 0.45f, 30, 100, 100), gameTimer.ToString ());
			GUI.skin.label.fontSize = orignalSize;
			GUI.skin.label.font = orignalFont;

		}
	}

	void Countdown () {
		if (--gameTimer == 0) {
			CancelInvoke ("Countdown");
			Application.LoadLevel (Application.loadedLevel);
		}
	}
}
