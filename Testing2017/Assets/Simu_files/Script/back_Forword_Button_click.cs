using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class back_Forword_Button_click : MonoBehaviour {

	public Text Power;
	public Text Weight;
	public Text Grip;

	public GameObject model1;
	public GameObject model2;
	public GameObject model3;

	private int change_model_no;
	public  Text powertext;
	public  Text weighttext;	
	public  Text griptext;
	public  Text bikeinfo;
	public  Text Model_bike_name;

	public Image loadingbarpower;
	public Image loadingbarweight;
	public Image loadingbargript;
	private bool loading;
	public float loadingtime;

	public static bool localizationFontchange;
	// Use this for initialization
	void Start () {
		localizationFontchange = false;
		Power.text = localization.power.ToString ();
		Weight.text = localization.weight.ToString ();
		Grip.text = localization.grip.ToString ();
	
		change_model_no = 0;
		PlayerPrefs.SetInt ("ModelNo",change_model_no+1);
	}

	public void Forword(){
		if(change_model_no<2)
			change_model_no += 1;
		else
			change_model_no = 0;

		if (change_model_no == 0) {
			model3.SetActive(false);
			model1.SetActive (true);
		}else if (change_model_no == 1) {
			model1.SetActive(false);
			model2.SetActive(true);
		}
		else if (change_model_no == 2) {
			model2.SetActive(false);
			model3.SetActive(true);
		}

		loadingbarpower.fillAmount = 0;
		loadingbarweight.fillAmount = 0;	
		loadingbargript.fillAmount = 0;
		powerpoint = 1000;
		weghtpoint = 4000;
		grippoint = 20000;

		PlayerPrefs.SetInt ("ModelNo",change_model_no+1);
		Model_information (change_model_no+1);
	}

	public void Back(){
		if(change_model_no>0)
			change_model_no -= 1;
		else
			change_model_no = 2;

		if (change_model_no ==0) {
			model2.SetActive(false);
			model1.SetActive (true);
		}else if (change_model_no == 1) {
			model3.SetActive(false);
			model2.SetActive(true);
		}
		else if (change_model_no == 2) {
			model1.SetActive(false);
			model3.SetActive(true);
		}


		loadingbarpower.fillAmount = 0;
		loadingbarweight.fillAmount = 0;	
		loadingbargript.fillAmount = 0;
		powerpoint = 1000;
		weghtpoint = 4000;
		grippoint = 20000;
		PlayerPrefs.SetInt ("ModelNo",change_model_no+1);
		Model_information (change_model_no+1);
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
	public  void Model_information( int model__no){
		//Debug.Log ("What calling..." + model__no);
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
			model_bike = "TRIUMPH RACING";
		}
		powertext.text = p.ToString();
		weighttext.text = w.ToString();
		griptext.text = g.ToString();
		bikeinfo.text = b.ToString();
		Model_bike_name.text = model_bike.ToString ();

		loadpower = (float)power /(float) powerpoint;
		loadweight =(float) weight /(float) weghtpoint;
		loadgrip =(float) grip /(float) grippoint;
	}

	// Update is called once per frame
	void Update () {
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
		if(localizationFontchange)
		{   Power.text = localization.power.ToString ();
			Weight.text = localization.weight.ToString ();
			Grip.text = localization.grip.ToString ();
			Model_information(PlayerPrefs.GetInt ("ModelNo"));
			localizationFontchange = false;
		}
	}
}
