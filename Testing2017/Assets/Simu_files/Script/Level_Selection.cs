using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
	public GameObject ScrollView_Pannel;
	public float cureenttime;
	// Use this for initialization
	void Start () {

		forword_back_notification = 0;

		if (PlayerPrefs.GetInt ("Level_Selection") == 0)
			PlayerPrefs.SetInt ("Level_Selection", 1);
		
		if (PlayerPrefs.GetInt ("Level_Selection") <= 1)
			b = true;
		else 
			b= false;
		button_active_inactive (level_02, lock_level_02, b);

		if (PlayerPrefs.GetInt ("Level_Selection") <= 2)
			b = true;
		else 
			b= false;
		button_active_inactive (level_03, lock_level_03, b);

		if (PlayerPrefs.GetInt ("Level_Selection") <= 3)
			b = true;
		else 
			b= false;
		button_active_inactive (level_04, lock_level_04, b);

		if (PlayerPrefs.GetInt ("Level_Selection") <= 4)
			b = true;
		else 
			b= false;
		button_active_inactive (level_05, lock_level_05, b);

		if (PlayerPrefs.GetInt ("Level_Selection") <= 5)
			b = true;
		else 
			b= false;
		button_active_inactive (level_06, lock_level_06, b);

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
		dis = 0f;
		forword_back_notification = 1;
	}
	public void Back_button(){
		//scrollend_pos = ScrollView_Pannel.transform.position - new Vector3 (5f, 0, 0);
		scrollstrt_pos = ScrollView_Pannel.transform.position;
		dis = 0f;
		forword_back_notification = 2;
	}
	Vector3 scrollstrt_pos;
	float dis;
	//float dis = 0;
	// Update is called once per frame
	void Update () {

		if (forword_back_notification == 1) {
			Vector3 v = ScrollView_Pannel.transform.position;
			Debug.Log ("levelselecttttt..." + v);
			if (dis <= 158) {
				//	dis += .1f;
				v.x -= 4f;
				ScrollView_Pannel.transform.position = v;
				dis = Vector3.Distance (scrollstrt_pos, ScrollView_Pannel.transform.position);
			} else {
				forword_back_notification = 0;
			}
		}

		if (forword_back_notification == 2) {
			Vector3 v = ScrollView_Pannel.transform.position;
			Debug.Log ("levelselecttttt..." + v);
			if (dis <= 158) {
				//	dis += .1f;
				v.x += 4f;
				ScrollView_Pannel.transform.position = v;
				dis = Vector3.Distance (scrollstrt_pos, ScrollView_Pannel.transform.position);
			} else {
				forword_back_notification = 0;
			}
		}
	}
}
