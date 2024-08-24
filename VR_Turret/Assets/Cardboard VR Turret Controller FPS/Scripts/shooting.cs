using UnityEngine;
using System.Collections;

public class shooting : MonoBehaviour {

	// Use this for initialization
	public AudioSource audio;
	public GameObject prefab;
	public float speed=40.0f;
	public float timeBetwShots=0.5f;
	public bool weapons_hot;
	public Transform[] weaponBarrel;
	public float elapsed_time;	
	public Transform w1,w2;
	public float rotSpeed=0, maxRotSpeed=10;
	public bool weaponOn=false;
	public UnityEngine.UI.Image[] heatImages;

	[Range(10.0f, 20.0f)]
	public float heatingTime;

	[Range(0.0f, 10.0f)]
	public float bulletDispersion;

	Light L1,L2;
	Component h1,h2;
	float doubleClick;
	float sootingStart;
	int barNumber;

	bool rightShoot=false;
			
	void Start () 
	{
		elapsed_time=0;

		//lighting effects
		L1=weaponBarrel[0].GetComponent<Light>();
		L2=weaponBarrel[1].GetComponent<Light>();
		h1=weaponBarrel[0].GetComponent("Halo");
		h2=weaponBarrel[1].GetComponent("Halo");
		barNumber=heatImages.Length;

	}
	
	// Update is called once per frame
	void FixedUpdate () 
	{
	
		elapsed_time+=Time.fixedDeltaTime ;


		//barrel rotation
		w1.localRotation=w1.localRotation*Quaternion.Euler(0,0,rotSpeed*Time.fixedDeltaTime);
		w2.localRotation=w2.localRotation*Quaternion.Euler(0,0,rotSpeed*Time.fixedDeltaTime+60);


		//check inter-bullet time
		if(elapsed_time >= timeBetwShots)
		{
			weapons_hot=false;

		}
		else
		{
			weapons_hot=true;
		}

		//lighting effects
		L1.enabled=false;
		L2.enabled=false;
		h1.GetType().GetProperty("enabled").SetValue(h1, false, null);
		h2.GetType().GetProperty("enabled").SetValue(h2, false, null);


		if( Input.GetMouseButton(0) && Time.fixedTime-doubleClick>1.2f)
		{
			doubleClick=Time.fixedTime;
			if(weaponOn==true)
			{
				weaponOn=false;
				audio.time=3.3f;
				//change heating bars
				for(int jj=0;jj<barNumber;jj++)
				{
						heatImages[jj].enabled=false;

				}

			}
			else
			{
				weaponOn=true;
				sootingStart=Time.fixedTime;
				audio.Play();
				audio.time=0f;
			}
		}


		if(	weapons_hot==false & weaponOn==true )
		{
			GameObject proyectil;


			rotSpeed+=maxRotSpeed/50;
			if(rotSpeed>maxRotSpeed)
			{
				rotSpeed=maxRotSpeed;
			}



			if(rotSpeed==maxRotSpeed)
			{

				Quaternion randomRot= Quaternion.Euler(Random.Range(-bulletDispersion,bulletDispersion),Random.Range(-bulletDispersion,bulletDispersion),Random.Range(-bulletDispersion,bulletDispersion));

				if (rightShoot==true)
				{
					proyectil=GameObject.Instantiate(prefab,weaponBarrel[0].position,weaponBarrel[0].rotation*randomRot) as GameObject;
					rightShoot=false;
					L1.enabled=true;
					h1.GetType().GetProperty("enabled").SetValue(h1, true, null);
			
				}

				else
				{
					proyectil=Instantiate(prefab,weaponBarrel[1].position,weaponBarrel[1].rotation*randomRot) as GameObject;
					rightShoot=true;
					L2.enabled=true;
					h2.GetType().GetProperty("enabled").SetValue(h2, true, null);
				}

						
				Rigidbody rb=proyectil.GetComponent<Rigidbody>();
				rb.velocity=rb.transform.forward*speed;					

				elapsed_time=0;
			}


			//restart sound while shooting
			if(audio.time>2.8f)
			{
				audio.time=1.0f;
			}


			// check weapon temperature
			if(Time.fixedTime-sootingStart>heatingTime)
			{
				weaponOn=false;
				audio.time=3.3f;

				//change heating bars
				for(int jj=0;jj<barNumber;jj++)
				{
						heatImages[jj].enabled=false;

				}

			}
			else
			{
				//change heating bars
				for(int jj=0;jj<barNumber;jj++)
				{
					if(Time.fixedTime-sootingStart>(jj)*heatingTime/barNumber)
					{
						heatImages[jj].enabled=true;
					}
				}
			}


		}
		else
		{
			
			rotSpeed-=maxRotSpeed/50;
			if(rotSpeed<0)
			{
				rotSpeed=0;
			
			}
			
		}
		
	}
}
