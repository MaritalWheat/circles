using UnityEngine;
using System.Collections;

public class CircleGameManager : MonoBehaviour {

	public static CircleGameManager Instance;
	[HideInInspector]
	public int consecutiveMissCount = 0;
	public Animator startButton;

	public enum GameState {
		Paused,
		Running,
		Stopped
	}

	[HideInInspector]
	public GameState currGameState = GameState.Stopped;

	void Awake() {
		if (Instance == null) {
			Instance = this;
		}
	}

	void Start () {

	}

	void Update () {
	
	}

	void ResetGameState() {
		consecutiveMissCount = 0;
		startButton.SetBool("Normal", true);
		CircleManager.ResetCircles();
	}

	public void StartGame() {
		CircleManager.Instance.BeginRound();
		currGameState = GameState.Running;
	}

	public void EndGame() {
		currGameState = GameState.Stopped;
		ResetGameState();
	}

	public static void IncreaseMissCount() {
		Instance.consecutiveMissCount++;
		if (Instance.consecutiveMissCount > 4) {
			Instance.EndGame();
		}
	}

	public static void ResetMissCount() {
		Instance.consecutiveMissCount = 0;
	}
}
