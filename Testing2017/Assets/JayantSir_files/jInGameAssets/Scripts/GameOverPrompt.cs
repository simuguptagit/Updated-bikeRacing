using UnityEngine;
using System.Collections;

public class GameOverPrompt : MonoBehaviour {

	// Use this for initialization
	public void OnReplayClick()
	{
		Debug.Log ("onreplayclick");
		GameController.GameCompleted = false;
		GameController.GameFailed = false;
		Application.LoadLevel ("Terrain_02");
	
	}
	public void OnMainMenuClick()
	{
		Debug.Log ("onmainmenuclick");
		GameController.GameCompleted = false;
		GameController.GameFailed = false;
		Application.LoadLevel ("bike_mainpage");

	}
	public void OnNextClick()
	{
		Debug.Log ("onnextclick");
		GameController.GameCompleted = false;
		GameController.GameFailed = false;
		switch (GameController.LevelNo) {

		case 1:
			
			GameController.LevelNo = 2;
			Application.LoadLevel ("Level2");
			break;
		case 2:

			GameController.LevelNo = 3;
			Application.LoadLevel ("Level3");
			break;
		case 3:

			GameController.LevelNo = 3;
			Application.LoadLevel ("Level3");
			break;

		}
		//Application.LoadLevel(
	}
	void Update()
	{
		if (Input.GetKeyDown (KeyCode.Escape)) {
			GameController.GameCompleted = false;
			GameController.GameFailed = false;
			Application.LoadLevel ("bike_mainpage");
		}
	}
}
