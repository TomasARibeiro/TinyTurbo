using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelTrailRendererHandler : MonoBehaviour
{
    //components
    private TopDownCarController _topDownCarController;
    private TrailRenderer _trailRenderer;

	private void Awake()
	{
        _topDownCarController = GetComponentInParent<TopDownCarController>();
		_trailRenderer = GetComponent<TrailRenderer>();

        _trailRenderer.emitting = false;
	}

	void Start()
    {
        
    }

    void Update()
    {
        //if the car tires are screeching we will emitt a trail
        if (_topDownCarController.IsTiresScreeching(out float lateralVelocity, out bool isBraking))
        {
            _trailRenderer.emitting = true;
        }
        else _trailRenderer.emitting = false;
    }
}
