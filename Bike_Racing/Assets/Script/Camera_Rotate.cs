using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_Rotate : MonoBehaviour {
	float arroeMouseSpeed = .5f;
	public static bool Scroll_stop;
	// Use this for initialization
	void Start () {
		Vector3 rot = transform.localRotation.eulerAngles;
		rotY = rot.y;
		rotX = rot.x;
		//touchIson = false;
	}
	
	// Update is called once per frame
	void Update () {
	//	if (Input.GetMouseButtonDown (0))
	//	//	touchIson = true;
	//	if(touchIson)
	//	MouseMovement ();
	
	//	if (Input.GetMouseButtonUp (0))
	//		touchIson = false;

		if (Input.touches.Length <= 0) {
		} else {
			for (int i = 0; i < Input.touchCount; i++) {
				if (Input.GetTouch (i).phase == TouchPhase.Moved && Scroll_stop ==false) {
					MouseMovement ();
				}
			}
		}

	}

	void MouseMovement(){
		moveCamera ( Input.GetTouch (0).deltaPosition.x, Input.GetTouch (0).deltaPosition.y, arroeMouseSpeed);;
	}

	float mouseX;
	float mouseY;
	Quaternion localRotation;
	private float rotY = 0f;
	private float rotX = 0f;

	void moveCamera(float horizontal, float verticle , float movespeed){
		mouseX = horizontal;
		mouseY = verticle;
		rotY += mouseX * movespeed;
		//rotX += mouseY * movespeed;

		localRotation = Quaternion.Euler (rotX , rotY,0f);
		transform.rotation = localRotation;
	}
}
