using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RaceInfoUIHandler : MonoBehaviour
{
	public TextMeshProUGUI LapCounterText;
	public TextMeshProUGUI CheckPointCounterText;

	private void Awake()
	{
		LapCounterText.text = "Lap: " + GameManager.Instance.CurrentLap.ToString() + "/" + GameManager.Instance.MaxLaps.ToString();
		CheckPointCounterText.text = "CheckPoint: " + GameManager.Instance.CurrentCheckPoint.ToString() + "/" + GameManager.Instance.MaxCheckPoints.ToString();
	}

	private void OnEnable()
	{
		GameManager.E_CheckPointPassed += HandleCheckPointPassed;
		GameManager.E_LapFinished += HandleLapFinished;
	}

	private void OnDisable()
	{
		GameManager.E_CheckPointPassed -= HandleCheckPointPassed;
		GameManager.E_LapFinished -= HandleLapFinished;
	}

	private void HandleCheckPointPassed()
	{
		CheckPointCounterText.text = "CheckPoint: " + GameManager.Instance.CurrentCheckPoint.ToString() + "/" + GameManager.Instance.MaxCheckPoints.ToString();
	}

	private void HandleLapFinished()
	{
		LapCounterText.text = "Lap: " + GameManager.Instance.CurrentLap.ToString() + "/" + GameManager.Instance.MaxLaps.ToString();
		CheckPointCounterText.text = "CheckPoint: " + GameManager.Instance.CurrentCheckPoint.ToString() + "/" + GameManager.Instance.MaxCheckPoints.ToString();
	}
}
