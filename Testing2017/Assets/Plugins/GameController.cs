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
	private GameObject LevelComplete, Levelfailed,NextButton,failNextButton;
	public GameObject Bike1,Bike2,Bike3;
	public static float Bike_x;
	public static float Bike_y;
	public static float Bike_z;
	public static float Bike_Rotation_y;
	public static int BikeNo;
	public static int LevelNo;
	public static int score;
	public static float levelTime,currentTime;
	private GameObject fuelEmpty;
	private GameObject iceParticle;
	float fractionValue;
	UnityEngine.UI.Image image;
	UnityEngine.UI.Image fuelEmptyBottle;

	void Start()
	{//Debug.Log ("gamecontroller..."+PlayerPrefs.GetInt ("ModelNo")+"   "+BikeNo+"  "+LevelNo);
		
		BikeNo = PlayerPrefs.GetInt ("ModelNo");
		Vector3 v = new Vector3 (Bike_x,Bike_y,Bike_z);
		score = 0;
		switch (LevelNo) {
		case 1:
			levelTime = 40;
			currentTime = levelTime;
			Instantiate (Resources.Load("level_01"));
			break;
		case 2:
			levelTime = 60;
			currentTime = levelTime;
			Instantiate (Resources.Load("level_02"));
			break;
		case 3:
			levelTime = 50;
			currentTime = levelTime;
			Instantiate (Resources.Load("level_03"));
			break;
		case 4:
			levelTime = 60;
			currentTime = levelTime;
			Instantiate (Resources.Load("level_04"));
			break;
		case 5:
			levelTime = 65;
			currentTime = levelTime;
			Instantiate (Resources.Load("level_05"));
			break;
		case 6:
			levelTime = 65;
			currentTime = levelTime;
			Instantiate (Resources.Load("level_06"));
			break;
		case 7:
			levelTime = 50;
			currentTime = levelTime;
			Instantiate (Resources.Load("level_07"));
			break;
		case 8:
			levelTime = 60;
			currentTime = levelTime;
			Instantiate (Resources.Load("level_08"));
			break;
		case 9:
			levelTime = 48;
			currentTime = levelTime;
			Instantiate (Resources.Load("level_09"));
			break;
		case 10:
			levelTime = 50;
			currentTime = levelTime;
			Instantiate (Resources.Load("level_10"));
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
		failNextButton=GameObject.Find ("Levelfailed/Next");
		fuelEmpty = GameObject.Find ("Canvas/fuelbase/fuelempty");
		image = fuelEmpty.GetComponent<UnityEngine.UI.Image>();
		fuelEmptyBottle = GameObject.Find ("Canvas/Nitros/emptyFuel2").GetComponent<UnityEngine.UI.Image>();
		LevelComplete.SetActive (false);
		Levelfailed.SetActive (false);
		fractionValue = 1 / levelTime;
		iceParticle = GameObject.Find("Particle_Ice");
		if (LevelNo <= 3)
			iceParticle.SetActive (false);
	    GameObject.Find ("Canvas/Nitros/fullFuel").GetComponent<Animator> ().enabled = false;
	}


	IEnumerator timer()
	{
		yield return new WaitForSeconds(1f);
		currentTime--;
		image.fillAmount =1- currentTime/levelTime;
		fuelEmptyBottle.fillAmount = 1- currentTime/levelTime;
		if (currentTime > 0) {
			StartCoroutine ("timer");
			if(GameObject.Find ("Canvas/Nitros/fullFuel").GetComponent<Animator> ().isActiveAndEnabled)
			   GameObject.Find ("Canvas/Nitros/fullFuel").GetComponent<Animator> ().enabled = false;
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
			if (i == 0) {
				if (PlayerPrefs.GetInt("0") <score)
					PlayerPrefs.SetInt("0", score);
			}
			else if (i == 1) {
				if (PlayerPrefs.GetInt("1") <score)
					PlayerPrefs.SetInt("1", score);
			}else if (i == 2) {
				if (PlayerPrefs.GetInt("2") <score)
					PlayerPrefs.SetInt("2", score);
			}else if (i == 3) {
				if (PlayerPrefs.GetInt("3") <score)
					PlayerPrefs.SetInt("3", score);
			}
			else if (i == 4) {
				if (PlayerPrefs.GetInt("4") <score)
					PlayerPrefs.SetInt("4", score);
			}
			else if (i == 5) {
				if (PlayerPrefs.GetInt("5") <score)
					PlayerPrefs.SetInt("5", score);
			}
			else if (i == 6) {
				if (PlayerPrefs.GetInt("6") <score)
					PlayerPrefs.SetInt("6", score);
			}
			else if (i == 7) {
				if (PlayerPrefs.GetInt("7") <score)
					PlayerPrefs.SetInt("7", score);
			}
			else if (i == 8) {
				if (PlayerPrefs.GetInt("8") <score)
					PlayerPrefs.SetInt("8", score);
			}
			else if (i == 9) {
				if (PlayerPrefs.GetInt("9") <score)
					PlayerPrefs.SetInt("9", score);
			}

			LevelComplete.SetActive (true);
			if(PlayerPrefs.GetInt ("Level_Selection") ==LevelNo)
				PlayerPrefs.SetInt ("Level_Selection",PlayerPrefs.GetInt ("Level_Selection")+1);
			if (LevelNo == 10) {
				NextButton.SetActive (false);
			}

		} else if (GameFailed) {
			if(PlayerPrefs.GetInt ("Level_Selection")<=LevelNo)
				failNextButton.SetActive (false);
			Levelfailed.SetActive (true);

		}
	}

}