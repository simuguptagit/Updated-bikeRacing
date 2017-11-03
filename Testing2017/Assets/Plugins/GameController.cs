using UnityEngine;
using System.Collections;





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
	public static int BikeNo;
	public static int LevelNo;
	public float levelTime,currentTime;
	private GameObject fuelEmpty;
	float fractionValue;
	UnityEngine.UI.Image image;

	void Start()
	{
		BikeNo = PlayerPrefs.GetInt ("ModelNo");
		//Debug.Log ("bike no..."+BikeNo+"   "+PlayerPrefs.GetInt ("ModelNo"));
		switch (BikeNo) {
		case 1:
			Bike2.SetActive (false);
			Bike3.SetActive (false);
			break;
		case 2:
			Bike1.SetActive (false);
			Bike3.SetActive (false);
			break;
		case 3:
			Bike2.SetActive (false);
			Bike1.SetActive (false);
			break;
		}
		switch (LevelNo) {
		case 1:
			levelTime = 80;
			currentTime = levelTime;
			break;
		case 2:
			levelTime = 90;
			currentTime = levelTime;
			break;
		case 3:
			levelTime = 100;
			currentTime = levelTime;
			break;
		case 4:
			levelTime = 100;
			currentTime = levelTime;
			break;
		case 5:
			levelTime = 110;
			currentTime = levelTime;
			break;
		case 6:
			levelTime = 120;
			currentTime = levelTime;
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
			
			LevelComplete.SetActive (true);
			if (LevelNo == 3) {
				NextButton.SetActive (false);
			}

		} else if (GameFailed) {
			Levelfailed.SetActive (true);

		}
	}

}