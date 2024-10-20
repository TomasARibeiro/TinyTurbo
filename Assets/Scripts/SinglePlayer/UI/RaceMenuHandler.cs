using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class RaceMenuHandler : MonoBehaviour
{
	[SerializeField] private GameObject _startMenuPrefab;
	[SerializeField] private TextMeshProUGUI _startMenuLevelText;
	[SerializeField] private GameObject _pauseMenuPrefab;
	[SerializeField] private GameObject _overMenuPrefab;
	[SerializeField] private TextMeshProUGUI _overMenuTimerText;
	[SerializeField] private TextMeshProUGUI _overMenuHighScoreText;
	[SerializeField] private TextMeshProUGUI _highScoreAlertText;

	private void Awake()
	{
		_startMenuPrefab.SetActive(true);
		_startMenuLevelText.text = SceneManager.GetActiveScene().name;
		_pauseMenuPrefab.SetActive(false);
		_overMenuPrefab.SetActive(false);
		_overMenuTimerText.text = "";
		_highScoreAlertText.text = "";
	}

	private void OnEnable()
	{
		GameManager.E_GamePaused += HandleRacePaused;
		GameManager.E_RaceOver += HandleRaceOver;
	}

	private void OnDisable()
	{
		GameManager.E_GamePaused -= HandleRacePaused;
		GameManager.E_RaceOver -= HandleRaceOver;
	}

	private void HandleRaceOver()
	{
		if (GameManager.Instance.GetGameState() == GameStates.RaceOver)
		{
			float highScore = GameManager.Instance.GetHighScoreForCurrentScene();
			float raceTime = GameManager.Instance.GetRaceTime();

			if (raceTime <= highScore)
			{
				_highScoreAlertText.text = "New High Score!";
			}

			int raceTimeMinutes = (int)Mathf.Floor(raceTime / 60);
			int raceTimeSeconds = (int)Mathf.Floor(raceTime % 60);
			int raceTimeMilliseconds = (int)((raceTime * 1000) % 1000);

			int hsTimeMinutes = (int)Mathf.Floor(highScore / 60);
			int hsTimeSeconds = (int)Mathf.Floor(highScore % 60);
			int hsTimeMilliseconds = (int)((highScore * 1000) % 1000);

			_overMenuHighScoreText.text = $"Best Time: {hsTimeMinutes:00}:{hsTimeSeconds:00}:{hsTimeMilliseconds:000}";
			_overMenuTimerText.text = $"New Time: {raceTimeMinutes:00}:{raceTimeSeconds:00}:{raceTimeMilliseconds:000}";
			_overMenuPrefab.SetActive(true);
		}
	}

	public void RaceRestarted()
	{
		Scene scene = SceneManager.GetActiveScene();
		if (GameManager.Instance.GetGameState() == GameStates.RaceOver || GameManager.Instance.GetGameState() == GameStates.InMenu)
		{
			GameManager.Instance.OnStartedLevel();
			SceneManager.LoadScene(scene.name);
		}
	}

	private void HandleRacePaused()
	{
		if (GameManager.Instance.GetGameState() == GameStates.Idle)
		{
			return;
		}
		else if (GameManager.Instance.GetGameState() == GameStates.InMenu)
		{
			_pauseMenuPrefab.SetActive(true);
		}
		else if (GameManager.Instance.GetGameState() == GameStates.Racing)
		{
			_pauseMenuPrefab.SetActive(false);
		}
	}

	public void ResumeRaceButton()
	{
		_pauseMenuPrefab.SetActive(false);
		GameManager.Instance.OnRacePaused();
	}

	public void StartRaceButton()
	{
		if (GameManager.Instance.GetGameState() == GameStates.Idle)
		{
			_startMenuPrefab.SetActive(false);
		}
		else if (GameManager.Instance.GetGameState() == GameStates.RaceOver)
		{
			_overMenuPrefab.SetActive(false);
		}
		GameManager.Instance.OnCountDownStart();
	}

	public void ExitRaceButton()
	{
		GameManager.Instance.OnExitRace();
	}
}
