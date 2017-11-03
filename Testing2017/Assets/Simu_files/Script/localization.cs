using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class localization : MonoBehaviour {

	public static string power;
	public static string weight;
	public static string grip;

	public static string power1Text;
	public static string weight1Text;
	public static string grip1Text;

	public static string power2Text;
	public static string weight2Text;
	public static string grip2Text;

	public static string power3Text;
	public static string weight3Text;
	public static string grip3Text;

	public static string Bike_1_info;
	public static string Bike_2_info;
	public static string Bike_3_info;

	public static string Bikelistbutton;
	public static string Settingbutton;
	public static string Racebutton;

	public static string SettingFont;
	public static string Music;
	public static string Sound;
	public static string Graphics;
	public static string Control;
	public static string Advanced;
	public static string Lang;

	public static string ControlText;
	public static string Steering;
	public static string Throttle;
	public static string Gear;
	public static string InvertControl;
	public static string DetachBreak;
	public static string Sensivity;
	public static string preview;
	public static string calibrate;
	public static string restOfDefault;

	public static string AdvancedText;
	public static string Speed;
	public static string wheather;
	public static string driveleft;
	public static string blood;

	public static string India;
	public static string England;
	public static string Canada;
	public static string Brazil;
	public static string Portgal;
	public static string Spain;
	public static string Usa;

	void Start () {
		local (PlayerPrefs.GetString ("Lang"));
	}

	public static void local(string str){
		if (str == "EN") {
			power = "Power ";
			weight = "Weight";
			grip = "Grip";

			power1Text = "834 bhp";
			weight1Text = "3321 ips";
			grip1Text = "16504";

			power2Text = "850 bhp";
			weight2Text = "3655 ips";
			grip2Text = "18904";

			power3Text = "900 bhp";
			weight3Text = "4000 ips";
			grip3Text = "20000";

			Bike_1_info = "Rear wheel drive 6 -speed gearbox";
			Bike_2_info = "Rear wheel drive 7 -speed gearbox";
			Bike_3_info = "Rear wheel drive 8 -speed gearbox";

			Bikelistbutton = "BikeList";
			Settingbutton = "Setting";
			Racebutton = "Race";

			SettingFont = "Setting";
			Music = "Music";
			Sound = "Sound";
			Graphics = "Graphics";
			Control = "Control";
			Advanced = "Advanced";
			Lang = "Language";

			ControlText = "Control";
			Steering = "Steering";
			Throttle = "Throttle";
			Gear = "Gear";
			InvertControl = "Invert Controls";
			DetachBreak = "Detach Breaks";
			Sensivity = "Sensivity";
			preview = "Preview";
			calibrate = "Calibrate";
			restOfDefault = "Rest Of Defaults";

			AdvancedText = "Advanced";
			Speed = "Speed unit";
			wheather = "Weather Effects";
			driveleft = "Drive On Left";
			blood = "Blood Effect";

			India = "INDIA";
			England = "ENGLAND";
			Canada = "CANADA";
			Brazil = "BRAZIL";
			Portgal = "PORTUGAL";
			Spain = "SPAIN";
			Usa = "USA";

		} else if (str == "CA") {
			
			power = "Leistung";
			weight = "Gewicht";
			grip = "Griff";

			power1Text = "834 ch";
			weight1Text = "3321 ips";
			grip1Text = "16504";

			power2Text = "850 ch";
			weight2Text = "3655 ips";
			grip2Text = "18904";

			power3Text = "900 ch";
			weight3Text = "4000 ips";
			grip3Text = "20000";

			Bike_1_info = "Traction arrière boîte de vitesses à 6 vitesses";
			Bike_2_info = "Traction arrière boîte de vitesses à 7 vitesses";
			Bike_3_info = "Traction arrière boîte de vitesses à 8 vitesses";

			Bikelistbutton = "BikeList";
			Settingbutton = "Réglage";
			Racebutton = "Course";

			SettingFont = "Réglage";
			Music = "La musique";
			Sound = "Du son";
			Graphics = "Graphique";
			Control = "Contrôle";
			Advanced = "Avancée";
			Lang = "La langue";

			ControlText = "Contrôle";
			Steering = "Pilotage";
			Throttle = "Étrangler";
			Gear = "Équipement";
			InvertControl = "Inverser les contrôles";
			DetachBreak = "Détachez les pauses";
			Sensivity = "Sensibilité";
			preview = "Aperçu";
			calibrate = "Étalonner";
			restOfDefault = "Reste des paramètres par défaut";

			AdvancedText = "Avancée";
			Speed = "Unité de vitesse";
			wheather = "Météo Effets";
			driveleft = "Conduire Sur La gauche";
			blood = "Du sang La gauche";

			India = "INDE";
			England = "ANGLETERRE";
			Canada = "CANADA";
			Brazil = "BRÉSIL";
			Portgal = "PORTUGAL";
			Spain = "ESPAGNE";
			Usa = "Etats-Unis";
		} else {
			power = "Power ";
			weight = "Weight";
			grip = "Grip";

			power1Text = "834 bhp";
			weight1Text = "3321 ips";
			grip1Text = "16504";

			power2Text = "850 bhp";
			weight2Text = "3655 ips";
			grip2Text = "18904";

			power3Text = "900 bhp";
			weight3Text = "4000 ips";
			grip3Text = "20000";

			Bike_1_info = "Rear wheel drive 6 -speed gearbox";
			Bike_2_info = "Rear wheel drive 7 -speed gearbox";
			Bike_3_info = "Rear wheel drive 8 -speed gearbox";

			Bikelistbutton = "BikeList";
			Settingbutton = "Setting";
			Racebutton = "Race";

			SettingFont = "Setting";
			Music = "Music";
			Sound = "Sound";
			Graphics = "Graphics";
			Control = "Control";
			Advanced = "Advanced";
			Lang = "Language";

			ControlText = "Control";
			Steering = "Steering";
			Throttle = "Throttle";
			Gear = "Gear";
			InvertControl = "Invert Controls";
			DetachBreak = "Detach Breaks";
			Sensivity = "Sensivity";
			preview = "Preview";
			calibrate = "Calibrate";
			restOfDefault = "Rest Of Defaults";

			AdvancedText = "Advanced";
			Speed = "Speed unit";
			wheather = "Weather Effects";
			driveleft = "Drive On Left";
			blood = "Blood Effect";

			India = "INDIA";
			England = "ENGLAND";
			Canada = "CANADA";
			Brazil = "BRAZIL";
			Portgal = "PORTUGAL";
			Spain = "SPAIN";
			Usa = "USA";
		}
		back_Forword_Button_click.localizationFontchange = true;
		Debug.Log ("local...." + str + "   " + power);
	}
}
