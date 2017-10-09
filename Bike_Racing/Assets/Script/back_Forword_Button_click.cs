using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class back_Forword_Button_click : MonoBehaviour {
	public GameObject model1;
	public GameObject model2;
	public GameObject model3;

	private int change_model_no;
	// Use this for initialization
	void Start () {
		change_model_no = 0;
	}
	public void Forword(){
		if(change_model_no<2)
			change_model_no += 1;
		else
			change_model_no = 0;

		if (change_model_no == 1) {
			model3.SetActive(false);
			model1.SetActive (true);
		}else if (change_model_no == 2) {
			model1.SetActive(false);
			model2.SetActive(true);
		}
		else if (change_model_no == 0) {
			model2.SetActive(false);
			model3.SetActive(true);
		}

	}

	public void Back(){
		if(change_model_no>0)
			change_model_no -= 1;
		else
			change_model_no = 2;

		if (change_model_no == 1) {
			model2.SetActive(false);
			model1.SetActive (true);
		}else if (change_model_no == 2) {
			model3.SetActive(false);
			model2.SetActive(true);
		}
		else if (change_model_no == 0) {
			model1.SetActive(false);
			model3.SetActive(true);
		}

	}
	// Update is called once per frame
	void Update () {
		
	}
}
