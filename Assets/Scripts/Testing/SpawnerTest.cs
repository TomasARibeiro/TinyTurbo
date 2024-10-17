using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using System.Threading.Tasks;

public class SpawnerTest : NetworkBehaviour
{
    [SerializeField] private GameObject _prefab;
    [SerializeField] private Vector2 _minSpawnPos;
	[SerializeField] private Vector2 _maxSpawnPos;

	public List<GameObject> players;

	public override void OnNetworkSpawn()
	{
		if (!IsServer)
		{
			enabled = false;
			return;
		}

		NetworkManager.Singleton.OnClientConnectedCallback += ClientConnected;
		NetworkManager.Singleton.OnClientDisconnectCallback += ClientDisconnected;
	}

	private void ClientConnected(ulong u)
	{
		players = new List<GameObject>(GameObject.FindGameObjectsWithTag("Player"));
	}

	private async void ClientDisconnected(ulong u)
	{
		await Task.Yield();
		players = new List<GameObject>(GameObject.FindGameObjectsWithTag("Player"));
	}

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.R))
		{
			GameObject objectToSpawn = Instantiate(_prefab, new Vector3(Random.Range(_minSpawnPos.x, _maxSpawnPos.x), Random.Range(_minSpawnPos.y, _maxSpawnPos.y), 0), Quaternion.identity);
			objectToSpawn.GetComponent<NetworkObject>().Spawn(true);
		}
	}
}
