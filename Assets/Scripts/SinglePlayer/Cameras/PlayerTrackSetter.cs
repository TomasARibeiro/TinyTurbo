using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class PlayerTrackSetter : MonoBehaviour
{
    private GameObject _player;
    [SerializeField] private CinemachineVirtualCamera _virtualCamera;

	private void OnEnable()
	{
        PlayerSpawner.E_PlayerReady += HandlePlayerReady;
	}

	private void OnDisable()
	{
		PlayerSpawner.E_PlayerReady -= HandlePlayerReady;
	}

    private void HandlePlayerReady(GameObject player)
    {
        _player = player;
        _virtualCamera.Follow = _player.transform;
        _virtualCamera.LookAt = _player.transform;
    }
}
