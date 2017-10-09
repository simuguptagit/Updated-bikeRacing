using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using GooglePlayGames;
using UnityEngine.SocialPlatforms;

public class All_Buttom_button_touch : MonoBehaviour {
	public GameObject Forword;
	public GameObject Back;
	public GameObject Select;
	public GameObject Buttom_panel;

	public GameObject Levels;

	// Use this for initialization
	void Start () {
		PlayGamesPlatform.Activate ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	public void Login(){
		Social.localUser.Authenticate((bool success) => {
			if(success)
			Debug.Log("You are successfully Login!!");
			else
				Debug.Log("You are failed Login!!");
		});
	}
	public void Play(){
		Forword.SetActive (true);
		Back.SetActive (true);
		Select.SetActive (true);
	}

	public void select(){
		Camera_Rotate.Scroll_stop = true;

		Buttom_panel.SetActive (false);
		Levels.SetActive (true);
		Forword.SetActive (false);
		Back.SetActive (false);
		Select.SetActive (false);
	}
}
