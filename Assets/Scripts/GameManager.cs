using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

	public enum GameState {
		Pregame,
		Postgame,
		InSession
	}

	public static GameManager Instance;

	private GameState m_currGameState;
	private GameObject m_player;
	private GameObject m_rocketManager;

	void Awake () {
		if (Instance == null) {
			Instance = this;
		}
	}

	void Start () {
		m_player = GameObject.FindGameObjectWithTag("Player");
		m_rocketManager = Rockets.Instance.gameObject;
		SetCurrentGameState(GameState.Pregame); 
	}
	

	void Update () {
		EnforceBounds();
	}

	void SetCurrentGameState(GameManager.GameState toSet) {
		if (toSet == GameState.Pregame) {
			m_player.SetActive(false);
			m_rocketManager.SetActive(false);
		} else if (toSet == GameState.InSession) {
			m_player.SetActive(true);
			m_rocketManager.SetActive(true);
		}

		m_currGameState = toSet;
	}

	public void OnStartClick() {
		SetCurrentGameState(GameState.InSession);
	}

	private void EnforceBounds()
	{
		Vector3 playerPos = m_player.transform.position; 
		Camera mainCamera = Camera.main;
		Vector3 cameraPosition = mainCamera.transform.position;

		float xDist = mainCamera.aspect * mainCamera.orthographicSize; 
		float xMax = cameraPosition.x + xDist;
		float xMin = cameraPosition.x - xDist;

		if ( playerPos.x < xMin || playerPos.x > xMax ) {
			SetCurrentGameState(GameState.Pregame);
		}
	}
}
