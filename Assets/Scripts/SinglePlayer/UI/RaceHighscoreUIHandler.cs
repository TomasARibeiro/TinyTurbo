using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class RaceHighscoreUIHandler : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _text;

	private void Awake()
	{
		float highScore = GameManager.Instance.GetHighScoreForCurrentScene();

		if (highScore == float.MaxValue)
		{
			_text.text = "No High Score";
		}
		else
		{
			int hsTimeMinutes = (int)Mathf.Floor(highScore / 60);
			int hsTimeSeconds = (int)Mathf.Floor(highScore % 60);
			int hsTimeMilliseconds = (int)((highScore * 1000) % 1000);

			_text.text = $"Best Time: {hsTimeMinutes:00}:{hsTimeSeconds:00}:{hsTimeMilliseconds:000}";
		}
	}
}
