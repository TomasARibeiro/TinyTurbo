using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class WheelParticleHandler : MonoBehaviour //NetworkBehaviour
{
	// Components
	private TopDownCarController _topDownCarController;
	private ParticleSystem _particleSystemSmoke;
	private ParticleSystem.EmissionModule _particleSystemEmissionModule;
	private float _particleEmissionRate = 0f;

	private void Awake()
	{
		_topDownCarController = GetComponentInParent<TopDownCarController>();
		_particleSystemSmoke = GetComponent<ParticleSystem>();
		_particleSystemEmissionModule = _particleSystemSmoke.emission;
		_particleSystemEmissionModule.rateOverTime = 0;
	}

	/*public override void OnNetworkSpawn()
	{
		if (!IsOwner)
		{
			enabled = false;
			return;
		}
	}*/

	void Update()
	{
		_particleEmissionRate = Mathf.Lerp(_particleEmissionRate, 0, Time.deltaTime * 5);
		_particleSystemEmissionModule.rateOverTime = _particleEmissionRate;

		// Check if the car tires are screeching (or braking) to emit particles
		if (_topDownCarController.IsTiresScreeching(out float lateralVelocity, out bool isBraking))
		{
			if (isBraking)
			{
				_particleEmissionRate = 30f;
			}
			else _particleEmissionRate = Mathf.Abs(lateralVelocity * 2);
			//SetEmittingServerRpc(isBraking, lateralVelocity);
		}
		/*else
		{
			SetEmittingServerRpc(false, 0);
		}*/
	}

	/*[ServerRpc]
	private void SetEmittingServerRpc(bool isBraking, float lateralVelocity)
	{
		// Determine the emission rate based on whether the car is braking or drifting
		float emissionRate = isBraking ? 30f : Mathf.Abs(lateralVelocity) * 2f;

		// Propagate the emission rate to all clients
		SetEmittingClientRpc(emissionRate);
	}

	[ClientRpc]
	private void SetEmittingClientRpc(float emissionRate)
	{
		// Set the emission rate on all clients
		var emission = _particleSystemSmoke.emission;
		emission.rateOverTime = emissionRate;
	}*/
}
