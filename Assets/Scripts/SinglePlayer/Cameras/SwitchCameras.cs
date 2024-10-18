using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchCameras : MonoBehaviour
{
	[SerializeField] private GameObject _levelCam;
	[SerializeField] private GameObject _playerCam;
	private bool _levelCamOn = true;

	private void OnEnable()
	{
		EventsManager.E_CameraSwitch += HandleCamerasSwitched;
	}

	private void OnDisable()
	{
		EventsManager.E_CameraSwitch -= HandleCamerasSwitched;
	}

	private void Awake()
	{
		_levelCam.SetActive(true);
		_playerCam.SetActive(false);
	}

	private void HandleCamerasSwitched()
	{
		if (_levelCamOn)
		{
			_levelCam.SetActive(false);
			_playerCam.SetActive(true);
		}
		else
		{
			_playerCam.SetActive(false);
			_levelCam.SetActive(true);
		}
	}
}
