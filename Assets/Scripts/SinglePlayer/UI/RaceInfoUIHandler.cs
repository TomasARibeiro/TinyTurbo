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
		LapCounterText.text = "Lap: " + GameManager.instance.CurrentLap.ToString() + "/" + GameManager.instance.MaxLaps.ToString();
		CheckPointCounterText.text = "CheckPoint: " + GameManager.instance.CurrentCheckPoint.ToString() + "/" + GameManager.instance.MaxCheckPoints.ToString();
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
		CheckPointCounterText.text = "CheckPoint: " + GameManager.instance.CurrentCheckPoint.ToString() + "/" + GameManager.instance.MaxCheckPoints.ToString();
	}

	private void HandleLapFinished()
	{
		LapCounterText.text = "Lap: " + GameManager.instance.CurrentLap.ToString() + "/" + GameManager.instance.MaxLaps.ToString();
		CheckPointCounterText.text = "CheckPoint: " + GameManager.instance.CurrentCheckPoint.ToString() + "/" + GameManager.instance.MaxCheckPoints.ToString();
	}
}
