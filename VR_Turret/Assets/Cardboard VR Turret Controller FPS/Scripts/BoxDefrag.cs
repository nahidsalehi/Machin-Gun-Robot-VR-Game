using UnityEngine;
using System.Collections;

public class BoxDefrag : MonoBehaviour {


	float u;
	public Transform container;

	// Use this for initialization
	void Start () 
	{
		u=transform.localScale[0];

		if(transform.localScale.magnitude<2f)
		{
			Destroy(gameObject);
		}
	}
	
	// Update is called once per frame
	void FixedUpdate () 
	{
		
	}

	void OnCollisionEnter(Collision col)
	{
		string tag=col.gameObject.tag;



		if(tag=="laser")
		{
			Destroy(col.gameObject);

			GameObject[] a=new GameObject[8];



			a[0]=GameObject.Instantiate(gameObject, transform.position -u/4*(1*transform.forward +1*transform.right +1*transform.up)  , transform.rotation) as GameObject;
			a[1]=GameObject.Instantiate(gameObject, transform.position -u/4*(-1*transform.forward -1*transform.right +1*transform.up)  , transform.rotation)as GameObject;
			a[2]=GameObject.Instantiate(gameObject, transform.position -u/4*(-1*transform.forward +1*transform.right +1*transform.up)  , transform.rotation)as GameObject;
			a[3]=GameObject.Instantiate(gameObject, transform.position -u/4*(1*transform.forward -1*transform.right +1*transform.up)  , transform.rotation)as GameObject;

			a[4]=GameObject.Instantiate(gameObject, transform.position -u/4*(1*transform.forward +1*transform.right -1*transform.up)  , transform.rotation)as GameObject;
			a[5]=GameObject.Instantiate(gameObject, transform.position -u/4*(-1*transform.forward -1*transform.right -1*transform.up)  , transform.rotation)as GameObject;
			a[6]=GameObject.Instantiate(gameObject, transform.position -u/4*(-1*transform.forward +1*transform.right -1*transform.up)  , transform.rotation)as GameObject;
			a[7]=GameObject.Instantiate(gameObject, transform.position -u/4*(1*transform.forward -1*transform.right -1*transform.up)  , transform.rotation)as GameObject;


			for (int jj=0;jj<8;jj++)
			{
				a[jj].transform.localScale=new Vector3(1,1,1)*u/2;
				a[jj].transform.rotation=transform.rotation;
				a[jj].GetComponent<Rigidbody>().velocity=transform.GetComponent<Rigidbody>().velocity;
				a[jj].GetComponent<Rigidbody>().mass=transform.GetComponent<Rigidbody>().mass/8;
				a[jj].transform.parent=container;
				a[jj].GetComponent<BoxDefrag>().container=container;
			}
			Destroy(gameObject);
		}
	}
}
