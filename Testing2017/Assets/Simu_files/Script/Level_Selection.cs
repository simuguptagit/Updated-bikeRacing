using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Level_Selection : MonoBehaviour {
	public GameObject level_02;
	public GameObject lock_level_02;
	public GameObject level_03;
	public GameObject lock_level_03;
	public GameObject level_04;
	public GameObject lock_level_04;
	public GameObject level_05;
	public GameObject lock_level_05;
	public GameObject level_06;
	public GameObject lock_level_06;
	bool b;
	int forword_back_notification;
	int button_status_foe_back;
	public GameObject ScrollView_Pannel;
	public float cureenttime;
	// Use this for initialization
	void Start () {
		
		forword_back_notification = 0;
		button_status_foe_back = 0;

		for (int i = 0; i < 10; i++) {
			if (PlayerPrefs.GetFloat ("i") == 0)
				PlayerPrefs.SetFloat ("i", 0);
		}

		if (PlayerPrefs.GetInt ("Level_Selection") == 0)
			PlayerPrefs.SetInt ("Level_Selection", 1);

		Level_button_text (0,GameObject.Find("Canvas/level_selection/Panel/Scrollview_Panel/level_01"));

		if (PlayerPrefs.GetInt ("Level_Selection") <= 1)
			b = true;
		else 
			b= false;
		//txt= GameObject.Find ("Canvas/level_selection/Panel/Scrollview_Panel/level_02").GetComponentsInChildren<Text> ();
		//txt[1].text = PlayerPrefs.GetFloat (1);
		Level_button_text (1,GameObject.Find("Canvas/level_selection/Panel/Scrollview_Panel/level_02"));
		button_active_inactive (GameObject.Find("Canvas/level_selection/Panel/Scrollview_Panel/level_02"),GameObject.Find("Canvas/level_selection/Panel/Scrollview_Panel/Lock_level_02"), b);

		if (PlayerPrefs.GetInt ("Level_Selection") <= 2)
			b = true;
		else 
			b= false;
		//txt= GameObject.Find ("Canvas/level_selection/Panel/Scrollview_Panel/level_03").GetComponentsInChildren<Text> ();
		//txt[1].text = PlayerPrefs.GetFloat (2);
		Level_button_text (2,GameObject.Find("Canvas/level_selection/Panel/Scrollview_Panel/level_03"));
		button_active_inactive (GameObject.Find("Canvas/level_selection/Panel/Scrollview_Panel/level_03"), GameObject.Find("Canvas/level_selection/Panel/Scrollview_Panel/Lock_level_03"), b);

		if (PlayerPrefs.GetInt ("Level_Selection") <= 3)
			b = true;
		else 
			b= false;
		//txt= GameObject.Find ("Canvas/level_selection/Panel/Scrollview_Panel/level_04").GetComponentsInChildren<Text> ();
		//txt[1].text = PlayerPrefs.GetFloat (3);
		Level_button_text (3,GameObject.Find("Canvas/level_selection/Panel/Scrollview_Panel/level_04"));
		button_active_inactive (GameObject.Find("Canvas/level_selection/Panel/Scrollview_Panel/level_04"), GameObject.Find("Canvas/level_selection/Panel/Scrollview_Panel/lock_level_04"), b);

		if (PlayerPrefs.GetInt ("Level_Selection") <= 4)
			b = true;
		else 
			b= false;
		//txt= GameObject.Find ("Canvas/level_selection/Panel/Scrollview_Panel/level_05").GetComponentsInChildren<Text> ();
	//	txt[1].text = PlayerPrefs.GetFloat (4);
		Level_button_text (4,GameObject.Find("Canvas/level_selection/Panel/Scrollview_Panel/level_05"));
		button_active_inactive (GameObject.Find("Canvas/level_selection/Panel/Scrollview_Panel/level_05"), GameObject.Find("Canvas/level_selection/Panel/Scrollview_Panel/Lock_level_05"), b);

		if (PlayerPrefs.GetInt ("Level_Selection") <= 5)
			b = true;
		else 
			b= false;
		//txt= GameObject.Find ("Canvas/level_selection/Panel/Scrollview_Panel/level_06").GetComponentsInChildren<Text> ();
		//txt[1].text = PlayerPrefs.GetFloat (5);
		Level_button_text (5,GameObject.Find("Canvas/level_selection/Panel/Scrollview_Panel/level_06"));
		button_active_inactive (GameObject.Find("Canvas/level_selection/Panel/Scrollview_Panel/level_06"), GameObject.Find("Canvas/level_selection/Panel/Scrollview_Panel/Lock_level_06"), b);

		if (PlayerPrefs.GetInt ("Level_Selection") <= 6)
			b = true;
		else 
			b= false;
		//txt= GameObject.Find ("Canvas/level_selection/Panel/Scrollview_Panel/level_07").GetComponentsInChildren<Text> ();
		//txt[1].text = PlayerPrefs.GetFloat (6);
		Level_button_text (6,GameObject.Find("Canvas/level_selection/Panel/Scrollview_Panel/level_07"));
		button_active_inactive (GameObject.Find("Canvas/level_selection/Panel/Scrollview_Panel/level_07"), GameObject.Find("Canvas/level_selection/Panel/Scrollview_Panel/Lock_level_07"), b);

		if (PlayerPrefs.GetInt ("Level_Selection") <= 7)
			b = true;
		else 
			b= false;
		//txt= GameObject.Find ("Canvas/level_selection/Panel/Scrollview_Panel/level_08").GetComponentsInChildren<Text> ();
		//txt[1].text = PlayerPrefs.GetFloat (7);
		Level_button_text (7,GameObject.Find("Canvas/level_selection/Panel/Scrollview_Panel/level_08"));
		button_active_inactive (GameObject.Find("Canvas/level_selection/Panel/Scrollview_Panel/level_08"), GameObject.Find("Canvas/level_selection/Panel/Scrollview_Panel/Lock_level_08"), b);

		if (PlayerPrefs.GetInt ("Level_Selection") <=8)
			b = true;
		else 
			b= false;
		//txt= GameObject.Find ("Canvas/level_selection/Panel/Scrollview_Panel/level_09").GetComponentsInChildren<Text> ();
		//txt[1].text = PlayerPrefs.GetFloat (8);
		Level_button_text (8,GameObject.Find("Canvas/level_selection/Panel/Scrollview_Panel/level_09"));
		button_active_inactive (GameObject.Find("Canvas/level_selection/Panel/Scrollview_Panel/level_09"), GameObject.Find("Canvas/level_selection/Panel/Scrollview_Panel/Lock_level_09"), b);

		if (PlayerPrefs.GetInt ("Level_Selection") <= 9)
			b = true;
		else 
			b= false;
		//txt= GameObject.Find ("Canvas/level_selection/Panel/Scrollview_Panel/level_10").GetComponentsInChildren<Text> ();
		//txt[1].text = PlayerPrefs.GetFloat (9);
		Level_button_text (9,GameObject.Find("Canvas/level_selection/Panel/Scrollview_Panel/level_10"));
		button_active_inactive (GameObject.Find("Canvas/level_selection/Panel/Scrollview_Panel/level_10"), GameObject.Find("Canvas/level_selection/Panel/Scrollview_Panel/Lock_level_10"), b);

		GameObject.Find ("Canvas/level_selection/back").SetActive (false);
		GameObject.Find ("Canvas/level_selection").SetActive (false);

	}

	Text[] txt;
	public void Level_button_text(int level,GameObject level_no){
		txt= level_no.GetComponentsInChildren<Text> ();
		txt[1].text = PlayerPrefs.GetFloat ("level").ToString();
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
		//scrollend_pos = ScrollView_Pannel.transform.position - new Vector3 (5f, 0, 0);
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
	//float dis;
	//float dis = 0;
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

			//if (dis == 0) {
				//	dis += .1f;
				v.x -= 4f;
				ScrollView_Pannel.transform.position = v;
				//dis = Vector3.Distance ( ScrollView_Pannel.transform.position,target_pos);
			//} else {
			//Debug.Log ("levelselecttttt..." +ScrollView_Pannel.transform.position.x+"   "+target_pos.x);
			if (ScrollView_Pannel.transform.position.x<= target_pos.x)
				forword_back_notification = 0;
			//}
		}

		if (forword_back_notification == 2) {
			Vector3 v = ScrollView_Pannel.transform.position;
			//Debug.Log ("levelselecttttt..." + v);
			//if (dis <= 158) {
				//	dis += .1f;
				v.x += 4f;
				ScrollView_Pannel.transform.position = v;
				//dis = Vector3.Distance (scrollstrt_pos, ScrollView_Pannel.transform.position);
		//	} else {
			if (ScrollView_Pannel.transform.position.x>= target_pos.x)
				forword_back_notification = 0;
			//}
		}
	}
}
