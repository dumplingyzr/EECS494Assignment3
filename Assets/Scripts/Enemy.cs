using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {
	public float radius;
	public float speed;
	public float angular_speed;

	public static bool enable = true;

	Vector3 br_direction;
	float br_length;
	Vector3 tr_direction;
	float tr_length;
	Vector3 origin;
	Quaternion r;

	// Update is called once per frame
	void Update () {
		if(rigidbody.velocity.y < -100f)
			GameObject.Destroy(gameObject);

		r = this.gameObject.transform.rotation;

		//Calculate forward origin of raycasts
		origin = Vector3.forward * radius;
		origin = r * origin;
		origin = transform.position + origin;

		//Calculate direction and length of bottom raycast
		br_direction = Vector3.forward * speed * Time.deltaTime + Vector3.down * radius;
		br_direction = r * br_direction;
		br_length = br_direction.magnitude * 1.1f;

		//Calculate direction and length of top raycast
		tr_direction = Vector3.forward * speed * Time.deltaTime;
		tr_direction = r * tr_direction;
		tr_length = tr_direction.magnitude * 1.1f;

		if(enable)
			Navigate ();
	}

	void MoveForward () {
		gameObject.rigidbody.MovePosition(gameObject.transform.position + tr_direction);
	}

	void Rotate180 () {
		gameObject.rigidbody.MoveRotation(Quaternion.AngleAxis(90, transform.up) * r);
	}

	void RotateRight () {
		gameObject.rigidbody.MoveRotation (Quaternion.AngleAxis (angular_speed * Time.deltaTime, transform.up) * r);
	}

	void RotateLeft () {
		gameObject.rigidbody.MoveRotation (Quaternion.AngleAxis (angular_speed * Time.deltaTime, Vector3.down) * r);
	}

	bool IfDropForward () {
		return !Physics.Raycast (origin, br_direction, br_length);
	}

	bool IfWallForward () {
		return Physics.Raycast (origin, tr_direction, tr_length);
	}

	void Navigate () {
		if (IfDropForward () || IfWallForward ()) {
			Rotate180 ();
		} else {
			MoveForward ();
		}
	}
}
