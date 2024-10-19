using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaceInformation : MonoBehaviour
{
	[SerializeField] private int _maxLaps = 3;
	[SerializeField] private int _maxCheckPoints = 3;

	private void Awake()
	{
		GameManager.instance.OnRaceLoad(_maxLaps, _maxCheckPoints);
	}
}
