using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Level_Selection : MonoBehaviour {
	/*public GameObject level_02;
	public GameObject lock_level_02;
	public GameObject level_03;
	public GameObject lock_level_03;
	public GameObject level_04;
	public GameObject lock_level_04;
	public GameObject level_05;
	public GameObject lock_level_05;
	public GameObject level_06;
	public GameObject lock_level_06;*/
	bool b;
	int forword_back_notification;
	int button_status_foe_back;
	public GameObject ScrollView_Pannel;
	public float cureenttime;
	// Use this for initialization
	void Start () {
		
		forword_back_notification = 0;
		button_status_foe_back = 0;

		if (PlayerPrefs.GetInt ("0") == 0)
			PlayerPrefs.SetInt ("0", 0);
		if (PlayerPrefs.GetInt ("1") == 0)
			PlayerPrefs.SetInt ("1", 0);
		if (PlayerPrefs.GetInt ("2") == 0)
			PlayerPrefs.SetInt ("2", 0);
		if (PlayerPrefs.GetInt ("3") == 0)
			PlayerPrefs.SetInt ("3", 0);
		if (PlayerPrefs.GetInt ("4") == 0)
			PlayerPrefs.SetInt ("4", 0);
		if (PlayerPrefs.GetInt ("5") == 0)
			PlayerPrefs.SetInt ("5", 0);
		if (PlayerPrefs.GetInt ("6") == 0)
			PlayerPrefs.SetInt ("6", 0);
		if (PlayerPrefs.GetInt ("7") == 0)
			PlayerPrefs.SetInt ("7", 0);
		if (PlayerPrefs.GetInt ("8") == 0)
			PlayerPrefs.SetInt ("8", 0);
		if (PlayerPrefs.GetInt ("9") == 0)
			PlayerPrefs.SetInt ("9", 0);
		
		if (PlayerPrefs.GetInt ("Level_Selection") == 0)
			PlayerPrefs.SetInt ("Level_Selection", 1);

		Level_button_text (0,GameObject.Find("Canvas/level_selection/Panel/Scrollview_Panel/level_01"));

		if (PlayerPrefs.GetInt ("Level_Selection") <= 1)
			b = true;
		else 
			b= false;
		Level_button_text (1,GameObject.Find("Canvas/level_selection/Panel/Scrollview_Panel/level_02"));
		button_active_inactive (GameObject.Find("Canvas/level_selection/Panel/Scrollview_Panel/level_02"),GameObject.Find("Canvas/level_selection/Panel/Scrollview_Panel/Lock_level_02"), b);

		if (PlayerPrefs.GetInt ("Level_Selection") <= 2)
			b = true;
		else 
			b= false;
		Level_button_text (2,GameObject.Find("Canvas/level_selection/Panel/Scrollview_Panel/level_03"));
		button_active_inactive (GameObject.Find("Canvas/level_selection/Panel/Scrollview_Panel/level_03"), GameObject.Find("Canvas/level_selection/Panel/Scrollview_Panel/Lock_level_03"), b);

		if (PlayerPrefs.GetInt ("Level_Selection") <= 3)
			b = true;
		else 
			b= false;
		Level_button_text (3,GameObject.Find("Canvas/level_selection/Panel/Scrollview_Panel/level_04"));
		button_active_inactive (GameObject.Find("Canvas/level_selection/Panel/Scrollview_Panel/level_04"), GameObject.Find("Canvas/level_selection/Panel/Scrollview_Panel/lock_level_04"), b);

		if (PlayerPrefs.GetInt ("Level_Selection") <= 4)
			b = true;
		else 
			b= false;
		Level_button_text (4,GameObject.Find("Canvas/level_selection/Panel/Scrollview_Panel/level_05"));
		button_active_inactive (GameObject.Find("Canvas/level_selection/Panel/Scrollview_Panel/level_05"), GameObject.Find("Canvas/level_selection/Panel/Scrollview_Panel/Lock_level_05"), b);

		if (PlayerPrefs.GetInt ("Level_Selection") <= 5)
			b = true;
		else 
			b= false;
		Level_button_text (5,GameObject.Find("Canvas/level_selection/Panel/Scrollview_Panel/level_06"));
		button_active_inactive (GameObject.Find("Canvas/level_selection/Panel/Scrollview_Panel/level_06"), GameObject.Find("Canvas/level_selection/Panel/Scrollview_Panel/Lock_level_06"), b);

		if (PlayerPrefs.GetInt ("Level_Selection") <= 6)
			b = true;
		else 
			b= false;
		Level_button_text (6,GameObject.Find("Canvas/level_selection/Panel/Scrollview_Panel/level_07"));
		button_active_inactive (GameObject.Find("Canvas/level_selection/Panel/Scrollview_Panel/level_07"), GameObject.Find("Canvas/level_selection/Panel/Scrollview_Panel/Lock_level_07"), b);

		if (PlayerPrefs.GetInt ("Level_Selection") <= 7)
			b = true;
		else 
			b= false;
		Level_button_text (7,GameObject.Find("Canvas/level_selection/Panel/Scrollview_Panel/level_08"));
		button_active_inactive (GameObject.Find("Canvas/level_selection/Panel/Scrollview_Panel/level_08"), GameObject.Find("Canvas/level_selection/Panel/Scrollview_Panel/Lock_level_08"), b);

		if (PlayerPrefs.GetInt ("Level_Selection") <=8)
			b = true;
		else 
			b= false;
		Level_button_text (8,GameObject.Find("Canvas/level_selection/Panel/Scrollview_Panel/level_09"));
		button_active_inactive (GameObject.Find("Canvas/level_selection/Panel/Scrollview_Panel/level_09"), GameObject.Find("Canvas/level_selection/Panel/Scrollview_Panel/Lock_level_09"), b);

		if (PlayerPrefs.GetInt ("Level_Selection") <= 9)
			b = true;
		else 
			b= false;
		Level_button_text (9,GameObject.Find("Canvas/level_selection/Panel/Scrollview_Panel/level_10"));
		button_active_inactive (GameObject.Find("Canvas/level_selection/Panel/Scrollview_Panel/level_10"), GameObject.Find("Canvas/level_selection/Panel/Scrollview_Panel/Lock_level_10"), b);

		GameObject.Find ("Canvas/level_selection/back").SetActive (false);
		GameObject.Find ("Canvas/level_selection").SetActive (false);

	}

	Text[] txt;string s;
	public void Level_button_text(int level,GameObject level_no){
		txt= level_no.GetComponentsInChildren<Text> ();
		if(level == 0)
			s= 	PlayerPrefs.GetInt ("0").ToString();
		if(level == 1)
			s= 	PlayerPrefs.GetInt ("1").ToString();
		if(level == 2)
			s= 	PlayerPrefs.GetInt ("2").ToString();
		if(level == 3)
			s= 	PlayerPrefs.GetInt ("3").ToString();
		if(level == 4)
			s= 	PlayerPrefs.GetInt ("4").ToString();
		if(level == 5)
			s= 	PlayerPrefs.GetInt ("5").ToString();
		if(level == 6)
			s= 	PlayerPrefs.GetInt ("6").ToString();
		if(level == 7)
			s= 	PlayerPrefs.GetInt ("7").ToString();
		if(level == 8)
			s= 	PlayerPrefs.GetInt ("8").ToString();
		if(level == 9)
			s= 	PlayerPrefs.GetInt ("9").ToString();

		txt [1].text = s;
	}
	public void button_active_inactive(GameObject level , GameObject lock_level , bool enable){
		if (enable) {
			level.SetActive (false);
			lock_level.SetActive (true);
		} else {
			level.SetActive (true);
			lock_level.SetActive (false);
		}
	}

	public void forword_button(){
		scrollstrt_pos = ScrollView_Pannel.transform.position;
		target_pos = scrollstrt_pos;
		target_pos.x -= 162; 
		button_status_foe_back++;
		forword_back_notification = 1;
	}
	public void Back_button(){
		button_status_foe_back--;
		scrollstrt_pos = ScrollView_Pannel.transform.position;
		target_pos = scrollstrt_pos;
		target_pos.x += 162; 
		forword_back_notification = 2;
	}
	Vector3 scrollstrt_pos;
	Vector3 target_pos;

	// Update is called once per frame
	void Update () {
		if(forword_back_notification>0){Debug.Log ("levelselecttttt..." +button_status_foe_back+"  "+forword_back_notification);
		if (button_status_foe_back == 0)
			GameObject.Find ("Canvas/level_selection/back").SetActive (false);
		else if (GameObject.Find ("Canvas/level_selection/back").activeSelf == false && button_status_foe_back > 0)
			GameObject.Find ("Canvas/level_selection/back").SetActive (true);
			
		if (button_status_foe_back ==6)
			GameObject.Find ("Canvas/level_selection/forword").SetActive (false);
		else if (button_status_foe_back >= 0 && button_status_foe_back < 6&& GameObject.Find ("Canvas/level_selection/forword").activeSelf == false)
			GameObject.Find ("Canvas/level_selection/forword").SetActive (true);
	}
		
		if (forword_back_notification == 1) {
			Vector3 v = ScrollView_Pannel.transform.position;

				v.x -= 4f;
				ScrollView_Pannel.transform.position = v;
			if (ScrollView_Pannel.transform.position.x<= target_pos.x)
				forword_back_notification = 0;
			//}
		}

		if (forword_back_notification == 2) {
			Vector3 v = ScrollView_Pannel.transform.position;
				v.x += 4f;
				ScrollView_Pannel.transform.position = v;
			if (ScrollView_Pannel.transform.position.x>= target_pos.x)
				forword_back_notification = 0;
		}
	}
}
