using UnityEngine;
using System.Collections;

public class InputManager : MonoBehaviour {
	
	void Start () {
	
	}

	void Update () {
		if (Input.GetMouseButtonUp(0)) {
			RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
			
			if(hit.collider != null)
			{
				if (hit.collider.gameObject.CompareTag("Circle")) {
					HandleHit(hit.collider.gameObject);
				}
			}
		}

		if (Input.touchCount > 1) {
			RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position), Vector2.zero);
			
			if(hit.collider != null)
			{
				if (hit.collider.gameObject.CompareTag("Circle")) {

					RaycastHit2D hitTwo = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.GetTouch(1).position), Vector2.zero);

					if (hitTwo.collider.gameObject.CompareTag("Circle")) {
						HandleDoubleHit(hit.collider.gameObject, false);
						HandleDoubleHit(hitTwo.collider.gameObject, true);
					}
				}
			}
		}
	}

	void HandleHit(GameObject hit) {
		CircleBehaviour circle = hit.GetComponent<CircleBehaviour>();
		if (!circle.fading) {
			circle.OnClick(0.2f, 1.5f);
		}
	}

	void HandleDoubleHit(GameObject hit, bool secondTouch) {
		CircleBehaviour circle = hit.GetComponent<CircleBehaviour>();
		if (!circle.fading) {
			circle.OnMultiTouch(0.2f, 1.5f, secondTouch);
		}
	}
}
