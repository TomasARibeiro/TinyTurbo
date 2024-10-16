using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelParticleHandler : MonoBehaviour
{
    private float _particleEmissionRate = 0f;
    
    //components
    TopDownCarController _topDownCarController;
    ParticleSystem _particleSystemSmoke;
    ParticleSystem.EmissionModule _particleSystemEmissionModule;

	private void Awake()
	{
        _topDownCarController = GetComponentInParent<TopDownCarController>();
		_particleSystemSmoke = GetComponent<ParticleSystem>();
        _particleSystemEmissionModule = _particleSystemSmoke.emission;
        _particleSystemEmissionModule.rateOverTime = 0;
	}

	void Start()
    {
        
    }

    void Update()
    {
        //reduce particles over time
        _particleEmissionRate = Mathf.Lerp(_particleEmissionRate, 0, Time.deltaTime * 5);
        _particleSystemEmissionModule.rateOverTime = _particleEmissionRate;

        if (_topDownCarController.IsTiresScreeching(out float lateralVelocity, out bool isBraking))
        {
            //if the car is screeching we emit smoke. If its braking we emitt a lot of smoke
            if (isBraking)
            {
                _particleEmissionRate = 30;
            }
            //if the player is drifting we emitt smoke based on how much the player is drifting
            else _particleEmissionRate = Mathf.Abs(lateralVelocity) * 2;
        }
    }
}
