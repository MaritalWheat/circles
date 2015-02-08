using UnityEngine;
using System.Collections;

public class CircleBehaviour : MonoBehaviour {

	private Vector3 from = Vector3.zero;
	private Vector3 to = new Vector3(0.15f, 0.15f, 0.15f);
	public float startTime;
	public Color startColor;
	public Color endColor;

	private bool clicked = false;
	public bool fading = false;
	public Color currColor;
	public CircleBehaviour partnerCircle;

	void Awake() {
		this.transform.localScale = Vector3.zero;
	}

	void Start (){
		startTime = Time.time;
		from = this.transform.localScale;
		if (CircleManager.Instance.colors.Count > 0) {
			int index = Random.Range(0, CircleManager.Instance.colors.Count);
			startColor = CircleManager.Instance.colors[index];
			endColor = CircleManager.Instance.complementaryColors[index];
		}
	}

	void Update () {
		if (!clicked) {
			float percentComplete = (Time.time - startTime) / CircleManager.Instance.circleLifetime;
			this.transform.localScale = Vector3.Lerp(from, to, percentComplete);
			SpriteRenderer sprite = this.gameObject.GetComponent<SpriteRenderer>();
			sprite.color = Color.Lerp(startColor, endColor, percentComplete);
			currColor = sprite.color;
			if (percentComplete > 0.99f) {
				LifetimeComplete(CircleManager.Instance.circleLifetime * 0.125f);
			}
		}
	}

	void Kill(bool addScore) {
		CircleManager.Instance.KillCircle(this.gameObject, addScore);
	}

	public void OnClick(float time, float expansionTimeFactor) {
		if (fading || clicked) return;
		clicked = true;
		AudioManager.Instance.PlaySuccessClip();
		StartCoroutine(ClickAnimationExpansion(time, expansionTimeFactor));
	}

	public void OnMultiTouch(float time, float expansionTimeFactor, bool secondTouch) {
		if (fading || clicked) return;
		clicked = true;
		AudioManager.Instance.PlaySuccessfulMultitouchClip(secondTouch);
		StartCoroutine(ClickAnimationExpansion(time, expansionTimeFactor));
	}

	public void LifetimeComplete(float time) {
		if (fading || clicked) return;
		fading = true;
		AudioManager.Instance.PlayMissedClip(this);
		StartCoroutine(OnLifetimeComplete(time));
	}

	private IEnumerator OnLifetimeComplete(float time)
	{
		float elapsedTime = 0;
		SpriteRenderer sprite = this.gameObject.GetComponent<SpriteRenderer>();
		Color startColor = sprite.color;
		Color endColor = new Color(startColor.r, startColor.g, startColor.b, 0.0f);
		
		
		while (elapsedTime < time)
		{
			sprite.color = Color.Lerp(startColor, endColor, elapsedTime / time);
			
			elapsedTime += Time.deltaTime;
			yield return null;
		}
		
		Kill(false);
	}

	private IEnumerator ClickAnimationExpansion(float time, float expansionTimeFactor)
	{
		float elapsedTime = 0;
		Vector3 startScale = this.transform.localScale;
		Vector3 peakScale = this.transform.localScale * 1.8f;


		while (elapsedTime < time)
		{
			this.transform.localScale = Vector3.Lerp(startScale, peakScale, elapsedTime / time);

			elapsedTime += Time.deltaTime;
			yield return null;
		}
		
		StartCoroutine(ClickAnimationDeflation(time * expansionTimeFactor));
	}

	private IEnumerator ClickAnimationDeflation(float time)
	{
		float elapsedTime = 0;
		Vector3 startScale = this.transform.localScale;
		Vector3 endScale = Vector3.zero;
		
		while (elapsedTime < time)
		{
			this.transform.localScale = Vector3.Lerp(startScale, endScale, elapsedTime / time);

			elapsedTime += Time.deltaTime;
			yield return null;
		}
		
		Kill(true);
	}
}
