using UnityEngine;
using System.Collections;

public class TurretMovement : MonoBehaviour {

	// Use this for initialization
	public float lerpSpeed=0.1f;
	public Transform bodyRotY, bodyRotZ;


	public Transform head;
	public Transform cardboard;

	bool checkTrigger;

	void Start () 
	{

	}


	
	// Update is called once per fixed frame
	void FixedUpdate () 
	{


		Quaternion relative = Quaternion.Inverse(bodyRotY.transform.rotation) * head.transform.rotation;



		//standard value [0-360]


		checkTrigger=(Input.GetMouseButton(0));


		//rotation of the second axis
		Quaternion objectiveRot=Quaternion.Euler(head.transform.rotation.eulerAngles[0]-90,head.transform.rotation.eulerAngles[1],0);
		bodyRotZ.transform.rotation=Quaternion.Lerp(bodyRotZ.transform.rotation,objectiveRot,lerpSpeed);


		//rotation of the firts axis
		objectiveRot=Quaternion.Euler(0,head.transform.rotation.eulerAngles[1]-90,90);
		bodyRotY.transform.rotation=Quaternion.Lerp(bodyRotY.transform.rotation,objectiveRot,lerpSpeed);




	}





	void restart()
	{
		Application.LoadLevel(Application.loadedLevel);
	}

}
