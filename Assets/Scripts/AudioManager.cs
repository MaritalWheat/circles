using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AudioManager : MonoBehaviour {

	public static AudioManager Instance;
	public List<AudioClip> popped;

	private int index = 0;
	private bool firstHit = true;

	void Awake() {
		if (Instance == null) {
			Instance = this;
		}
	}

	void Start () {
		
	}

	void Update () {

	}

	public void PlaySuccessClip() {
		if (!firstHit) {
			index++;
		} else {
			firstHit = false;
		}
		this.gameObject.audio.PlayOneShot(popped[index % popped.Count], 1.0f);
		CircleGameManager.ResetMissCount();
	}

	public void PlaySuccessfulMultitouchClip(bool secondTouch) {
		if (!firstHit && !secondTouch) {
			index++;
		} else {
			firstHit = false;
		}
		this.gameObject.audio.PlayOneShot(popped[index % popped.Count], 1.0f);
	}

	public void PlayMissedClip(CircleBehaviour missedCircle) {
		if ((missedCircle.partnerCircle == null) || (!missedCircle.partnerCircle.fading)) {
			if ((index - 1) <= 0) {
				index = 0;
				CircleGameManager.IncreaseMissCount();
			} else {
				index--;
			}
		}
		this.gameObject.audio.PlayOneShot(popped[index % popped.Count], 1.0f);
	}
}
