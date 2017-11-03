using UnityEngine;
using System.Collections;

public class MainMenuHandling : MonoBehaviour {
	private GameObject MainPanel, BikeSelection, LevelSelection;
	//public static int BikeNo = 1;
	private GameObject bike1sel, bike2sel, bike3sel;
	// Use this for initialization
	void Start () {
		MainPanel = GameObject.Find ("UI Root/MainPanel");
		BikeSelection=GameObject.Find ("UI Root/BikeSelection");
		LevelSelection=GameObject.Find ("UI Root/LevelSelection");
		bike1sel = GameObject.Find ("UI Root/BikeSelection/bike1sel") ;
		bike2sel = GameObject.Find ("UI Root/BikeSelection/bike2sel") ;
		bike3sel = GameObject.Find ("UI Root/BikeSelection/bike3sel");
		BikeSelection.SetActive (false);
		LevelSelection.SetActive (false);
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.Escape)) {

			if (NGUITools.GetActive (MainPanel))
				Application.Quit ();
			else if (NGUITools.GetActive (BikeSelection)) {
				BikeSelection.SetActive (false);
				MainPanel.SetActive (true);
			}
			else if (NGUITools.GetActive (LevelSelection)) {
				BikeSelection.SetActive (true);
			
			}
		}
	}
	public void OnPlayClick()
	{
		//Debug.Log ("on play click");
		MainPanel.SetActive (false);
	
		BikeSelection.SetActive (true);
		//Application.LoadLevel ("Level2");
	}
	public void OnBike1Click()
	{
		GameController.BikeNo = 1;
		bike1sel.GetComponent<UILabel>().text = "Selected";
		bike2sel.GetComponent<UILabel>().text = "NotSelected";
		bike3sel.GetComponent<UILabel>().text = "NotSelected";
	}
	public void OnBike2Click()
	{
		GameController.BikeNo = 2;
		bike2sel.GetComponent<UILabel>().text = "Selected";
		bike1sel.GetComponent<UILabel>().text = "NotSelected";
		bike3sel.GetComponent<UILabel>().text = "NotSelected";
	}
	public void OnBike3Click()
	{
		bike3sel.GetComponent<UILabel>().text = "Selected";
		bike1sel.GetComponent<UILabel>().text = "NotSelected";
		bike2sel.GetComponent<UILabel>().text = "NotSelected";
		GameController.BikeNo = 3;
	}
	public void OnNextClick()
	{
		BikeSelection.SetActive (false);
		LevelSelection.SetActive(true);
	}
	public void Level1Click()
	{
		GameController.LevelNo = 1;
		Application.LoadLevel ("Level1");
	}
	public void Level2Click()
	{
		GameController.LevelNo = 2;

		Application.LoadLevel ("Level2");
	}
	public void Level3Click()
	{
		GameController.LevelNo = 3;
		Application.LoadLevel ("Level3");
	}
}
