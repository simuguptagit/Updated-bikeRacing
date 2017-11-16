using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class GameController : MonoBehaviour  {

	public float Vertical;
	public float Horizontal;
	
	public float VerticalMassShift;
	public float HorizontalMassShift;

	public bool ESPMode;

	public bool rearBrakeOn;
	public bool restartBike;
	public bool fullRestartBike; 

	public bool reverse;//for reverse speed
	public static bool GameCompleted,GameFailed;
	private GameObject LevelComplete, Levelfailed,NextButton;
	public GameObject Bike1,Bike2,Bike3;
	//public GameObject Level_01,Level_02,Level_03;
	public static float Bike_x;
	public static float Bike_y;
	public static float Bike_z;
	public static float Bike_Rotation_y;
	public static int BikeNo;
	public static int LevelNo;
	public static float score;
	public float levelTime,currentTime;
	private GameObject fuelEmpty;
	float fractionValue;
	UnityEngine.UI.Image image;

	void Start()
	{
		BikeNo = PlayerPrefs.GetInt ("ModelNo");
		Vector3 v = new Vector3 (Bike_x,Bike_y,Bike_z);
		score = 0;
		switch (LevelNo) {
		case 1:
			levelTime = 80;
			currentTime = levelTime;
			Instantiate (Resources.Load("level_01"));
			//GameObject.Find ("LEVELS/level_02").SetActive (false);
			break;
		case 2:
			levelTime = 90;
			currentTime = levelTime;
			Instantiate (Resources.Load("level_02"));
			//GameObject.Find ("LEVELS/level_01").SetActive (false);;
			break;
		case 3:
			levelTime = 100;
			currentTime = levelTime;
			Instantiate (Resources.Load("level_03"));
			break;
		case 4:
			levelTime = 100;
			currentTime = levelTime;
			Instantiate (Resources.Load("level_04"));
			break;
		case 5:
			levelTime = 110;
			currentTime = levelTime;
			Instantiate (Resources.Load("level_05"));
			break;
		case 6:
			levelTime = 120;
			currentTime = levelTime;
			Instantiate (Resources.Load("level_06"));
			break;
		case 7:
			levelTime = 110;
			currentTime = levelTime;
			break;
		case 8:
			levelTime = 110;
			currentTime = levelTime;
			break;
		case 9:
			levelTime = 120;
			currentTime = levelTime;
			break;
		case 10:
			levelTime = 120;
			currentTime = levelTime;
			break;
		case 11:
			levelTime = 120;
			currentTime = levelTime;
			break;
		case 12:
			levelTime = 130;
			currentTime = levelTime;
			break;
		case 13:
			levelTime = 130;
			currentTime = levelTime;
			break;
		case 14:
			levelTime = 130;
			currentTime = levelTime;
			break;
		case 15:
			levelTime = 130;
			currentTime = levelTime;
			break;
		}

		switch (BikeNo) {
		case 1:
			Bike2.SetActive (false);
			Bike3.SetActive (false);
			Bike1.transform.position = v;
			Bike1.transform.rotation = Quaternion.Euler (0,Bike_Rotation_y,0);
			break;
		case 2:
			Bike1.SetActive (false);
			Bike3.SetActive (false);
			Bike2.transform.position = v;
			Bike2.transform.rotation = Quaternion.Euler (0,Bike_Rotation_y,0);
			break;
		case 3:
			Bike2.SetActive (false);
			Bike1.SetActive (false);
			Bike3.transform.position = v;
			Bike3.transform.rotation = Quaternion.Euler (0,Bike_Rotation_y,0);
			break;
		}

		LevelComplete = GameObject.Find ("LevelComplete");
		Levelfailed = GameObject.Find ("Levelfailed");
		NextButton=GameObject.Find ("LevelComplete/next");
		fuelEmpty = GameObject.Find ("Canvas/fuelbase/fuelempty");
		image = fuelEmpty.GetComponent<UnityEngine.UI.Image>();
		LevelComplete.SetActive (false);
		Levelfailed.SetActive (false);
		fractionValue = 1 / levelTime;

	}


	IEnumerator timer()
	{
		yield return new WaitForSeconds(1f);
		currentTime--;
	
		image.fillAmount =1- currentTime/levelTime;

		if (currentTime > 0) {
			StartCoroutine ("timer");
		} else {
			currentTime = 0;
			GameFailed = true;
			CallfromInGame ();
			StopCoroutine ("timer");
		}
	}

	public  void startTimer()
	{
		
		StartCoroutine ("timer");
	}

	public void CallfromInGame()
	{

		if (GameCompleted) {
			score = 1000;
			int i = LevelNo - 1;
			if (PlayerPrefs.GetFloat ("i") <score)
		      PlayerPrefs.SetFloat ("i", score);
			LevelComplete.SetActive (true);
			if(PlayerPrefs.GetInt ("Level_Selection") ==LevelNo)
				PlayerPrefs.SetInt ("Level_Selection",PlayerPrefs.GetInt ("Level_Selection")+1);
			//if (LevelNo == 3) {
			//	NextButton.SetActive (false);
			//}

		} else if (GameFailed) {
			Levelfailed.SetActive (true);

		}
	}

}