using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {
	public float radius;
	public float speed;
	public float angular_speed;

	Vector3 br_direction;
	float br_length;
	Vector3 tr_direction;
	float tr_length;
	Vector3 origin;
	Quaternion r;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		if(rigidbody.velocity.y < -100f)
			GameObject.Destroy(gameObject);

		//Calculate origin of raycasts
		r = this.gameObject.transform.rotation;
		origin = Vector3.forward * radius;
		origin = r * origin;
		origin = gameObject.transform.position + origin;

		//Calculate direction and length of bottom raycast
		br_direction = Vector3.forward * speed * Time.deltaTime + Vector3.down * radius;
		br_direction = r * br_direction;
		br_length = br_direction.magnitude * 1.1f;

		//Calculate direction and length of top raycast
		tr_direction = Vector3.forward * speed * Time.deltaTime;
		tr_direction = r * tr_direction;
		tr_length = tr_direction.magnitude * 1.1f;

		if (Physics.Raycast (origin, br_direction, br_length) && !Physics.Raycast (origin, tr_direction, tr_length)) {
			gameObject.rigidbody.MovePosition(gameObject.transform.position + tr_direction);
		} else {
			gameObject.rigidbody.MoveRotation(Quaternion.AngleAxis(180, Vector3.up) * r);
		}
	}
}
