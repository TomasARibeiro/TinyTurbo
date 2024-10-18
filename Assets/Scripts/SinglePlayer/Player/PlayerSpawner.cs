using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
	public static event Action<GameObject> E_PlayerReady;
	[SerializeField] private GameObject _player;

	private void OnEnable()
	{
		EventsManager.E_SpawnPlayer += HandlePlayerSpawn;
	}

	private void OnDisable()
	{
		EventsManager.E_SpawnPlayer -= HandlePlayerSpawn;
	}

	private void HandlePlayerSpawn()
	{
		GameObject playerInstance = Instantiate(_player, gameObject.transform.position, Quaternion.identity);
		E_PlayerReady?.Invoke(playerInstance);
	}
}
