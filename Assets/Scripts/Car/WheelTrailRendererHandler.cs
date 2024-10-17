using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class WheelTrailRendererHandler : NetworkBehaviour
{
	// Components
	private TopDownCarController _topDownCarController;
	private TrailRenderer _trailRenderer;

	public override void OnNetworkSpawn()
	{
		if (!IsOwner)
		{
			enabled = false;
			return;
		}
	}

	private void Awake()
	{
		_topDownCarController = GetComponentInParent<TopDownCarController>();
		_trailRenderer = GetComponent<TrailRenderer>();

		_trailRenderer.emitting = false;
	}

	void Update()
	{
		// Check if the car tires are screeching (or braking) to emit the trail
		if (_topDownCarController.IsTiresScreeching(out float lateralVelocity, out bool isBraking))
		{
			SetEmittingServerRpc(true);
		}
		else
		{
			SetEmittingServerRpc(false);
		}
	}

	[ServerRpc]
	private void SetEmittingServerRpc(bool isBraking)
	{
		// Update the trail emission state on the server and propagate it to all clients
		SetEmittingClientRpc(isBraking);
	}

	[ClientRpc]
	private void SetEmittingClientRpc(bool isBraking)
	{
		// Update the trail renderer on all clients based on the braking state
		_trailRenderer.emitting = isBraking;
	}
}
