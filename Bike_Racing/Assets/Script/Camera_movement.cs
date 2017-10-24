using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_movement : MonoBehaviour {
	public float speed = 15f;

	private float pitch = 0.0f,
		yaw = 0.0f;
 
	void OnTouchMovedAnywhere(){

		yaw += Input.GetTouch (0).deltaPosition.y * speed * Time.deltaTime;
		Quaternion localRotation = Quaternion.Euler (yaw , pitch,0f);
		transform.rotation = localRotation;
		//this.transform.eulerAngles = new Vector3 (yaw, pitch,0.0f);
	}

	void Update () {
		transform.Rotate (0,6*Time.deltaTime,0);
		if (Input.touches.Length <= 0) {
		} else {
			for (int i = 0; i < Input.touchCount; i++) {
				if (Input.GetTouch (i).phase == TouchPhase.Moved && Camera_Rotate.Scroll_stop == false) {
					//OnTouchMovedAnywhere ();
				}
			}
		}
	}


}
