using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class TestEnemy : NetworkBehaviour
{
	private Vector3 _rotationAxis = new Vector3(0, 0, 1);
	private float _rotationSpeed = 100.0f;

	public override void OnNetworkSpawn()
	{
		if (!IsServer)
		{
			enabled = false;
			return;
		}
	}

	private void Update()
	{
		transform.Rotate(_rotationAxis, _rotationSpeed * Time.deltaTime);
	}
}
