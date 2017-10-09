using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_movement : MonoBehaviour {
	
	public float speed = 10f;

	private float pitch = 0.0f,
		yaw = 0.0f;
 
	void OnTouchMovedAnywhere(){

		//pitch -= Input.GetTouch (0).deltaPosition.y * speed * Time.deltaTime;
		yaw += Input.GetTouch (0).deltaPosition.x * speed * Time.deltaTime;

		this.transform.eulerAngles = new Vector3 (pitch, yaw,0.0f);
	}

	void Update () {

		if (Input.touches.Length <= 0) {
		} else {
			for (int i = 0; i < Input.touchCount; i++) {
				if (Input.GetTouch (i).phase == TouchPhase.Moved && Camera_Rotate.Scroll_stop == false) {
					OnTouchMovedAnywhere ();
				}
			}
		}
	//	Debug.Log ("testing touch,,,,"+Input.touches.Length +"  "+Input.touchCount);
	}
}
