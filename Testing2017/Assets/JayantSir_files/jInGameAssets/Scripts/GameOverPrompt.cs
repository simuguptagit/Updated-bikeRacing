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
		GameController.LevelNo = GameController.LevelNo+1;
		Application.LoadLevel ("Terrain_02");
		gameNextLevel_Assign (GameController.LevelNo);
	}
	public void gameNextLevel_Assign(int lvlno){
		switch (lvlno) {
		case 1:
			Level_Play_assign (1,15.93f,1.17f,179.392f,168.0995f);
			break;
		case 2:
			Level_Play_assign (2,219.536f,1.17f,109.291f, -20f);
			break;
		case 3:
			Level_Play_assign (3,189.585f,1.17f,95.8024f, -18.324f);
			break;
		case 4:
			Level_Play_assign (4,81.93f,1.17f,69.5f, 0f);
			break;
		case 5:
			Level_Play_assign (5,114.033f,1.17f,105.391f, -16.0945f);
			break;
		case 6:
			Level_Play_assign (6,33.01f,1.17f,62.39f,0f);
			break;
		case 7:
			Level_Play_assign (6,33.01f,1.17f,62.39f,0f);
			break;
		case 8:
			Level_Play_assign (6,33.01f,1.17f,62.39f,0f);
			break;
		case 9:
			Level_Play_assign (6,33.01f,1.17f,62.39f,0f);
			break;
		case 10:
			Level_Play_assign (6,33.01f,1.17f,62.39f,0f);
			break;
		}
	}
	public void Level_Play_assign(int lvlno , float bikex , float bikey , float bikez , float bikeroty){
		GameController.LevelNo = lvlno;
		GameController.Bike_x = bikex;
		GameController.Bike_y = bikey;
		GameController.Bike_z = bikez;
		GameController.Bike_Rotation_y = bikeroty;
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
