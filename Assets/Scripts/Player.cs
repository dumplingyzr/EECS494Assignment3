using UnityEngine;
using System.Collections;
using System.Linq;


public class Player : MonoBehaviour {
	public bool freeze;
	float speed = 50;
	float rotate_speed = 120;
	float gravity_scale = -4000;
	string gravity;
	// Use this for initialization
	void Start () {
		freeze = false;
		Physics.gravity = new Vector3 (0, gravity_scale, 0);
		gravity = "-y";
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 vel = rigidbody.velocity;
		if (!freeze) {
			if (Input.GetKey (KeyCode.RightArrow) ||
			    Input.GetKey (KeyCode.D))
			{
				transform.Rotate(Physics.gravity,rotate_speed*Time.deltaTime);

			}
			else if (Input.GetKey (KeyCode.LeftArrow) ||
			         Input.GetKey (KeyCode.A))
			{
				transform.Rotate(Physics.gravity,-rotate_speed*Time.deltaTime);

			}
			if (Input.GetKey (KeyCode.UpArrow) ||
			         Input.GetKey (KeyCode.W))
				vel = transform.forward * speed;
			else {
				vel = new Vector3(0,0,0);
			}
		}
		rigidbody.velocity = vel;

		if (rigidbody.velocity.y < -1)
			Time.timeScale = 0.8f;
		else
			Time.timeScale = 1.0f;
		//Time.fixedDeltaTime = 0.02F * Time.timeScale;

		if (vel.sqrMagnitude < -40)
			Application.LoadLevel ("Scene0");

		switchCheck();
	}

	void switchCheck() {
		if (Physics.Raycast(transform.position, -(transform.forward), 6))
		{
			Debug.Log("Hit!");

			float x = Mathf.Abs(transform.forward.x);
			float y = Mathf.Abs(transform.forward.y);
			float z = Mathf.Abs(transform.forward.z);

			float[] a = {x,y,z};

			if(a.Max() == z) {
				if(transform.forward.z >= 0) {
					gravity = "-z";
					//transform.Rotate(new Vector3(-1,0,0),90);
					//transform.Rotate(new Vector3(0,1,1),90);
				}
				else {
					gravity = "z";
					//transform.Rotate(new Vector3(-1,0,0),90);
				}
			}
			else if(a.Max() == y) {
				if(transform.forward.y >= 0) {
					//transform.Rotate(new Vector3(0,0,1),90);
					gravity = "-y";
				}
				else {
					//transform.Rotate(new Vector3(0,0,-1),90);
					gravity = "y";
				}
			}
			else if(a.Max() == x) {
				if(transform.forward.x >= 0) {
					//transform.Rotate(new Vector3(0,1,0),90);
					gravity = "-x";
				}
				else {
					gravity = "x";
					//transform.Rotate(new Vector3(0,-1,0),90);
				}
			}
			//transform.Rotate(-transform.right,90);

			updateGravity();
		}
	}

	void updateGravity() {
		Debug.Log ("updating gravity to " + gravity);
		switch(gravity) {
		case "-y":
			Physics.gravity = new Vector3 (0, gravity_scale, 0);
			break;
		case "y":
			Physics.gravity = new Vector3 (0, -gravity_scale, 0);
			break;
		case "-x":
			Physics.gravity = new Vector3 (gravity_scale, 0, 0);
			break;
		case "x":
			Physics.gravity = new Vector3 (-gravity_scale, 0, 0);
			break;
		case "-z":
			Physics.gravity = new Vector3 (0, 0, gravity_scale);
			break;
		case "z":
			Physics.gravity = new Vector3 (0, 0, -gravity_scale);
			break;
		}
	}
	void OnCollisionExit(Collision other)
	{
		/*if (rigidbody.velocity.y < -1) {
			Physics.gravity = new Vector3 (0, 0, 0);
			switch (direction) {
			case 1:{InvokeRepeating ("Rot_Z_Pos", 0.2f, 0.02f);break;}
			case 2:{InvokeRepeating ("Rot_X_Pos", 0.2f, 0.02f);break;}
			case 3:{InvokeRepeating ("Rot_Z_Neg", 0.2f, 0.02f);break;}
			case 4:{InvokeRepeating ("Rot_X_Neg", 0.2f, 0.02f);break;}
			default: break;
		}
		freeze = true;*/
	}

	void OnTriggerEnter(Collider other)
	{
		if(other.tag == "Finish" || other.tag == "Enemy")
			Application.LoadLevel ("Scene0");
		/*
		if (rigidbody.velocity.y > -0.1f && rigidbody.velocity.y < 0.1f) {
			Physics.gravity = new Vector3 (0, 0, 0);
			switch (direction) {
			case 1:{InvokeRepeating ("Rot_Z_Neg", 0.2f, 0.02f);break;}
			case 2:{InvokeRepeating ("Rot_X_Neg", 0.2f, 0.02f);break;}
			case 3:{InvokeRepeating ("Rot_Z_Pos", 0.2f, 0.02f);break;}
			case 4:{InvokeRepeating ("Rot_X_Pos", 0.2f, 0.02f);break;}
			default: break;
			}
			freeze = true;
		}*/
	}

}
