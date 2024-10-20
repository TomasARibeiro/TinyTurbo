using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RaceTimerUIHandler : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _text;
    private float _lastRaceTimeUpdated = 0f;

	private void Awake()
	{
        _text.text = "";
	}

	private void OnEnable()
	{
        GameManager.E_RaceBegin += HandleRaceStarted;
	}

	private void OnDisable()
	{
		GameManager.E_RaceBegin -= HandleRaceStarted;
	}

    private void HandleRaceStarted()
    {
        StartCoroutine(UpdateTimeCO());
    }

    IEnumerator UpdateTimeCO()
    {
        while (true)
        {
			float raceTime = GameManager.Instance.GetRaceTime();

            if (_lastRaceTimeUpdated != raceTime)
            {
                int raceTimeMinutes = (int)Mathf.Floor(raceTime / 60);
                int raceTimeSeconds = (int)Mathf.Floor(raceTime % 60);
                int raceTimeMilliseconds = (int)((raceTime * 1000) % 1000);

                _text.text = $"Current Time: {raceTimeMinutes:00}:{raceTimeSeconds:00}:{raceTimeMilliseconds:000}";

                _lastRaceTimeUpdated = raceTime;
            }

            yield return new WaitForSeconds(0.05f);
		}
    }
}
