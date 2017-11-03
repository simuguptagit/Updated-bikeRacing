#pragma strict

var isVisibleAtStart = false;
var fadeInAtStart = false;
var fadeInDelay = 0.0;
var fadeInSpeed = 0.1;
private var textureRect : Rect;
var texture : Texture2D;
var depth = 0;

private var isVisible = true;
public var alpha = 0.0; //an alpha of 1 means the texture is opaque, 0 means completely see-through. This variable changes to fade in and out.
private var isFadingIn = false;
private var guiColor : Color; //includes gui alpha variable
private var isFadingOut = false;

function Awake () {
	textureRect=new Rect(Screen.width/2-(Screen.width*texture.width/2)/800,Screen.height/2-(Screen.height*texture.height)/480,(Screen.width*texture.width)/800,(Screen.height*texture.height)/480);
	guiColor = Color.white;
	
	if (isVisibleAtStart == false) {
		isVisible = false;
	}
}

function Start () {
//switch(this.gameObject.name)
//{
//case "three":
//
//break;
//case "two":
//
//break;
//case "one":
//
//break;
//case "go":
//
//break;
//}
	if (fadeInAtStart == true) {
		yield WaitForSeconds(fadeInDelay);
		FadeIn();
	}
	

}

function Update () {

	if (isFadingIn == true) {
		alpha = alpha + fadeInSpeed;
		if (alpha > 1) {
			//finished fading in
			alpha = 1;
			isFadingIn = false;
		}
	}
	
	if (isFadingOut == true) {
		alpha = alpha - fadeInSpeed;
		if (alpha < 0) {
			//finished fading out
			alpha = 0;
			isFadingOut = false;
		}
	}
	
}

function OnGUI() {
	
	GUI.depth = depth;
	guiColor.a = alpha;
	GUI.color = guiColor;

	if (isVisible == true) {
		//GUI.DrawTexture(textureRect, texture, ScaleMode.StretchToFill, true, 10.0f);
		GUI.DrawTexture(textureRect,texture);

	}
	
}

public function FadeIn () {

	alpha = 0;
	isFadingIn = true;
	
}

public function FadeOut () {

	alpha = 1;
	isFadingOut = true;
	
}