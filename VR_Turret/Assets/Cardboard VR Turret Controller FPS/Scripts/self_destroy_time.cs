using UnityEngine;
using System.Collections;

public class self_destroy_time : MonoBehaviour {
	
		private GameObject Obj;
		private float elapsed;
		public float duration=5.5f;

		private Color col_rend;
		private Renderer rend_for_alpha;
		
		public bool has_render=false;

		// Use this for initialization
		void Start () {
			Obj=gameObject;

		}
		
		// Update is called once per frame
		
		void FixedUpdate () 
		{
			elapsed+=Time.fixedDeltaTime;
			
			if( elapsed > duration)
				{
				

						Destroy(Obj);
				
				}	

			
		}
		


		void OnCollisionEnter()
		{
			Destroy(gameObject);
		}
}
	