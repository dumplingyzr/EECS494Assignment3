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
	public float angle = 0;
	public bool freeze = false;
	// Use this for initialization

	void Start () {
		Physics.gravity = new Vector3 (0, -100, 0);
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 vel = rigidbody.velocity;
		if (!freeze) {
			if (Input.GetKeyDown (KeyCode.RightArrow) ||
			    Input.GetKeyDown (KeyCode.D))
			{
				transform.Rotate(new Vector3(0,1,0),90);
				direction ++;
				if(direction == 5)
					direction = 1;
			}
			else if (Input.GetKeyDown (KeyCode.LeftArrow) ||
			    Input.GetKeyDown (KeyCode.A))
			{
				transform.Rotate(new Vector3(0,1,0),-90);
				direction --;
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
		rigidbody.velocity = vel;
		if (rigidbody.velocity.y < -1)
						Time.timeScale = 0.8f;
				else
						Time.timeScale = 1.0f;
		if (vel.y < -100)
			Application.LoadLevel ("Scene0");

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

	void Rot_Z_Pos(){
		if (angle < 90) {
			platform1.RotateAround (transform.position, z_axis, 10);
			//platform2.RotateAround (transform.position, z_axis, 10);
			angle += 10;
		} else {
			CancelInvoke ("Rot_Z_Pos");
			Physics.gravity = new Vector3 (0, -100, 0);
			angle = 0;
			freeze = false;
		}
	}

	void Rot_Z_Neg(){
		if (angle <90) {
			platform1.RotateAround (transform.position, z_axis, -10);
			//platform2.RotateAround (transform.position, z_axis, -10);
			Physics.gravity = new Vector3 (0, -100, 0);
			angle += 10;
		} else {
			CancelInvoke ("Rot_Z_Neg");
			//Physics.gravity = new Vector3 (0, -100, 0);
			angle = 0;
			freeze = false;
		}
	}

	void Rot_X_Pos(){
		if (angle < 90) {
			platform1.RotateAround (transform.position, x_axis, 10);
			//platform2.RotateAround (transform.position, x_axis, 10);
			angle += 10;
		} else {
			CancelInvoke ("Rot_X_Pos");
			Physics.gravity = new Vector3 (0, -100, 0);
			angle = 0;
			freeze = false;
		}
	}
	
	void Rot_X_Neg(){
		if (angle <90) {
			platform1.RotateAround (transform.position, x_axis, -10);
			//platform2.RotateAround (transform.position, x_axis, -10);
			angle += 10;
			Physics.gravity = new Vector3 (0, -100, 0);
		} else {
			CancelInvoke ("Rot_X_Neg");
			//Physics.gravity = new Vector3 (0, -100, 0);
			angle = 0;
			freeze = false;
		}
	}

	void OnCollisionExit(Collision other)
	{
		if (rigidbody.velocity.y < -1) {
			Physics.gravity = new Vector3 (0, 0, 0);
			switch (direction) {
			case 1:{InvokeRepeating ("Rot_Z_Pos", 0.2f, 0.02f);break;}
			case 2:{InvokeRepeating ("Rot_X_Pos", 0.2f, 0.02f);break;}
			case 3:{InvokeRepeating ("Rot_Z_Neg", 0.2f, 0.02f);break;}
			case 4:{InvokeRepeating ("Rot_X_Neg", 0.2f, 0.02f);break;}
			default: break;
			}
			freeze = true;
		}
	}

	void OnTriggerEnter(Collider other)
	{
		if(other.tag == "Finish" || other.tag == "Enemy")
			Application.LoadLevel ("Scene0");

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
		}
	}

}
