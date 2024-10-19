using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarLapCounter : MonoBehaviour
{
	private int _passedCheckPointNumber = 0;
	private int _numberOfPassedCheckPoints = 0;

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.CompareTag("CheckPoint"))
		{
			CheckPoint checkPoint = collision.GetComponent<CheckPoint>();

			if (checkPoint.IsFinishLine && _passedCheckPointNumber + 1 == checkPoint.CheckPointNumber)
			{
				GameManager.instance.OnLapFinished();
				ResetCheckPointsForNewLap();
			}
			else if (!checkPoint.IsFinishLine && _passedCheckPointNumber + 1 == checkPoint.CheckPointNumber && _passedCheckPointNumber + 1 <= GameManager.instance.MaxCheckPoints)
			{
				_passedCheckPointNumber = checkPoint.CheckPointNumber;
				_numberOfPassedCheckPoints++;

				GameManager.instance.OnCheckPointPassed(_numberOfPassedCheckPoints);
			}
		}
	}

	private void ResetCheckPointsForNewLap()
	{
		_passedCheckPointNumber = 0;
		_numberOfPassedCheckPoints = 0;
	}
}
