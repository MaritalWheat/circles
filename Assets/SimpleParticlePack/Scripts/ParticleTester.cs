using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ParticleTester : MonoBehaviour {
	public GameObject flare;


	void Awake () {
		Object flareRes = Resources.Load("Flare02-Multicolor", typeof(GameObject));
		flare = GameObject.Instantiate(flareRes, Vector3.zero, Quaternion.identity) as GameObject;
			
	}
	
	void Update () {
		Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		flare.transform.position = new Vector3(mousePos.x, mousePos.y, mousePos.z - 50.0f);
	}
	


		
}
