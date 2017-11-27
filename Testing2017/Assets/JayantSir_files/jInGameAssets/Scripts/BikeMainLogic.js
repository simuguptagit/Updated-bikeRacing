#pragma strict

var coll_frontWheel : WheelCollider;
var coll_rearWheel : WheelCollider;

var meshFrontWheel : GameObject;
var meshRearWheel : GameObject;

private var isFrontWheelInAir : boolean = true;
private var stiffPowerGain : float = 0.0;
private var Collision_position : Vector3;

private var tmpMassShift : float = 0.0;
private var counter : int = 0;

public var crashed : boolean = false;

var crashAngle01: float;											
var crashAngle02: float;											
var crashAngle03: float;											
var crashAngle04: float;											
											

var CoM : Transform; 
var normalCoM : float; 							
var CoMWhenCrahsed : float; 												




var rearPendulumn : Transform; 
var steeringWheel : Transform; 
var suspensionFront_down : Transform; 
private var normalFrontSuspSpring : int; 
private var normalRearSuspSpring : int; 
private var forgeBlocked : boolean  = true;


private var baseDistance : float; 


var wheelbarRestrictCurve : AnimationCurve = new AnimationCurve(new Keyframe(0f, 20f), new Keyframe(100f, 1f));


private var tempMaxWheelAngle : float;


private var wheelPossibleAngle : float = 0.0;


private var wheelCCenter : Vector3;
private var hit : RaycastHit;


static var bikeSpeed : float;
static var isReverseOn : boolean = false; 
// Engine
var frontBrakePower : float; 																	
var EngineTorque : float; 

var airRes : float; 																									

var MaxEngineRPM : float; 							
var EngineRedline : float; 																											
var MinEngineRPM : float; 																
static var EngineRPM : float; 

var GearRatio: float[];                                                                                              	

var CurrentGear : int = 0; 

private var ctrlHub : GameObject;
private var outsideControls : GameController;
public var gamepathslider :UI.Image;

private var ESP : boolean = false;

var StartRace:boolean=false;
var bikeparticle:GameObject;

var speedText:GameObject;
var textField:UI.Text;
var speedometerRot:GameObject ;
var pathslider:GameObject ;
 var oldMax:float=100;//100;
 var newMax:float=126;//126
 var factor:float;

function Start () {
 
	ctrlHub = GameObject.Find("gameScenario");
	bikeparticle=transform.Find("bikeparticle").gameObject;
	speedText=GameObject.Find("Canvas/speedometer/speedText");
	textField=speedText.GetComponent(UI.Text);
	bikeparticle.SetActive(false);
	outsideControls = ctrlHub.GetComponent(GameController);

    pathslider = GameObject.Find ("Canvas/pathgameobject/pathslider");
    gamepathslider = pathslider.GetComponent(UI.Image);
	gamepathslider.fillAmount = 0;

	speedometerRot = GameObject.Find ("Canvas/speedometer/speedometerrot");

	  factor =newMax/oldMax;
	var setInitialTensor : Vector3 = GetComponent.<Rigidbody>().inertiaTensor;
	GetComponent.<Rigidbody>().centerOfMass = Vector3(CoM.localPosition.x, CoM.localPosition.y, CoM.localPosition.z);
	GetComponent.<Rigidbody>().inertiaTensor = setInitialTensor;
	

	meshFrontWheel.GetComponent.<Renderer>().material.color = Color.black;
	meshRearWheel.GetComponent.<Renderer>().material.color = Color.black;
	

	GetComponent.<Rigidbody>().interpolation = RigidbodyInterpolation.Interpolate;
	

	EngineTorque = EngineTorque * 20;
	

	frontBrakePower = frontBrakePower * 30;
	

	normalRearSuspSpring = coll_rearWheel.suspensionSpring.spring;
	normalFrontSuspSpring = coll_frontWheel.suspensionSpring.spring;
	
	baseDistance = coll_frontWheel.transform.localPosition.z - coll_rearWheel.transform.localPosition.z;
}

function FixedUpdate (){
if(!StartRace)
	GetComponent.<Rigidbody>().velocity=Vector3.zero;

	EngineRPM = coll_rearWheel.rpm * GearRatio[CurrentGear];
	if (EngineRPM > EngineRedline){
		EngineRPM = MaxEngineRPM;
	}
	ShiftGears();

	ESP = outsideControls.ESPMode;

	ApplyLocalPositionToVisuals(coll_frontWheel);
	ApplyLocalPositionToVisuals(coll_rearWheel);
 	
 	

 
 	rearPendulumn.transform.localRotation.eulerAngles.x = 0-8+(meshRearWheel.transform.localPosition.y*100);

	suspensionFront_down.transform.localPosition.y =(meshFrontWheel.transform.localPosition.y - 0.15);
	meshFrontWheel.transform.localPosition.z = meshFrontWheel.transform.localPosition.z - (suspensionFront_down.transform.localPosition.y + 0.4)/5;


	meshFrontWheel.GetComponent.<Renderer>().material.color = Color.black;
	meshRearWheel.GetComponent.<Renderer>().material.color = Color.black;
	

	if (!crashed){
		GetComponent.<Rigidbody>().drag = GetComponent.<Rigidbody>().velocity.magnitude / 210  * airRes; 
		GetComponent.<Rigidbody>().angularDrag = 7 + GetComponent.<Rigidbody>().velocity.magnitude/20;
	}
	

	bikeSpeed = Mathf.Round((GetComponent.<Rigidbody>().velocity.magnitude * 3.6)*10) * 0.1; 
    
     var z:float=(bikeSpeed*-factor)+63f;
 
    speedometerRot.GetComponent(RectTransform).rotation.eulerAngles.z=z;//Mathf.Clamp(z,-63f,63f);

    textField.text=(Mathf.FloorToInt(bikeSpeed)).ToString();

		if (!crashed && outsideControls.Vertical >0 && !isReverseOn){//Debug.Log ("BikeMainLogic21212121....");
			coll_frontWheel.brakeTorque = 0;
			coll_rearWheel.motorTorque = EngineTorque * outsideControls.Vertical;

		
			CoM.localPosition.z = 0.0 + tmpMassShift;
			CoM.localPosition.y = normalCoM;
			GetComponent.<Rigidbody>().centerOfMass = Vector3(CoM.localPosition.x, CoM.localPosition.y, CoM.localPosition.z);
		}

		if (!crashed && outsideControls.Vertical >0 && isReverseOn){Debug.Log ("BikeMainLogic20202020....");
			coll_rearWheel.motorTorque = EngineTorque * -outsideControls.Vertical/10+(bikeSpeed*50);

		
			CoM.localPosition.z = 0.0 + tmpMassShift;
			CoM.localPosition.y = normalCoM;
			GetComponent.<Rigidbody>().centerOfMass = Vector3(CoM.localPosition.x, CoM.localPosition.y, CoM.localPosition.z);
		}
		

		if (!crashed && outsideControls.Vertical >0.9 && !isReverseOn){//Debug.Log ("BikeMainLogic19191919....");
			coll_frontWheel.brakeTorque = 0;
			coll_rearWheel.motorTorque = EngineTorque * 1.2;

			GetComponent.<Rigidbody>().angularDrag  = 20;
			
		
			if (!ESP){
				CoM.localPosition.z = -(2-baseDistance/1.4) + tmpMassShift;

				var stoppieEmpower : float = (bikeSpeed/3)/100;
			
				if (this.transform.localEulerAngles.z < 70){	
					var angleLeanCompensate = this.transform.localEulerAngles.z/30;
						if (angleLeanCompensate > 0.5){
							angleLeanCompensate = 0.5;
						}
				}
				if (this.transform.localEulerAngles.z > 290){
					angleLeanCompensate = (360-this.transform.localEulerAngles.z)/30;
						if (angleLeanCompensate > 0.5){
							angleLeanCompensate = 0.5;
						}
				}
					
				if (stoppieEmpower + angleLeanCompensate > 0.5){
					stoppieEmpower = 0.5;
				}
				CoM.localPosition.y =  -(1 - baseDistance/2.8) - stoppieEmpower ;
				CoM.localPosition.y = CoM.localPosition.y += 0.002;
			}
			GetComponent.<Rigidbody>().centerOfMass = Vector3(CoM.localPosition.x, CoM.localPosition.y, CoM.localPosition.z);
		

			if (this.transform.localEulerAngles.x > 200.0){
				coll_rearWheel.suspensionSpring.spring = normalRearSuspSpring + (360-this.transform.localEulerAngles.x)*500;
				if (coll_rearWheel.suspensionSpring.spring >= normalRearSuspSpring + 20000) coll_rearWheel.suspensionSpring.spring = normalRearSuspSpring + 20000;
			}
		} else RearSuspensionRestoration();
		

		if (!crashed && outsideControls.Vertical <0 && !isFrontWheelInAir){//Debug.Log ("BikeMainLogic18181818....");

			coll_frontWheel.brakeTorque = frontBrakePower * -outsideControls.Vertical;
			coll_rearWheel.motorTorque = 0; 

			if (!ESP){
				if (bikeSpeed >1){
					
			
					if(outsideControls.rearBrakeOn){
						var rearBrakeAddon = 0.0025;
					}
					CoM.localPosition.y = CoM.localPosition.y += (frontBrakePower/200000)+tmpMassShift/50-rearBrakeAddon;
					
					CoM.localPosition.z = CoM.localPosition.z += 0.05;
				} 	
					else if (bikeSpeed <=1 && !crashed && this.transform.localEulerAngles.z < 45 || bikeSpeed <=1 && !crashed && this.transform.localEulerAngles.z >315){
							if (this.transform.localEulerAngles.x < 5 || this.transform.localEulerAngles.x > 355){
								CoM.localPosition.y = normalCoM;
							}
						}
		
				if (CoM.localPosition.y >= -0.1) CoM.localPosition.y = -0.1;
				
				if (CoM.localPosition.z >= 0.2+(GetComponent.<Rigidbody>().mass/1100)) CoM.localPosition.z = 0.2+(GetComponent.<Rigidbody>().mass/1100);
				

				var maxFrontSuspConstrain : float;
				maxFrontSuspConstrain = CoM.localPosition.z;
				if (maxFrontSuspConstrain >= 0.5) maxFrontSuspConstrain = 0.5;
				var springWeakness : int  = normalFrontSuspSpring-(normalFrontSuspSpring*1.5) * maxFrontSuspConstrain;
				
			}
			GetComponent.<Rigidbody>().centerOfMass = Vector3(CoM.localPosition.x, CoM.localPosition.y, CoM.localPosition.z);
		
			meshFrontWheel.GetComponent.<Renderer>().material.color = Color.red;
			

			forgeBlocked = true;
		} else FrontSuspensionRestoration(springWeakness);
			
		

		if (!crashed && outsideControls.rearBrakeOn){//Debug.Log ("BikeMainLogic17171717....");
			coll_rearWheel.brakeTorque = frontBrakePower / 2;
			
			if (this.transform.localEulerAngles.x > 180 && this.transform.localEulerAngles.x < 350){
				CoM.localPosition.z = 0.0 + tmpMassShift;
			}
			
			coll_frontWheel.sidewaysFriction.stiffness = 1.0 - ((stiffPowerGain/2)-tmpMassShift*3);
			
			stiffPowerGain = stiffPowerGain += 0.025 - (bikeSpeed/10000);
				if (stiffPowerGain > 0.9 - bikeSpeed/300){ 
					stiffPowerGain = 0.9 - bikeSpeed/300;
				}
			coll_rearWheel.sidewaysFriction.stiffness = 1.0 - stiffPowerGain;
			meshRearWheel.GetComponent.<Renderer>().material.color = Color.red;
			
		} else{//Debug.Log ("BikeMainLogic16161616....");

			coll_rearWheel.brakeTorque = 0;

			stiffPowerGain = stiffPowerGain -= 0.05;
				if (stiffPowerGain < 0){
					stiffPowerGain = 0;
				}
			coll_rearWheel.sidewaysFriction.stiffness = 1.0 - stiffPowerGain;
			coll_frontWheel.sidewaysFriction.stiffness = 1.0 - stiffPowerGain;
			
		}
		

		if (!crashed && outsideControls.reverse && bikeSpeed <=0){//Debug.Log ("BikeMainLogic15151515....");
				outsideControls.reverse = false;
				if(isReverseOn == false){
				isReverseOn = true;
				} else isReverseOn = false;
		}
			
		

		tempMaxWheelAngle = wheelbarRestrictCurve.Evaluate(bikeSpeed);
		
		if (!crashed && outsideControls.Horizontal !=0){	
	         //Debug.Log ("BikeMainLogic14141414....");
			
			coll_frontWheel.steerAngle = tempMaxWheelAngle * outsideControls.Horizontal;

			steeringWheel.rotation = coll_frontWheel.transform.rotation * Quaternion.Euler (0, coll_frontWheel.steerAngle, coll_frontWheel.transform.rotation.z);
		} else coll_frontWheel.steerAngle = 0;
		
		


		if (outsideControls.VerticalMassShift >0){//Debug.Log ("BikeMainLogic13131313....");
			tmpMassShift = outsideControls.VerticalMassShift/12.5;
			CoM.localPosition.z = tmpMassShift;	
			GetComponent.<Rigidbody>().centerOfMass = Vector3(CoM.localPosition.x, CoM.localPosition.y, CoM.localPosition.z);
		}
		if (outsideControls.VerticalMassShift <0){//Debug.Log ("BikeMainLogic12121212....");
			tmpMassShift = outsideControls.VerticalMassShift/12.5;
			CoM.localPosition.z = tmpMassShift;
			GetComponent.<Rigidbody>().centerOfMass = Vector3(CoM.localPosition.x, CoM.localPosition.y, CoM.localPosition.z);
		}
		if (outsideControls.HorizontalMassShift <0){//Debug.Log ("BikeMainLogic1010101....");
			CoM.localPosition.x = outsideControls.HorizontalMassShift/40;
			GetComponent.<Rigidbody>().centerOfMass = Vector3(CoM.localPosition.x, CoM.localPosition.y, CoM.localPosition.z);
			
		}
		if (outsideControls.HorizontalMassShift >0){//Debug.Log ("BikeMainLogic9999....");
			CoM.localPosition.x = outsideControls.HorizontalMassShift/40;
			GetComponent.<Rigidbody>().centerOfMass = Vector3(CoM.localPosition.x, CoM.localPosition.y, CoM.localPosition.z);
		}

		if (!crashed && outsideControls.Vertical == 0 && !outsideControls.rearBrakeOn || (outsideControls.Vertical < 0 && isFrontWheelInAir)){
		//Debug.Log ("BikeMainLogic8888....");
			CoM.localPosition.y = normalCoM;
			CoM.localPosition.z = 0.0 + tmpMassShift;
			coll_frontWheel.motorTorque = 0;
			coll_frontWheel.brakeTorque = 0;
			coll_rearWheel.motorTorque = 0;
			coll_rearWheel.brakeTorque = 0;
			GetComponent.<Rigidbody>().centerOfMass = Vector3(CoM.localPosition.x, CoM.localPosition.y, CoM.localPosition.z);
		}

		if (outsideControls.VerticalMassShift == 0){//Debug.Log ("BikeMainLogic7777....");
			CoM.localPosition.z = 0.0;
			tmpMassShift = 0.0;
		}
	
		if (outsideControls.HorizontalMassShift == 0){//Debug.Log ("BikeMainLogic66666666....");
			CoM.localPosition.x = 0.0;
		}
		

		if (outsideControls.restartBike){//Debug.Log ("BikeMainLogic55555....");
			if (outsideControls.fullRestartBike){
				transform.position = Vector3(0,1,-11);
				transform.rotation=Quaternion.Euler( 0.0, 0.0, 0.0 );
			}
			crashed = false;
			transform.position+=Vector3(0,0.1,0);
			transform.rotation=Quaternion.Euler( 0.0, transform.localEulerAngles.y, 0.0 );
			GetComponent.<Rigidbody>().velocity=Vector3.zero;
			GetComponent.<Rigidbody>().angularVelocity=Vector3.zero;
			CoM.localPosition.x = 0.0;
			CoM.localPosition.y = normalCoM;
			CoM.localPosition.z = 0.0;

			coll_frontWheel.motorTorque = 0;
			coll_frontWheel.brakeTorque = 0;
			coll_rearWheel.motorTorque = 0;
			coll_rearWheel.brakeTorque = 0;
			GetComponent.<Rigidbody>().centerOfMass = Vector3(CoM.localPosition.x, CoM.localPosition.y, CoM.localPosition.z);
		}
		
		
		

	if (((this.transform.localEulerAngles.z >=crashAngle01 && this.transform.localEulerAngles.z <=crashAngle02) || (this.transform.localEulerAngles.x >=crashAngle03 && this.transform.localEulerAngles.x <=crashAngle04))&&(!crashed)){
	bikeparticle.SetActive(true);
		GetComponent.<Rigidbody>().drag = 0.1; 
		GetComponent.<Rigidbody>().angularDrag = 0.01;
		crashed = true;
		CoM.localPosition.x = 0.0;
		CoM.localPosition.y = CoMWhenCrahsed;
		CoM.localPosition.z = 0.0;
		GetComponent.<Rigidbody>().centerOfMass = Vector3(CoM.localPosition.x, CoM.localPosition.y, CoM.localPosition.z);
		 Invoke("BikeCrash",1);

	}
	
	if (crashed) coll_rearWheel.motorTorque = 0;
}


function ShiftGears() {//Debug.Log ("BikeMainLogic444444....");
	if ( EngineRPM >= MaxEngineRPM ) {
		var AppropriateGear : int = CurrentGear;
		
		for ( var i = 0; i < GearRatio.length; i ++ ) {
			if (coll_rearWheel.rpm * GearRatio[i] < MaxEngineRPM ) {
				AppropriateGear = i;
				break;
			}
		}
		
		CurrentGear = AppropriateGear;
	}
	
	if ( EngineRPM <= MinEngineRPM ) {
		AppropriateGear = CurrentGear;
		
		for ( var j = GearRatio.length-1; j >= 0; j -- ) {
			if (coll_rearWheel.rpm * GearRatio[j] > MinEngineRPM ) {
				AppropriateGear = j;
				break;
			}
		}
		CurrentGear = AppropriateGear;
	}
}
	
function ApplyLocalPositionToVisuals (collider : WheelCollider) {//Debug.Log ("BikeMainLogic3333....");
		if (collider.transform.childCount == 0) {
			return;
		}
		
		var visualWheel : Transform = collider.transform.GetChild (0);
		wheelCCenter = collider.transform.TransformPoint (collider.center);	
		if (Physics.Raycast (wheelCCenter, -collider.transform.up, hit, collider.suspensionDistance + collider.radius)) {
			visualWheel.transform.position = hit.point + (collider.transform.up * collider.radius);
			if (collider.name == "coll_front_wheel") isFrontWheelInAir = false;
			
		} else {
			visualWheel.transform.position = wheelCCenter - (collider.transform.up * collider.suspensionDistance);
			if (collider.name == "coll_front_wheel") isFrontWheelInAir = true;
		}
		var position : Vector3;
		var rotation : Quaternion;
		collider.GetWorldPose (position, rotation);

		visualWheel.localEulerAngles = Vector3(visualWheel.localEulerAngles.x, collider.steerAngle - visualWheel.localEulerAngles.z, visualWheel.localEulerAngles.z);
		visualWheel.Rotate (collider.rpm / 60 * 360 * Time.deltaTime, 0, 0);

}

function RearSuspensionRestoration (){//Debug.Log ("BikeMainLogic22222....");
	if (coll_rearWheel.suspensionSpring.spring > normalRearSuspSpring){
		coll_rearWheel.suspensionSpring.spring = coll_rearWheel.suspensionSpring.spring -= 500;
	}
}

function FrontSuspensionRestoration (sprWeakness : int){//Debug.Log ("BikeMainLogic1111....");
	if (forgeBlocked) {
		coll_frontWheel.suspensionSpring.spring = sprWeakness;
		forgeBlocked = false;
	}
	if (coll_frontWheel.suspensionSpring.spring < normalFrontSuspSpring){
		coll_frontWheel.suspensionSpring.spring = coll_frontWheel.suspensionSpring.spring += 500;
	}

	}
function OnCollisionEnter (col : Collision)
	{
	//if(GameController.GameCompleted||GameController.GameFailed)
	//return;
	/*if(col.gameObject.name == "Terrain")
    {
//    Debug.Log(col.gameObject.name);
    bikeparticle.SetActive(true);
      GetComponent.<Rigidbody>().drag = 0.1;
		GetComponent.<Rigidbody>().angularDrag = 0.01;
		crashed = true;
		CoM.localPosition.x = 0.0;
		CoM.localPosition.y = CoMWhenCrahsed;//normalCoM;
		CoM.localPosition.z = 0.0;
		GetComponent.<Rigidbody>().centerOfMass = Vector3(CoM.localPosition.x, CoM.localPosition.y, CoM.localPosition.z);
    Invoke("BikeCrash",5);
    }
	*/
	}
	function OnTriggerEnter(col:Collider){Debug.Log ("Collision.."+col.gameObject.name);
	if(GameController.GameCompleted|| GameController.GameFailed)
	return;
		if (col.gameObject.tag == "StartPoint") {
//			Debug.Log ("StartPoint");
			col.gameObject.SetActive(false);
		}
		if (col.gameObject.name == "CheckPoint") {
			//Debug.Log ("EndPont");
			 GameController.GameCompleted=true;
		     outsideControls.CallfromInGame();
		    // var currentLevel = PlayerPrefs.GetInt ("Level_Selection");
		}if(col.gameObject.name == "Spike Strip"){
		    GameController.GameFailed=true;
		     outsideControls.CallfromInGame();
		}

		if(col.gameObject.name == "collision_point_1")
		 gamepathslider.fillAmount = .1f;
		if(col.gameObject.name == "collision_point_2")
		 gamepathslider.fillAmount = .2f;
		if(col.gameObject.name == "collision_point_3")
		 gamepathslider.fillAmount = .3f;
		if(col.gameObject.name == "collision_point_4")
		 gamepathslider.fillAmount = .4f;
		if(col.gameObject.name == "collision_point_5")
		 gamepathslider.fillAmount = .5f;
		if(col.gameObject.name == "collision_point_6")
		 gamepathslider.fillAmount = .6f;
		if(col.gameObject.name == "collision_point_7")
		 gamepathslider.fillAmount = .7f;
		if(col.gameObject.name == "collision_point_8")
		 gamepathslider.fillAmount = .8f;
		if(col.gameObject.name == "collision_point_9")
		 gamepathslider.fillAmount = .9f;
		if(col.gameObject.name == "collision_point_10")
		 gamepathslider.fillAmount = 1f;
		if(col.gameObject.name == "Nitrous stand" || col.gameObject.name == "Bottle Boost")
		  { GameController.currentTime =GameController.levelTime;
		   Destroy(col.transform.parent.gameObject);
		   GameObject.Find ("Canvas/Nitros/fullFuel").GetComponent(Animator).enabled = true;
		  }
		//Debug.Log ("collision_point..."+Collision_position);
	}

	function BikeCrash()
    {
		 GameController.GameFailed=true;
		outsideControls.CallfromInGame();
	}
