using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaceMenuHandler : MonoBehaviour
{
	[SerializeField] private GameObject _startMenuPrefab;
	[SerializeField] private GameObject _pauseMenuPrefab;

	private void Awake()
	{
		_startMenuPrefab.SetActive(true);
		_pauseMenuPrefab.SetActive(false);
	}

	private void OnEnable()
	{
		GameManager.E_GamePaused += HandleRacePaused;
	}

	private void OnDisable()
	{
		GameManager.E_GamePaused -= HandleRacePaused;
	}

	private void HandleRacePaused()
	{
		if (GameManager.Instance.GetGameState() == GameStates.Idle)
		{
			return;
		}
		else if (GameManager.Instance.GetGameState() == GameStates.Racing)
		{
			_pauseMenuPrefab.SetActive(true);
		}
		else if (GameManager.Instance.GetGameState() == GameStates.InMenu)
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
		_startMenuPrefab.SetActive(false);
		GameManager.Instance.OnCountDownStart();
	}

	public void ExitRaceButton()
	{
		GameManager.Instance.OnExitRace();
	}
}
