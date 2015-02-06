using UnityEngine;
using System.Collections;

public class PlayerCharacter : MonoBehaviour {
	bool m_stopFalling;
	// Use this for initialization
	void Start () {
		
	}

	// Update is called once per frame
	void Update () {
		/*if (!m_stopFalling) {
			Vector3 currPos = this.gameObject.transform.position;
			currPos.y -= 0.5f;
			this.gameObject.transform.position = currPos;
		}*/
	}

	void OnTriggerEnter2D(Collider2D other) {
		m_stopFalling = true;
		Debug.Log("Hit a collider.");
		if (other.tag != "Bullet") {
		other.gameObject.transform.position = new Vector3(other.transform.position.x, other.transform.position.y - 50.0f, other.transform.position.z);
		}
	}
}
