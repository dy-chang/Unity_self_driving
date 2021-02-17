using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Self_Car : MonoBehaviour {
	public Rigidbody car;
	public GameObject centerOfMass;

	private float throttle;// = Input.GetAxis("Vertical");
	private float steer;// = Input.GetAxis("Horizontal");
	private bool isSteering;
	private bool isThrottle;
	public float tempSteer;
	public float calculatedSteer;
	public WheelCollider frontLwheelColl;
	public WheelCollider frontRwheelColl;
	public WheelCollider backLwheelColl;
	public WheelCollider backRwheelColl;
	public float distanceCounter;
	public bool isAutoPilot;
	public Transform locationObject;
	public GameObject frontRightTireMesh;
	public GameObject frontLeftTireMesh;
    private GameObject goalOfCar;
    public Text mphText;
    private float carsTorque;
  

    // Use this for initialization
    void Start () {
		isAutoPilot=false;
		car.centerOfMass = centerOfMass.transform.localPosition;//GameObject.Find ("CenterOfGravity").transform.localPosition;
		tempSteer = 0;
		distanceCounter = 0;
        goalOfCar = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        carsTorque = 15000;
	
	}
	
	// Update is called once per frame
	void Update () {
     
        
        
        
		frontLeftTireMesh.transform.localEulerAngles = new Vector3 (frontLeftTireMesh.transform.localEulerAngles.x, frontLwheelColl.steerAngle - frontLeftTireMesh.transform.localEulerAngles.z, frontLeftTireMesh.transform.localEulerAngles.z);
        frontRightTireMesh.transform.localEulerAngles = new Vector3(frontRightTireMesh.transform.localEulerAngles.x, frontRwheelColl.steerAngle - frontRightTireMesh.transform.localEulerAngles.z, frontRightTireMesh.transform.localEulerAngles.z);

        if (Input.GetKeyDown (KeyCode.P)) {
			isAutoPilot = !isAutoPilot;
				}
		distanceCounter = distanceCounter + (Mathf.Abs (frontLwheelColl.rpm)* 2*Mathf.PI * frontLwheelColl.radius/60*3.2808f)*Time.deltaTime;
		throttle = Input.GetAxis("Vertical");
		steer = Input.GetAxis("Horizontal");
		calculatedSteer =  -23798.0f *Mathf.Pow (steer, 5) - 106656.0f * Mathf.Pow (steer, 4) + 9929.8f * Mathf.Pow (steer, 3) + 113753.0f * Mathf.Pow (steer, 2) + 97408.0f * steer;
		centerOfMass.transform.localPosition = new Vector3 (steer, centerOfMass.transform.localPosition.y, centerOfMass.transform.localPosition.z);

		if (Input.GetKey (KeyCode.UpArrow) || Input.GetKey (KeyCode.DownArrow)) {
						isThrottle = true;
						//Debug.Log (throttle);
						//car.AddForce (car.transform.forward*40 * throttle, ForceMode.Acceleration);
				} else {
			isThrottle = false;
				}
		if (Input.GetKey (KeyCode.LeftArrow) || Input.GetKey (KeyCode.RightArrow) || steer!=0.0f) {
						isSteering = true;
	//		Debug.Log (steer);
					//	car.AddTorque (new Vector3 (0, 400 * steer, 0), ForceMode.Force);
				} else {
			isSteering = false;
				}
		if (Input.GetKeyDown (KeyCode.H)) {
			Debug.Log (distanceCounter + "total miles advanced!");


	// Add Speed on Dash board
				}
        mphText.text =(Mathf.Abs(frontLwheelColl.rpm *60) * 2 * Mathf.PI * frontLwheelColl.radius  * 0.000621371f).ToString("0.00") + " MPH" + System.Environment.NewLine + distanceCounter + "distance advanced feet";




    }
	void FixedUpdate(){

		if (isThrottle) {
		//	car.AddForce (car.transform.forward*40 * -throttle, ForceMode.Acceleration);
			frontLwheelColl.motorTorque = throttle*5000;
            frontRwheelColl.motorTorque = throttle*5000;
            backLwheelColl.motorTorque = throttle * 15000;
            backRwheelColl.motorTorque = throttle * 15000;
		//	Debug.Log ((frontLwheelColl.rpm * 2*Mathf.PI * frontLwheelColl.radius*60/1000)*.621371f);
		//	backLwheelColl.motorTorque = throttle*600;
		//	backRwheelColl.motorTorque = throttle*600;

				}
		if (isSteering) {
			frontLwheelColl.steerAngle = steer*100;
			frontRwheelColl.steerAngle = steer*100;
		//	car.AddTorque (new Vector3(0,calculatedSteer,0), ForceMode.Force);
				}
		if (isAutoPilot) {
//			frontLwheelColl.steerAngle = Vector3.Angle ( frontLwheelColl.transform.forward, locationObject.transform.position);
//			frontRwheelColl.steerAngle = Vector3.Angle ( frontLwheelColl.transform.forward, locationObject.transform.position);
//			frontLwheelColl.motorTorque = throttle*600;
//			frontRwheelColl.motorTorque = throttle*600;


				// now we just find the relative position of the waypoint from the car transform,
				// that way we can determine how far to the left and right the waypoint is.
			Vector3 RelativeWaypointPosition;
			RelativeWaypointPosition = transform.InverseTransformPoint(new Vector3( locationObject.transform.position.x, 
			                                                                       transform.position.y, 
			                                                                       locationObject.transform.position.z));
            goalOfCar.transform.position = RelativeWaypointPosition;
			//transform.InverseTransformPoint( Vector3( 
				                                                                       //           locationObject.transform.position.x, 
				                                                                        //          transform.position.y, 
			                                                                   // locationObject.transform.position.z ) );
			
			
			// by dividing the horizontal position by the magnitude, we get a decimal percentage of the turn angle that we can use to drive the wheels
				float inputSteer = RelativeWaypointPosition.x / RelativeWaypointPosition.magnitude;
		//	frontLwheelColl.steerAngle = inputSteer * 5.0f;
		//	frontRwheelColl.steerAngle = inputSteer * 5.0f;
            frontLwheelColl.steerAngle = 180/Mathf.PI* inputSteer;
            frontRwheelColl.steerAngle = 180 / Mathf.PI * inputSteer;
            //Accelerate as well:
            backLwheelColl.motorTorque = carsTorque;
            backRwheelColl.motorTorque = carsTorque;
      //      frontLwheelColl.motorTorque = 1 * 5000;
      //      frontRwheelColl.motorTorque = 1 * 5000;
    //        Debug.Log(frontRwheelColl.rpm + "front R wheel rpms");
       //     Debug.Log(frontLwheelColl.rpm + "front L wheel rpms");
       //     Debug.Log(backRwheelColl.rpm + "back R wheel rpms");
        //    Debug.Log(backLwheelColl.rpm + "back L wheel rpms");

            // now we do the same for torque, but make sure that it doesn't apply any engine torque when going around a sharp turn...
            if ( Mathf.Abs( inputSteer ) < 0.5 ) {
					float inputTorque = RelativeWaypointPosition.z / RelativeWaypointPosition.magnitude - Mathf.Abs( inputSteer );
		//		frontLwheelColl.motorTorque = inputTorque;
		//		frontRwheelColl.motorTorque = inputTorque;
				}else{
				float inputTorque = 0;
			//	frontLwheelColl.motorTorque = 0;
			//	frontRwheelColl.motorTorque = 0;
			}
			
				// this just checks if the car's position is near enough to a waypoint to count as passing it, if it is, then change the target waypoint to the
				// next in the list.
	//			if ( RelativeWaypointPosition.magnitude < 20 ) {
	//				currentWaypoint ++;
					
	//				if ( currentWaypoint >= waypoints.length ) {
	//					currentWaypoint = 0;
	//				}
				}
				
			}
    public void ChangeTorque(float changeTorqueTo)
    {
        carsTorque = changeTorqueTo;

    }
    public void ApplyBrakeTorque(float BrakeTorque)
    {
        frontLwheelColl.brakeTorque = BrakeTorque;
        frontRwheelColl.brakeTorque = BrakeTorque;
        backLwheelColl.brakeTorque = BrakeTorque;
        backRwheelColl.brakeTorque = BrakeTorque;

    }



}

