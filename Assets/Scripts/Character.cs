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
	public bool freeze = false;

	public static int level;
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

	public GameObject enemy;
	
	// Use this for initialization

	void Start () {
		Physics.gravity = new Vector3 (0, -100, 0);
		level = Application.loadedLevel;
		enemy = GameObject.FindGameObjectWithTag ("Enemy");
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
				vel.x = 0;
				vel.z = 0;
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
		if (curX > -200 && !tutOneDone) {
			tutOne = false;
			tutOneDone = true;
		}
		if (curX > -125 && !tutTwoDone) {
			tutTwo = true;
			tutTwoDone = true;
		}
		if (curX > -80) {
			tutTwo = false;
		}
		if (curX > -40 && !tutThreeDone) {
			tutThree = true;
			tutThreeDone = true;
		}
		if (curX > -25) {
			tutThree = false;
		}
		if (curX > -15 && !tutFourDone) {
			tutFour = true;
			tutFourDone = true;
		}
		if (curX > 15) {
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
		if (vel.y < -200)
			Application.LoadLevel (Application.loadedLevel);

	}

	Vector3 Move(Vector3 vel){
		switch (direction) {
		case 1: //facing forward
			vel.x = speed;
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
			enemy.transform.Rotate(0,0,90);
			Enemy.enable = true;
			//Physics.gravity = new Vector3 (0, -100, 0);
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
			enemy.transform.Rotate(0,0,-90);
			Enemy.enable = true;
			//Physics.gravity = new Vector3 (0, -100, 0);
			angle = 0;
			//freeze = false;
		}
	}

	void Rot_X_Pos(){
		if (angle < 90) {
			platform1.RotateAround (transform.position, x_axis, 10);
			//platform2.RotateAround (transform.position, x_axis, 10);
			angle += 10;
			//Rotate_displace();

		} else {
			CancelInvoke ("Rot_X_Pos");
			//Physics.gravity = new Vector3 (0, -100, 0);
			angle = 0;
			//freeze = false;
		}
	}
	
	void Rot_X_Neg(){
		if (angle <90) {
			platform1.RotateAround (transform.position, x_axis, -10);
			//platform2.RotateAround (transform.position, x_axis, -10);
			angle += 10;
			//Physics.gravity = new Vector3 (0, -100, 0);
			//Rotate_displace();

		} else {
			CancelInvoke ("Rot_X_Neg");
			//Physics.gravity = new Vector3 (0, -100, 0);
			angle = 0;
			//freeze = false;
		}
	}
	
	void OnCollisionStay(Collision other)
	{
		if (jumping && other.gameObject.tag == "Platform") {
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

		Physics.gravity = new Vector3 (0, -100, 0);

		Invoke ("Unfreeze", 0.2f);
	}
	void OnCollisionExit(Collision other)
	{
		if (rigidbody.velocity.y < -1 
		    && 
		    ((other.gameObject.tag == "Platform" && this.gameObject.tag == "Player_G") || (other.gameObject.tag == "GravityTile"))
		    && (timeSinceEnter - Time.time) < -0.5f
		    && !freeze) 
		{
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
		}

		timeSinceExit = Time.time;
	}

	void OnTriggerEnter(Collider other)
	{
		if(other.tag == "Finish")
			Application.LoadLevel ("Scene_End_of_Level");
		if(other.tag == "Enemy")
			Application.LoadLevel (Application.loadedLevel);

		if (rigidbody.velocity.y > -0.1f && rigidbody.velocity.y < 0.1f 
		    && 
		    ((other.gameObject.tag == "Platform" && this.gameObject.tag == "Player_G") || (other.gameObject.tag == "GravityTile"))
		    && (timeSinceExit - Time.time) < -0.5f
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
		}
		timeSinceEnter = Time.time;

		if (other.gameObject.tag == "Gravity") {
			Destroy (other.gameObject);
			this.gameObject.tag = "Player_G";
		}
		if (other.gameObject.tag == "Item") {
			Destroy (other.gameObject);
		}
		if (other.gameObject.tag == "CoinExtraLife") {
			MainCamera.numLives++;
			Destroy (other.gameObject);
		}
		if (other.gameObject.tag == "CoinExtraScore") {
			MainCamera.gameScore += 100;
			Destroy (other.gameObject);
		}
	}

	void OnCollisionEnter(Collision other)
	{
		if (other.gameObject.tag == "Gravity") {
			Destroy (other.gameObject);
			this.gameObject.tag = "Player_G";
		}
		if (other.gameObject.tag == "Item") {
			Destroy (other.gameObject);
		}
		if (other.gameObject.tag == "CoinExtraLife") {
			MainCamera.numLives++;
			Destroy (other.gameObject);
		}
		if (other.gameObject.tag == "CoinExtraScore") {
			MainCamera.gameScore += 100;
			Destroy (other.gameObject);
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

	void OnGUI() {
		if (tutOne)
			GUI.Window(0, new Rect(110, 10, 250, 80), DoWindow1, "Moving");
		if (tutTwo) 
			GUI.Window(0, new Rect(110, 10, 250, 60), DoWindow2, "Gravity");
		if (tutThree) 
			GUI.Window(0, new Rect(110, 10, 220, 80), DoWindow3, "Powerups");
		if (tutFour) 
			GUI.Window(0, new Rect(110, 10, 220, 60), DoWindow4, "Mission");
		if (tutFive) 
			GUI.Window(0, new Rect(110, 10, 200, 80), DoWindow5, "Hidden Items");
		if (tutSix) 
			GUI.Window(0, new Rect(110, 10, 220, 50), DoWindow6, "Enemies");
	}
}
