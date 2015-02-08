using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class CircleManager : MonoBehaviour {

	public static CircleManager Instance;

	public GameObject circle;
	public GameObject scoreOne;

	public int circleCount;
	public List<Color> colors;
	public List<Color> complementaryColors;
	public GameState currentGameState;

	private bool spawnLock;
	private bool roundInProgress;
	public float circleLifetime = 2.0f;
	private float timeToNextDifficulty = 5.0f;
	private List<GameObject> liveCircle = new List<GameObject>();

	public enum GameState {
		Normal,
		DoubleRound,
		ChainRound
	}

	void Awake() {
		if (Instance == null) {
			Instance = this;
		}
	}

	void Start() {

	}

	void Update() {

	}

	public void BeginRound() {
		circleCount = 0;
		currentGameState = GameState.Normal;
		SpawnCircle();
		StartCoroutine(DifficultyChangeTimer(timeToNextDifficulty));
	}

	void SpawnCircle() {
		if (spawnLock || roundInProgress) return;
		roundInProgress = true;
		Vector3 spawnPosition = Vector3.zero;
		spawnPosition.x = Random.Range(0, Screen.width);
		spawnPosition.y = Random.Range(0, Screen.height);
		GameObject circleObj = Instantiate (circle, Camera.main.ScreenToWorldPoint(new Vector3(spawnPosition.x, spawnPosition.y, 10)), Quaternion.identity) as GameObject;
		liveCircle.Add (circleObj);
		circleCount++;
		Debug.Log(circleCount);
		spawnLock = true;
		StartCoroutine(LockTimer(0.1f));
	}

	void SpawnDoubleCircle() {
		if (spawnLock || roundInProgress) return;
		roundInProgress = true;
		List<CircleBehaviour> circles = new List<CircleBehaviour>();
		for (int i = 0; i < 2; i++) {
			Vector3 spawnPosition = Vector3.zero;
			spawnPosition.x = Random.Range(0, Screen.width);
			spawnPosition.y = Random.Range(0, Screen.height);
			GameObject circleObj = Instantiate (circle, Camera.main.ScreenToWorldPoint(new Vector3(spawnPosition.x, spawnPosition.y, 10)), Quaternion.identity) as GameObject;
			circles.Add(circleObj.GetComponent<CircleBehaviour>());
			liveCircle.Add (circleObj);
			circleCount++;
		}
		CircleBehaviour circleOne = circles[0];
		CircleBehaviour circleTwo = circles[1];

		circleOne.partnerCircle = circleTwo;
		circleTwo.partnerCircle = circleOne;

		spawnLock = true;
		StartCoroutine(LockTimer(0.1f));
		Debug.Log (circleCount);
	}

	void StartNextRound() {
		Debug.Log (circleCount);
		if (CircleGameManager.Instance.currGameState == CircleGameManager.GameState.Stopped) return;
		if (circleCount < 1) {
			int roll = Random.Range(0, 10);
			if (roll < 5) {
				currentGameState = GameState.Normal;
				SpawnCircle();
			} else if (roll >= 5) {
				currentGameState = GameState.DoubleRound;
				SpawnDoubleCircle();
			}
		}
	}

	public void KillCircle(GameObject toKill, bool addScore) {
		if (addScore) {
			Vector3 scorePos = new Vector3(toKill.transform.position.x, toKill.transform.position.y, 20.0f); 
			GameObject scoreUI = GameObject.Instantiate(scoreOne, toKill.transform.position, Quaternion.identity) as GameObject;
			scoreUI.GetComponent<ScoreMarker>().Initialize(toKill.gameObject.GetComponent<CircleBehaviour>().currColor);
		}
		liveCircle.Remove(toKill);
		Destroy(toKill);
		circleCount--;
		if (liveCircle.Count == 0) roundInProgress = false;
		StartNextRound();
	}

	IEnumerator LockTimer(float waitTime) {
		yield return new WaitForSeconds(waitTime);
		spawnLock = false;
	}

	IEnumerator DifficultyChangeTimer(float waitTime) {
		yield return new WaitForSeconds(waitTime);
		circleLifetime = circleLifetime - 0.1f;
		timeToNextDifficulty += 10.0f;
		Debug.Log ("New circle lifetime: " + circleLifetime);
		StartCoroutine(DifficultyChangeTimer(timeToNextDifficulty));
	}

	//change this to match what the publicly exposed editor variables for circleLifteime and timeToNextDifficulty are
	public static void ResetCircles() {
		Instance.StopAllCoroutines();
		Instance.timeToNextDifficulty = 5.0f;
		Instance.circleLifetime = 2.0f;
	}
}
