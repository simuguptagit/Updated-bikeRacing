#pragma strict

var initialDelay = 0.0;
var three : TextureScript;
var two : TextureScript;
var one : TextureScript;
var go : TextureScript;
//var blackprompt:UI2DSprite;
//private var staticClassRef:_StaticValues;
private var notStart:boolean;
var fadeInAtStart = false;
var fadeInDelay = 0.0;
var fadeInSpeed = 0.1;
private var isFadingIn = false;
public var alpha = 1.0;
private var isFadingOut = false;
private var cam:GameObject;
private var cameraSwitchView:Transform[];
private var Switch:int;
function Start () {
Switch=1;

	
}
	function StartRace(){
//	Debug.Log(GameObject.FindGameObjectWithTag ("Bike"));
	GameObject.FindGameObjectWithTag ("Bike").GetComponent(BikeMainLogic).StartRace=true;
		GameObject.FindGameObjectWithTag("GameController").GetComponent(GameController).startTimer();
		
	}

		function StartCount()
	{
	yield WaitForSeconds(initialDelay);
	three.FadeIn();
	yield WaitForSeconds(2);
	three.FadeOut();
	two.FadeIn();
	yield WaitForSeconds(2);
	two.FadeOut();
	one.FadeIn();
	yield WaitForSeconds(2);
	one.FadeOut();
	go.FadeIn();
	yield WaitForSeconds(2);
	go.FadeOut();
	yield WaitForSeconds(1);
	StartRace();
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
if(!(notStart))
{

    // blackprompt.color=Color (1, 1, 1, 1);
	//cam.transform.position = cameraSwitchView [0].position;//extra
//	cam.transform.rotation = Quaternion.Lerp (transform.rotation, cameraSwitchView [0].rotation, Time.deltaTime * 5.0f);//extra
  
notStart=true;
StartCount();
}
}
function BlackpromptfadeIn()
{
    alpha = 0;
	isFadingIn = true;
}
function BlackpromptfadeOut()
{
alpha = 1;
	isFadingOut = true;
}
