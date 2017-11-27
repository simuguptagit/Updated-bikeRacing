using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WrongDirection : MonoBehaviour {
	List<Vector3> BikePointsPosition = new List<Vector3>();
	public GameObject wrng;
	private bool Wrongtimer = false;
	// Use this for initialization
	void Start () {
		BikePointsPosition.Clear ();
		//GameObject.Find("Canvas/Wrong").SetActive(false);
	//	lastPosition = transform.position;
		//StartCoroutine (StartWrongDirectiontimer ());
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	IEnumerator StartWrongDirectiontimer(){
		yield return new WaitForSeconds (.8f);
		wrng.SetActive(false);
	}

	void Adding_Position(Vector3 v){
		Debug.Log ("how many times..."+v+"  "+BikePointsPosition+"   "+BikePointsPosition.Contains (v));
		if (BikePointsPosition.Contains (v)) {
			wrng.SetActive (true);
			StartCoroutine (StartWrongDirectiontimer ());
		}
		else
			BikePointsPosition.Add(v);
	}
	IEnumerator Starttimer(){
		yield return new WaitForSeconds (1.2f);
		Wrongtimer = false;
		Debug.Log ("timerrr...");
	}
	void OnTriggerEnter(Collider col){
		Debug.Log ("collision111..."+col.gameObject.name+"   "+BikePointsPosition+"  "+col.gameObject.transform.position );
		if (col.gameObject.name == "collision_point_1" && Wrongtimer ==false) {	
			//Debug.Log ("collision2222..."+col.gameObject.name+"   "+BikePointsPosition+"  "+col.gameObject.transform.position );
			Wrongtimer = true;
			StartCoroutine (Starttimer ());
			Adding_Position (col.gameObject.transform.position);
				
		}
		if (col.gameObject.name == "collision_point_2"&& Wrongtimer ==false) {
			Wrongtimer = true;
			StartCoroutine (Starttimer ());
			Adding_Position (col.gameObject.transform.position);
		}
		if (col.gameObject.name == "collision_point_3"&& Wrongtimer ==false) {
			Wrongtimer = true;
			StartCoroutine (Starttimer ());
			Adding_Position (col.gameObject.transform.position);
		}
		if (col.gameObject.name == "collision_point_4"&& Wrongtimer ==false) {
			Wrongtimer = true;
			StartCoroutine (Starttimer ());
			Adding_Position (col.gameObject.transform.position);
		}
		if (col.gameObject.name == "collision_point_5"&& Wrongtimer ==false) {
			Wrongtimer = true;
			StartCoroutine (Starttimer ());
			Adding_Position (col.gameObject.transform.position);
		}
		if (col.gameObject.name == "collision_point_6"&& Wrongtimer ==false) {
			Wrongtimer = true;
			StartCoroutine (Starttimer ());
			Adding_Position (col.gameObject.transform.position);
		}
		if (col.gameObject.name == "collision_point_7"&& Wrongtimer ==false) {
			Wrongtimer = true;
			StartCoroutine (Starttimer ());
			Adding_Position (col.gameObject.transform.position);
		}
		if (col.gameObject.name == "collision_point_8"&& Wrongtimer ==false) {
			Wrongtimer = true;
			StartCoroutine (Starttimer ());
			Adding_Position (col.gameObject.transform.position);
		}
		if (col.gameObject.name == "collision_point_9"&& Wrongtimer ==false) {
			Wrongtimer = true;
			StartCoroutine (Starttimer ());
			Adding_Position (col.gameObject.transform.position);
		}
		if (col.gameObject.name == "collision_point_10"&& Wrongtimer ==false) {
			Wrongtimer = true;
			StartCoroutine (Starttimer ());
			Adding_Position (col.gameObject.transform.position);
		}
	}
}
