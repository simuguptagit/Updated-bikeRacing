using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class All_Buttom_button_touch : MonoBehaviour {
	public GameObject Forword;
	public GameObject Back;
	public GameObject Select;
	public GameObject Level_Selection_Pannel;
	public GameObject Levels;

	public GameObject Lang_Panel;
	public Image langImage;
	public Image india;
	public Image england;
	public Image canada;
	public Image brazil;
	public Image portugal;
	public Image spain;
	public Image usa;

	public  Text SettingFont;
	public  Text game_text;
	public  Text bike_text;
	public  Text lang;

	public  Text Cntrl_Steeringtxt;
	public  Text Cntrl_invertcntrl;
	public  Text Cntrl_sensivity;
	public  Text Cntrl_preview;
	public  Text Cntrl_restofdefaults;

	public  Text lang_india;
	public  Text lang_england;
	public  Text lang_brazil;
	public  Text lang_canada;
	public  Text lang_portugal;
	public  Text lang_spain;
	public  Text lang_usa;

	public  Text Speed;
	public GameObject setting_panel;

	public Slider bikeSlider;
	public Slider gameslider;

	public Dropdown Cntrl_steering;
	public GameObject cntrl_preview1;
	public GameObject cntrl_preview2;
	public Toggle cntrl_invert;
	public Slider cntrl_Slider_sensivity;

	public Dropdown Advdropspeed;
	public GameObject Bikelistselected;
	public GameObject Settingselected;
	public GameObject raceselected;

	private float sliderfillingamount = .001f;
	private float sliderfillingtime = .01f;

	private Animator settinganim;

	public  Text BikeListbutton;
	public  Text SettingButton;
	public  Text RaceButton;
	public  Text MoreButton;

	public Image loadingbar;
	public float loadingtime;

	public GameObject Loading;
	public GameObject Dot;
	public GameObject Dot1;
	public GameObject Dot2;
	public GameObject Dot3;
	public GameObject Dot4;
	private int dot;
	private bool loadingbool;
	AsyncOperation async;

	// Use this for initialization
	void Start () {
		loadingbar.fillAmount = 0;
		BUTTOM_ButtonFont ();
		settinganim =  setting_panel.GetComponent<Animator> ();

		Bikelistselected.SetActive (true);
		raceselected.SetActive(false);

		if (PlayerPrefs.GetFloat ("game") == 0)
			PlayerPrefs.SetFloat ("game", 1);
		if (PlayerPrefs.GetFloat ("bike") == 0)
			PlayerPrefs.SetFloat ("bike", 1);
		if (PlayerPrefs.GetString ("Graphics") == null)
			PlayerPrefs.SetString ("Graphics", null);
		if (PlayerPrefs.GetString ("Lang") == null)
			PlayerPrefs.SetString ("Lang", null);

		if (PlayerPrefs.GetString ("cntrl_steering") == null)
			PlayerPrefs.SetString ("cntrl_steering", null);
		if (PlayerPrefs.GetString ("cntrl_throttle") == null)
			PlayerPrefs.SetString ("cntrl_throttle", null);
		if (PlayerPrefs.GetString ("cntrl_gear") == null)
			PlayerPrefs.SetString ("cntrl_gear", null);
		if (PlayerPrefs.GetString ("cntrl_invert") == null)
			PlayerPrefs.SetString ("cntrl_invert", null);
		if (PlayerPrefs.GetString ("cntrl_detach") == null)
			PlayerPrefs.SetString ("cntrl_detach", null);
		if (PlayerPrefs.GetFloat ("sensivity") == 0)
			PlayerPrefs.SetFloat ("sensivity", 1);

		if (PlayerPrefs.GetFloat ("gamesliderpath") == 0)
			PlayerPrefs.SetFloat ("gamesliderpath", 0);

		if (PlayerPrefs.GetString ("Advspeed") == null)
			PlayerPrefs.SetString ("Advspeed", null);
		if (PlayerPrefs.GetString ("Advwheather") == null)
			PlayerPrefs.SetString ("Advwheather", null);
		if (PlayerPrefs.GetString ("AdvDrive") == null)
			PlayerPrefs.SetString ("AdvDrive", null);
		if (PlayerPrefs.GetString ("Advblood") == null)
			PlayerPrefs.SetString ("Advblood", null);
	}
	public  void BUTTOM_ButtonFont(){
		BikeListbutton.text = localization.Bikelistbutton.ToString ();
		SettingButton.text = localization.Settingbutton.ToString ();
		RaceButton.text = localization.Racebutton.ToString ();
		MoreButton.text = localization.Morebutton.ToString ();
	}

	// Update is called once per frame
	void Update () {

		if (loadingbool == true) {
			if( dot == 0) Dot.SetActive (true);
			if (dot == 1) {
				Dot1.SetActive (true);
			} else if (dot == 2) {
				Dot2.SetActive (true);
			} else if (dot == 3) {
				Dot3.SetActive (true);
			} else if (dot == 4) {
				Dot4.SetActive (true);
			}
			else if (dot == 5) {
				Dot.SetActive (false);
				Dot4.SetActive (false);
				Dot3.SetActive (false);
				Dot2.SetActive (false);
				Dot1.SetActive (false);
			}

			if (loadingbar.fillAmount <= 1) {
				loadingbar.fillAmount += 1.0f / loadingtime * Time.deltaTime;
			} 
			if(loadingbar.fillAmount >= 1 && async.progress == .9f) {
				async.allowSceneActivation = true;
			}
		}
		
	}
	public IEnumerator FillSlider(Slider slider, float value)
	{
		if (slider.value < value) {
			yield return new WaitForSeconds (sliderfillingtime);
			slider.value += sliderfillingamount;
			StartCoroutine (FillSlider (slider, value));
		}
	}

	public void Setting(){
		
		Bikelistselected.SetActive (false);
		Cntrl_Steeringtxt.text = localization.Steering.ToString ();
		SettingFont.text = localization.SettingFont.ToString ();
		game_text.text = localization.game_sound.ToString ();
		bike_text.text = localization.bike_sound.ToString ();
		Cntrl_invertcntrl.text = localization.InvertControl.ToString ();
		Cntrl_sensivity.text = localization.Sensivity.ToString ();
		Cntrl_preview.text = localization.preview.ToString ();
		Cntrl_restofdefaults.text = localization.restOfDefault.ToString ();
		Speed.text = localization.Speed.ToString();

		lang.text = localization.Lang.ToString ();

		settinganim.SetBool ("reverse",false);
		Settingselected.GetComponentInChildren<Text>().text = localization.SettingFont;
		Settingselected.SetActive (true);

		if (PlayerPrefs.GetString ("cntrl_steering") == "TILT") {
			Cntrl_steering.value = 0;
			cntrl_preview1.SetActive (true);
		} else if (PlayerPrefs.GetString ("cntrl_steering") == "Button") {
			Cntrl_steering.value = 1;
			cntrl_preview2.SetActive (true);
		}

		if (PlayerPrefs.GetString ("Advspeed") == "KMH") 
			Advdropspeed.value = 0;
		else 
			Advdropspeed.value = 1;

		cntrl_Slider_sensivity.value = PlayerPrefs.GetFloat ("sensivity");

		if (PlayerPrefs.GetString ("cntrl_invert") == "true")
			cntrl_invert.isOn = true;
		else
			cntrl_invert.isOn = false;
			gameslider.value = PlayerPrefs.GetFloat ("game");
		bikeSlider.value = PlayerPrefs.GetFloat ("bike");
	
		if (PlayerPrefs.GetString ("Lang") == "IN")
			langImage.sprite = india.sprite;
		else if (PlayerPrefs.GetString ("Lang") == "EN")
			langImage.sprite = england.sprite;
		else if (PlayerPrefs.GetString ("Lang") == "CA")
			langImage.sprite = canada.sprite;
		else if (PlayerPrefs.GetString ("Lang") == "BR")
			langImage.sprite = brazil.sprite;
		else if (PlayerPrefs.GetString ("Lang") == "SP")
			langImage.sprite = spain.sprite;
		else if (PlayerPrefs.GetString ("Lang") == "USA")
			langImage.sprite = usa.sprite;
		else if (PlayerPrefs.GetString ("Lang") == "PO")
			langImage.sprite = portugal.sprite;

		setting_panel.SetActive (true);
		//Debug.Log ("Setting..."+PlayerPrefs.GetString ("cntrl_steering")+"   "+PlayerPrefs.GetString ("Advspeed")+"   "+PlayerPrefs.GetString ("Lang")
		//	+"   "+PlayerPrefs.GetString ("cntrl_invert")+"  "+gameslider.value+"  "+bikeSlider.value+"  "+cntrl_Slider_sensivity.value);
	}
	public void Reset(){
		Cntrl_steering.value = 0;
		PlayerPrefs.SetString ("cntrl_steering", Cntrl_steering.options[Cntrl_steering.value].text);
		if (Cntrl_steering.value == 1) {
			cntrl_preview2.SetActive (true);
			cntrl_preview1.SetActive (false);
		} else {
			cntrl_preview2.SetActive (false);
			cntrl_preview1.SetActive (true);
		}

		Advdropspeed.value = 0;
		PlayerPrefs.SetString ("Advspeed", Advdropspeed.options[Advdropspeed.value].text);

		langImage.sprite = india.sprite;
		PlayerPrefs.SetString ("Lang","IN");
		font_Calling ();

		cntrl_invert.isOn = false;
		if(cntrl_invert.isOn)
			PlayerPrefs.SetString ("cntrl_invert","true");
		else
			PlayerPrefs.SetString ("cntrl_invert","false");

		gameslider.value = 0;
		PlayerPrefs.SetFloat ("game", gameslider.value);

		bikeSlider.value = 0;
		PlayerPrefs.SetFloat ("bike", bikeSlider.value);

		cntrl_Slider_sensivity.value = 0;
		PlayerPrefs.SetFloat ("sensivity", cntrl_Slider_sensivity.value);
	}

	public void cross_Setting(){
		Bikelistselected.SetActive (true);
		Settingselected.SetActive (false);
		settinganim.SetBool ("reverse",true);
	}

	public void cross_Lang(){
		Lang_Panel.SetActive (false);
	}
	public void Race(){
		Bikelistselected.SetActive (false);
		raceselected.GetComponentInChildren<Text>().text = localization.Racebutton;
		raceselected.SetActive(true);
		Level_Selection_Pannel.SetActive (true);
	}

	public void select(){
		Camera_Rotate.Scroll_stop = true;
		//Buttom_panel.SetActive (false);
		Levels.SetActive (true);
		Forword.SetActive (false);
		Back.SetActive (false);
		Select.SetActive (false);
	}

	public void OnControl_game(){
		PlayerPrefs.SetFloat ("game", gameslider.value);
	}
	public void OnControl_bike(){
		PlayerPrefs.SetFloat ("bike", bikeSlider.value);
	}

	public void OnControl_lang(){
		lang_india.text = localization.India.ToString ();
		lang_england.text = localization.England.ToString ();
		lang_canada.text  = localization.Canada.ToString ();
		lang_brazil.text  = localization.Brazil.ToString ();
		lang_portugal.text  = localization.Portgal.ToString ();
		lang_spain.text = localization.Spain.ToString ();
		lang_usa.text = localization.Usa.ToString ();
		Lang_Panel.SetActive(true);
	}
	public void font_Calling(){
		localization.local (PlayerPrefs.GetString ("Lang"));
		Setting ();
		BUTTOM_ButtonFont ();
	}
	public void OnTouch_india(){
		//Debug.Log ("lang..india");
		Lang_Panel.SetActive (false);
		langImage.sprite = india.sprite;
		PlayerPrefs.SetString ("Lang","IN");
		font_Calling ();
	}
	public void OnTouch_england(){//Debug.Log ("lang..england");
		Lang_Panel.SetActive (false);
		langImage.sprite = england.sprite;
		PlayerPrefs.SetString ("Lang","EN");
		font_Calling();
	}
	public void OnTouch_brazil(){Debug.Log ("lang..brazil");
		Lang_Panel.SetActive (false);
		langImage.sprite = brazil.sprite;
		PlayerPrefs.SetString ("Lang","CA");
		font_Calling();
	}
	public void OnTouch_canada(){//Debug.Log ("lang..canada");
		Lang_Panel.SetActive (false);
		langImage.sprite = canada.sprite;
		PlayerPrefs.SetString ("Lang","CA");
		font_Calling();
	}
	public void OnTouch_portugal(){//Debug.Log ("lang..portugal");
		Lang_Panel.SetActive (false);
		langImage.sprite = portugal.sprite;
		PlayerPrefs.SetString ("Lang","PO");localization.local (PlayerPrefs.GetString ("Lang"));
	}
	public void OnTouch_spain(){//Debug.Log ("lang..spain");
		Lang_Panel.SetActive (false);
		langImage.sprite = spain.sprite;
		PlayerPrefs.SetString ("Lang","SP");localization.local (PlayerPrefs.GetString ("Lang"));
	}
	public void OnTouch_usa(){//Debug.Log ("lang..usa");
		Lang_Panel.SetActive (false);
		langImage.sprite = usa.sprite;
		PlayerPrefs.SetString ("Lang","USA");localization.local (PlayerPrefs.GetString ("Lang"));
	}

	public void Onsteering(){
		PlayerPrefs.SetString ("cntrl_steering", Cntrl_steering.options[Cntrl_steering.value].text);
		if (Cntrl_steering.value == 1) {
			cntrl_preview2.SetActive (true);
			cntrl_preview1.SetActive (false);
		} else {
			cntrl_preview2.SetActive (false);
			cntrl_preview1.SetActive (true);
		}
	}

	public void Oninvert(){
		if(cntrl_invert.isOn)
			PlayerPrefs.SetString ("cntrl_invert","true");
		else
			PlayerPrefs.SetString ("cntrl_invert","false");
	}
	public void OnSensivity(){
		PlayerPrefs.SetFloat ("sensivity", cntrl_Slider_sensivity.value);
	}

	public void OnAdv_Speed(){
		PlayerPrefs.SetString ("Advspeed", Advdropspeed.options[Advdropspeed.value].text);
	}


	public  void Level_selection_backPress(){
		raceselected.SetActive(false);
		Bikelistselected.SetActive (true);
		Level_Selection_Pannel.SetActive (false);
	}
	public  void Level_selection_level_01(){
		//Level_Selection_Pannel.SetActive (false);
		Level_Play_assign (1,15.93f,1.17f,179.392f,168.0995f);
		//Debug.Log ("printlevel222....");
	}
	public  void Level_selection_level_02(){
		Level_Play_assign (2,219.536f,1.17f,109.291f, -20f);
		//Debug.Log ("printlevel222222....");
	}
	public  void Level_selection_level_03(){
		Level_Play_assign (3,189.585f,1.17f,95.8024f, -18.324f);
		//Debug.Log ("printlevel333333....");
	}
	public  void Level_selection_level_04(){
		Level_Play_assign (4,81.93f,1.17f,69.5f, 0f);
		//Debug.Log ("printlevel44444....");
	}
	public  void Level_selection_level_05(){
		Level_Play_assign (5,114.033f,1.17f,105.391f, -16.0945f);
		//Debug.Log ("printlevel55555....");
	}
	public  void Level_selection_level_06(){
		Level_Play_assign (6,33.01f,1.17f,62.39f,0f);
		//Debug.Log ("printlevel66666....");
	}
	public  void Level_selection_level_07(){
		Level_Play_assign (7,199.1f,1.34f,197.59f,180f);
		//Debug.Log ("printlevel66666....");
	}
	public  void Level_selection_level_08(){
		Level_Play_assign (8,86.6297f,1.48f,24.26351f,82.3026f);
		//Debug.Log ("printlevel66666....");
	}
	public  void Level_selection_level_09(){
		Level_Play_assign (9,122.6117f,1.36f,87.26141f,-13.86395f);
		//Debug.Log ("printlevel66666....");
	}
	public  void Level_selection_level_10(){
		Level_Play_assign (10,73.69089f,1.28f,187.2304f,154.2717f);
		//Debug.Log ("printlevel66666....");
	}


	public void Level_Play_assign(int lvlno , float bikex , float bikey , float bikez , float bikeroty){
		GameController.LevelNo = lvlno;
		GameController.Bike_x = bikex;
		GameController.Bike_y = bikey;
		GameController.Bike_z = bikez;
		GameController.Bike_Rotation_y = bikeroty;
		StartCoroutine (PlayChange());
	}
	IEnumerator PlayChange(){
		loadingbool = true;
		Dot.SetActive (true);
		StartCoroutine (DotnoChange());
		Loading.SetActive (true);
		async = SceneManager.LoadSceneAsync(1);
		async.allowSceneActivation = false;

		while (async.isDone == false) {
			yield return null;
		}
	}

	IEnumerator DotnoChange(){
		yield return new WaitForSeconds(.2f);
		if (dot == 0) dot = 1;
		else if (dot == 1) dot = 2;
		else if (dot == 2) dot = 3;
		else if (dot == 3) dot = 4;
		else if (dot == 4) dot = 5;
		else if (dot == 5) dot = 0;
		StartCoroutine (DotnoChange());
	}
}
