
var GUIDashboard : Texture2D;
var dashboardArrow : Texture2D;
private var topSpeed: float;
private var stopAngle: float;
private var topSpeedAngle: float;
var speed: float;


var chronoTex: Texture2D;
private var topRPM: float;
private var stopRPMAngle: float;
private var topRPMAngle: float;
var RPM: float;


var linkToBike : BikeMainLogic;


function Start () {
	linkToBike = GameObject.FindGameObjectWithTag("Bike").GetComponent("BikeMainLogic");
 	findCurrentBike();
}


function FixedUpdate(){
 	speed = linkToBike.bikeSpeed;
 	RPM = linkToBike.EngineRPM;
}
function findCurrentBike(){
	var curBikeName : GameObject;
	curBikeName = GameObject.Find("rigid_bike");
	if (curBikeName != null){
		SetSpeedometerSettings("sport");
		return;
	}
	
}
function SetSpeedometerSettings(par : String){
	if (par == "sport"){
		topSpeed = 210;
		stopAngle = -215;
		topSpeedAngle = 0;
		topRPM = 12000;
		stopRPMAngle = -200;
		topRPMAngle = 0;
		yield WaitForSeconds(0.5);	
		var linkToBike1 = GameObject.Find("rigid_bike").GetComponent("bikelogic");
		linkToBike = linkToBike1;
	}
}