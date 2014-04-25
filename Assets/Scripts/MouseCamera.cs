using UnityEngine;
using System.Collections;

public class MouseCamera : MonoBehaviour {
	public GameObject target;
	public float rotateSpeed = 5;
	Vector3 offset;
	public bool RightButtonHold = false;
	public int CamLevel = 0;
	Quaternion PrevRotation;
	Quaternion ThisPrevRotation;
	Vector3 PrevPosition;
	Vector3 ThisPrevPosition;
		
	void Start() {
		offset = target.transform.position - transform.position;
	}
	
	void Update() {
		float horizontal = Input.GetAxis("Mouse X") * rotateSpeed;
		float vertical   = Input.GetAxis("Mouse Y") * rotateSpeed;
		float desiredAngle = target.transform.eulerAngles.y;
		Quaternion rotation = Quaternion.Euler(0, desiredAngle-90, 0);

		if (Input.GetMouseButton (1)|| Input.GetKey(KeyCode.LeftShift)) {
			target.rigidbody.velocity = new Vector3(0,0,0);
			Character.freeze = true;
			if(!RightButtonHold)
			{
				PrevPosition = target.transform.position;
				PrevRotation = target.transform.rotation;
				ThisPrevPosition = transform.position;
				ThisPrevRotation = transform.rotation;
			}
			target.transform.Rotate (0, horizontal, 0);
			target.transform.Rotate (-vertical, 0, 0);
			//transform.position = target.transform.position - (rotation * offset);

			transform.LookAt (target.transform);
			RightButtonHold = true;
		} else if (RightButtonHold) {
			target.transform.rotation = PrevRotation;
			transform.position = ThisPrevPosition;
			transform.rotation = ThisPrevRotation;
			RightButtonHold = false;
			Character.freeze = false;
		}

		Vector3 pos = this.transform.localPosition;
		if (Input.GetAxis("Mouse ScrollWheel") > 0) // forward
		{
			if(CamLevel>=-3){
				CamLevel--;
				pos.y*=0.8f;
				pos.z*=0.8f;
				transform.localPosition = pos;
			}
		}
		if (Input.GetAxis("Mouse ScrollWheel") < 0) // back
		{
			if(CamLevel<=3){
				CamLevel++;
				pos.y*=1.25f;
				pos.z*=1.25f;
				transform.localPosition = pos;}
		}

	}
}


