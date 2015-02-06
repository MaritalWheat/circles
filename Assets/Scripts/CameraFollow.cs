using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour 
{
	private Transform player;		// Reference to the player's transform.


	void Awake ()
	{
		// Setting up the reference.
		player = GameObject.FindGameObjectWithTag("Player").transform;
	}

	void Update() {
		this.gameObject.transform.position = new Vector3(this.transform.position.x, player.position.y - 5.0f, this.gameObject.transform.position.z);
	}
}
