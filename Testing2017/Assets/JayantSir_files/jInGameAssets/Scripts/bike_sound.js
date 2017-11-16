#pragma strict

var linkToBike : BikeMainLogic;

var lastGear : int;
private var highRPMAudio : AudioSource;
private var skidSound : AudioSource;
var Bgsound : AudioSource;
var engineStartSound : AudioClip;
var gearingSound : AudioClip;
var IdleRPM : AudioClip;
var highRPM : AudioClip;
var skid : AudioClip;


var isSkidingFront : boolean = false;
var isSkidingRear : boolean = false;

private var ctrlHub : GameObject;
private var outsideControls : GameController;


function Start () {

    Bgsound.Stop();
    Bgsound.volume =PlayerPrefs.GetFloat ("game");
    Bgsound.Play();
    Debug.Log ("mussiiccc calling..."+PlayerPrefs.GetFloat ("game")+"   "+PlayerPrefs.GetFloat ("bike"));
	ctrlHub = GameObject.Find("gameScenario");
	outsideControls = ctrlHub.GetComponent(GameController);
	
	//assign sound to audioSource
	highRPMAudio = gameObject.AddComponent(AudioSource);
	highRPMAudio.loop = true;
    highRPMAudio.playOnAwake = false;
    highRPMAudio.clip = highRPM;
    highRPMAudio.pitch = 0;
    highRPMAudio.volume = 0.0;
    //same assign for skid sound
    skidSound = gameObject.AddComponent(AudioSource);
	skidSound.loop = false;
    skidSound.playOnAwake = false;
    skidSound.clip = skid;
    skidSound.pitch = 0.5;//1.0
    skidSound.volume = PlayerPrefs.GetFloat ("bike");// 0.5;//1.0
	
 	linkToBike = this.GetComponent(BikeMainLogic);
	GetComponent.<AudioSource>().PlayOneShot(engineStartSound);
	playEngineWorkSound();
	lastGear = linkToBike.CurrentGear;
}


function Update(){
	
   if(!(GameController.GameCompleted|| GameController.GameFailed))
   {
	GetComponent.<AudioSource>().pitch = Mathf.Abs(linkToBike.EngineRPM  / linkToBike.MaxEngineRPM) + 0.5f;//1
	GetComponent.<AudioSource>().volume =  PlayerPrefs.GetFloat ("bike")- (Mathf.Abs(linkToBike.EngineRPM  / linkToBike.MaxEngineRPM));
	highRPMAudio.pitch = Mathf.Abs(linkToBike.EngineRPM  / linkToBike.MaxEngineRPM);
	highRPMAudio.volume = PlayerPrefs.GetFloat ("bike");//Mathf.Abs(linkToBike.EngineRPM  / linkToBike.MaxEngineRPM);

	if (outsideControls.restartBike){
		GetComponent.<AudioSource>().Stop();
		GetComponent.<AudioSource>().pitch = 0.5f;//1
		GetComponent.<AudioSource>().PlayOneShot(engineStartSound);
		playEngineWorkSound();
	}
	

	if (linkToBike.CurrentGear != lastGear){
		GetComponent.<AudioSource>().PlayOneShot(gearingSound);
		lastGear = linkToBike.CurrentGear;
	}

	if (linkToBike.coll_rearWheel.sidewaysFriction.stiffness < 0.5 && PlayerPrefs.GetFloat ("bike")>0&&!isSkidingRear && linkToBike.bikeSpeed >1){
			skidSound.Play();
			isSkidingRear = true;
	} else if (linkToBike.coll_rearWheel.sidewaysFriction.stiffness >= 0.5 && isSkidingRear || linkToBike.bikeSpeed <=1){
					skidSound.Stop();
				isSkidingRear = false;
	}
	if (linkToBike.coll_frontWheel.brakeTorque >= (linkToBike.frontBrakePower-10) && PlayerPrefs.GetFloat ("bike")>0&&!isSkidingFront && linkToBike.bikeSpeed >1){
			skidSound.Play();
			isSkidingFront = true;
	} else if (linkToBike.coll_frontWheel.brakeTorque < linkToBike.frontBrakePower && isSkidingFront || linkToBike.bikeSpeed <=1){
				skidSound.Stop();
				isSkidingFront = false;
	}
	}


	else

	{
//	Debug.Log("stopsound");
	highRPMAudio.pitch=0;
	highRPMAudio.volume=0;
	  skidSound.pitch = 0;//1.0
    skidSound.volume =0;
    GetComponent.<AudioSource>().pitch=0;
     GetComponent.<AudioSource>().volume=0;

	}
}

function playEngineWorkSound(){ 
	yield WaitForSeconds(1);
	GetComponent.<AudioSource>().clip = IdleRPM;
	GetComponent.<AudioSource>().volume = PlayerPrefs.GetFloat ("bike");
	GetComponent.<AudioSource>().Play();
	highRPMAudio.Play();
}