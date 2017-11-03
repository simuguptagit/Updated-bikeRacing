using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fishEyeView : MonoBehaviour {

	Transform myTransform;
	UIPanel myPanel;
	UIWidget myWidget;
	float cellWidth, downScale;
	float pos, dist;

	// Use this for initialization
	void Start () {
		myTransform = transform;
		myPanel = myTransform.parent.parent.GetComponent<UIPanel> ();
		myWidget = GetComponent<UIWidget> ();

		cellWidth = 150;
		downScale = .70f;
	}
	
	// Update is called once per frame
	void Update () {
		pos = myTransform.localPosition.x - myPanel.clipOffset.x;
		dist = Mathf.Clamp (Mathf.Abs(pos),0f,cellWidth);
		myWidget.width = System.Convert.ToInt32 (((cellWidth - dist * downScale) / cellWidth) * cellWidth);
	}
}
