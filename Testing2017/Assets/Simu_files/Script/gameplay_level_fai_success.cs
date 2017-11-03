using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gameplay_level_fai_success : MonoBehaviour {

	public GameObject gameplay_fail;
	public GameObject gameplay_succcess;

	// Use this for initialization
	void Start () {
		
	}
	public void Gameplay_home(){
		gameplay_fail.SetActive (false);
		gameplay_succcess.SetActive (false);
	}
	public void Gameplay_fail(){
		gameplay_fail.SetActive (true);
	}
	public void Gameplay_success(){
		gameplay_succcess.SetActive (true);
	}
	// Update is called once per frame
	void Update () {
		
	}
}
