using UnityEngine;
using System.Collections;

public class Rockets : MonoBehaviour {
	public static Rockets Instance;

	public Rigidbody2D rocket;
	public float horSpeed = 20.0f;
	private Transform player;
	// Use this for initialization
	void Awake () {
		player = GameObject.FindGameObjectWithTag("Player").transform;
		if (Instance == null) {
			Instance = this;
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (Random.Range(0, 25) == 1) {
			Vector3 playerTransform = new Vector3(player.position.x, player.position.y, player.position.z);
			playerTransform.y -= 20.0f;
			playerTransform.x += (float)(Random.Range(-2, 2));
			Rigidbody2D bulletInstance = Instantiate(rocket, playerTransform, Quaternion.Euler(new Vector3(0,0,90))) as Rigidbody2D;
			bulletInstance.velocity = new Vector2(0, (float)(Random.Range(2, 8)));
		}
	}
}
