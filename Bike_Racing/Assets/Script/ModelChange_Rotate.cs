using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModelChange_Rotate : MonoBehaviour {
	public Camera cam;
	// Use this for initialization
	void Start () {
	}

	// Update is called once per frame
	void Update () {
		if(Camera_Rotate.Scroll_stop == false)
		transform.Rotate (0,5*Time.deltaTime,0);
	}
}
