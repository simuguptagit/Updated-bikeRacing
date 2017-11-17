using UnityEngine;
using System.Collections;

public class keyboardControls : MonoBehaviour {



	private GameObject ctrlHub;
	private GameController outsideControls;
	private GameObject cam;
	private string horizontalAccess;
	private int steeringValue = 0;
	private float senstivity;
	private bool rightArrow,leftArrow,Acceleration,brake;
	UnityEngine.UI.Image image;
	private GameObject accelerator;


	// Use this for initialization
	void Start () {
		cam = GameObject.Find ("Camera") as GameObject;
		ctrlHub = GameObject.Find("gameScenario");
		outsideControls = ctrlHub.GetComponent<GameController>();
		horizontalAccess=PlayerPrefs.GetString ("cntrl_steering");
		senstivity=PlayerPrefs.GetFloat ("sensivity");
		Debug.Log ("keyboard..."+(PlayerPrefs.GetString ("cntrl_invert").Equals("true"))+"  "+PlayerPrefs.GetString ("cntrl_invert") );
		if (horizontalAccess == "Button") {
			
			if (PlayerPrefs.GetString ("cntrl_invert").Equals("true")) 
				InvertChange (GameObject.Find("Canvas/Button_brake"),GameObject.Find("Canvas/Button_acceleration"));

			steeringValue = 1;
			GameObject.Find("Canvas/brake").SetActive(false);
			GameObject.Find("Canvas/acceleration").SetActive(false);

		} else {
			if (PlayerPrefs.GetString ("cntrl_invert").Equals("true")) 
				InvertChange (GameObject.Find ("Canvas/brake"),GameObject.Find ("Canvas/acceleration"));

			GameObject.Find("Canvas/left").SetActive(false);
			GameObject.Find("Canvas/right").SetActive(false);
			GameObject.Find("Canvas/Button_brake").SetActive(false);
			GameObject.Find("Canvas/Button_acceleration").SetActive(false);
		
			steeringValue = 0;
		}
		//Debug.Log ("steeringensivity..." + steeringValue+"  "+PlayerPrefs.GetString ("cntrl_steering"));
		accelerator = GameObject.Find ("Canvas/highAccelerator");
		image = accelerator.GetComponent<UnityEngine.UI.Image>();
	}
	public void InvertChange(GameObject Breakobj , GameObject Accelerationobj){
		Vector3 v = Breakobj.transform.position;
		Vector3 v1 = Accelerationobj.transform.position;
		Breakobj.transform.position = v1;
		Accelerationobj.transform.position = v;
	}
	public void leftArrowPointerDown()
	{
		leftArrow = true;


	}
	public void leftArrowPointerUp()
	{
		leftArrow =false;
	
	
	}

	public void rightArrowPointerDown()
	{
		rightArrow = true;

	
	}
	public void rightArrowPointerUp()
	{
		rightArrow = false;
	
	
	}
	public void brakePointerdown()
	{
		brake = true;

	
	}
	public void brakePointerUp()
	{
		brake = false;

	
	}
	public void AccelerationPointerdown()
	{
		Acceleration = true;


	}
	public void AccelerationPointerup()
	{
		Acceleration = false;


	}

	void Update () {

		if (Application.platform == RuntimePlatform.Android) {
			if (Acceleration) {
				outsideControls.Vertical = Mathf.Lerp (outsideControls.Vertical, 1, 10 * Time.deltaTime)/ 1.112f;
			} else
				outsideControls.Vertical = Mathf.Lerp (outsideControls.Vertical, 0, 10 * Time.deltaTime)/ 1.112f;
			if(brake)
				outsideControls.rearBrakeOn = true;
			else
				outsideControls.rearBrakeOn = false;
			if (steeringValue == 1) {//Debug.Log("steering.."+steeringValue+"  "+leftArrow+"  "+rightArrow+"  "+senstivity);
				if (leftArrow)
					outsideControls.Horizontal = Mathf.Lerp (outsideControls.Horizontal, -1, 20 *senstivity* Time.deltaTime)/ 1.112f;
				else if(rightArrow)
					outsideControls.Horizontal = Mathf.Lerp (outsideControls.Horizontal, 1, 20 *senstivity*Time.deltaTime)/ 1.112f;
				else
					outsideControls.Horizontal = Mathf.Lerp (outsideControls.Horizontal, 0, 20 *senstivity*Time.deltaTime)/ 1.112f;	
			}
		else
				outsideControls.Horizontal = Input.acceleration.x* 1.1f;

		} else {
			if (!Input.GetKey (KeyCode.Alpha2)) {
				//	Debug.Log ("notvertical");
				if (Input.GetAxis ("Vertical") > 0) {
					//	outsideControls.Vertical = Mathf.Lerp (outsideControls.Vertical, 1, 1 * Time.deltaTime) / 1.112f;
					outsideControls.Vertical = Input.GetAxis ("Vertical") / 1.112f;//to get less than 0.9 as acceleration to prevent wheelie(wheelie begins at >0.9)
					//				Debug.Log ("notvertical" + Input.GetAxis ("Vertical"));
				}
				if (Input.GetAxis ("Vertical") < 0) {
					outsideControls.Vertical = outsideControls.Vertical * 1.112f;
					//outsideControls.Vertical = Mathf.Lerp (outsideControls.Vertical, 0, 100 * Time.deltaTime) / 1.112f;
				}
				//need to get 1(full power) for front brake
			}

		


	
		
			///
		}
//		Debug.Log (outsideControls.Vertical);
		image.fillAmount = outsideControls.Vertical+0.11f;
	}
}
