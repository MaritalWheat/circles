using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScoreMarker : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void Initialize(Color associatedCircleColor) {
		GetComponentInChildren<Text>().color = associatedCircleColor;
		StartCoroutine(Lifetime(1.0f));

	}

	private IEnumerator Lifetime(float time)
	{
		float elapsedTime = 0;
		Text text = GetComponentInChildren<Text>();
		Color startColor = text.color;
		Color endColor = new Color(startColor.r, startColor.g, startColor.b, 0.0f);
		Vector3 startPos = this.transform.position;
		Vector3 endPos = new Vector3(startPos.x, startPos.y + 2.0f, startPos.z);
		
		
		while (elapsedTime < time)
		{
			text.color = Color.Lerp(startColor, endColor, elapsedTime / time);
			transform.position = Vector3.Lerp(startPos, endPos, elapsedTime / time);
			elapsedTime += Time.deltaTime;
			yield return null;
		}
		
		Destroy(this);
	}
}
