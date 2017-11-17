using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ModelChange_Rotate : MonoBehaviour {
	//public Camera cam;
	public  Text powertext;
	public  Text weighttext;	
	public  Text griptext;
	public  Text bikeinfo;
	public int model__no;
	public  Text Model_bike_name;

	public Image loadingbarpower;
	public Image loadingbarweight;
	public Image loadingbargript;
	private bool loading;
	public float loadingtime;

	// Use this for initialization
	void Start () {
		localization.local (PlayerPrefs.GetString ("Lang"));
		//Debug.Log ("locallyy...."+PlayerPrefs.GetString ("Lang")+"   "+localization.power);

		loading = false;
		loadingbarpower.fillAmount = 0;
		loadingbarweight.fillAmount = 0;	
		loadingbargript.fillAmount = 0;
		powerpoint = 1000;
		weghtpoint = 4000;
		grippoint = 20000;

		Model_information ();
	}
	string p,
	w,
	g,
	b,
	model_bike;
	float loadpower ,
	loadweight,
	loadgrip;

	int power,
	weight,
	grip,
	powerpoint,
	weghtpoint,
	grippoint;
	public void Model_information( ){
		
		if (model__no == 1) {
			p = localization.power1Text;
			w = localization.weight1Text;
			g = localization.grip1Text;
			b = localization.Bike_1_info;
			power = 834;
			weight = 3321;
			grip = 16504;
			model_bike = "MOOSE RACING";
		}
		else if(model__no == 2){
			p = localization.power2Text;
			w = localization.weight2Text;
			g = localization.grip2Text;
			b = localization.Bike_2_info;
			power = 850;
			weight = 3655;
			grip = 18904;
			model_bike = "BMW RACING";
		}
		else if(model__no == 3){
			p = localization.power3Text;
			w = localization.weight3Text;
			g = localization.grip3Text;
			b = localization.Bike_3_info;
			power = 900;
			weight = 4000;
			grip = 20000;
			model_bike = "Triumph RACING";
		}
		Debug.Log ("checkhh..."+p+"  "+power+"   "+powerpoint);

		powertext.text = p.ToString();
		weighttext.text = w.ToString();
		griptext.text = g.ToString();
		bikeinfo.text = b.ToString();
		Model_bike_name.text = model_bike.ToString ();

		loadpower = (float)power /(float) powerpoint;
		loadweight =(float) weight /(float) weghtpoint;
		loadgrip =(float) grip /(float) grippoint;
		loading = true;
	}

	// Update is called once per frame
	void Update () {

		if (Input.GetKeyDown (KeyCode.Escape)) Application.Quit ();

		if(Camera_Rotate.Scroll_stop == false)
		transform.Rotate (0,6*Time.deltaTime,0);

		if (loading == true) {//Debug.Log ("check..."+loadpower+"  "+loadgrip);
			if (loadingbarpower.fillAmount <= loadpower) {
				loadingbarpower.fillAmount += 1.0f / loadingtime * Time.deltaTime;
			} 
			if (loadingbarweight.fillAmount <= loadweight) {
				loadingbarweight.fillAmount += 1.0f / loadingtime * Time.deltaTime;
			}
			if (loadingbargript.fillAmount <= loadgrip) {
				loadingbargript.fillAmount += 1.0f / loadingtime * Time.deltaTime;
			} 
		}
	}
}
