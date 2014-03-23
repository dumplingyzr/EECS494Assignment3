using UnityEngine;
using System.Collections;

public class Character : MonoBehaviour {
	public float speed = 15;
	public int direction = 1;
	public Transform platform1;
	public Transform platform2;
	public Vector3 point = new Vector3 (25,0,0);
	public Vector3 axis = new Vector3 (0,0,1);
	public float angle = 0;
	public bool freeze = false;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 vel = rigidbody.velocity;

		if (!freeze) {
				if (Input.GetKey (KeyCode.LeftArrow) ||
						Input.GetKey (KeyCode.A))
						vel.x = speed;
				else if (Input.GetKey (KeyCode.RightArrow) ||
						Input.GetKey (KeyCode.D))
						vel.x = -speed;
				else
						vel.x = 0;
		}
		rigidbody.velocity = vel;

	}

	void RotateClockWise(){
		if (angle < 90) {
			platform1.RotateAround (transform.position, axis, 10);
			platform2.RotateAround (transform.position, axis, 10);
			angle += 10;
		} else {
			CancelInvoke ("RotateClockWise");
			angle = 0;
			freeze = false;
		}
	}

	void RotateCounterClockWise(){
		if (angle <90) {
			platform1.RotateAround (transform.position, axis, -10);
			platform2.RotateAround (transform.position, axis, -10);
			angle += 10;
		} else {
			CancelInvoke ("RotateCounterClockWise");
			angle = 0;
			freeze = false;
		}
	}

	void OnCollisionExit(Collision other)
	{
		if (rigidbody.velocity.y < 0) {
			if (rigidbody.velocity.x > 0)
				InvokeRepeating ("RotateClockWise", 0, 0.1f);
			else if (rigidbody.velocity.x < 0)
				InvokeRepeating ("RotateCounterClockWise", 0, 0.1f);
			freeze = true;
		}
	}
}
