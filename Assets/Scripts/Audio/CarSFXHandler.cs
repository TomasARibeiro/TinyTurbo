using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarSFXHandler : MonoBehaviour
{
	[Header("Audio Sources")]
	public AudioSource TiresScreechingAS;
	public AudioSource EngineAS;

	[SerializeField] private float _desiredEnginePitch = 0.5f;
	[SerializeField] private float _desiredTireScreechPitch = 0.5f;
	private TopDownCarController _topDownCarController;

	private void Awake()
	{
		_topDownCarController = GetComponent<TopDownCarController>();
	}

	private void Update()
	{
		UpdateEngineSFX();
		UpdateTireScreechingSFX();
	}

	private void UpdateEngineSFX()
	{
		float velocityMagnitude = _topDownCarController.GetVelocityMagnitude();
		float desiredEngineVolume = velocityMagnitude * 0.05f;

		desiredEngineVolume = Mathf.Clamp(desiredEngineVolume, 0.2f, 1.0f);

		EngineAS.volume = Mathf.Lerp(EngineAS.volume, desiredEngineVolume, Time.deltaTime * 10f);

		_desiredEnginePitch = velocityMagnitude * 0.2f;
		_desiredEnginePitch = Mathf.Clamp(_desiredEnginePitch, 0.5f, 2f);
		EngineAS.pitch = Mathf.Lerp(EngineAS.pitch, _desiredEnginePitch, Time.deltaTime * 1.5f);
	}

	private void UpdateTireScreechingSFX()
	{
		if (_topDownCarController.IsTiresScreeching(out float lateralVelocity, out bool isBraking))
		{
			if (isBraking)
			{
				TiresScreechingAS.volume = Mathf.Lerp(TiresScreechingAS.volume, 1.0f, Time.deltaTime * 10f);
				_desiredTireScreechPitch = Mathf.Lerp(_desiredTireScreechPitch, 0.5f, Time.deltaTime * 10f);
			}
			else
			{
				TiresScreechingAS.volume = Mathf.Abs(lateralVelocity) * 0.05f;
				_desiredTireScreechPitch = Mathf.Abs(lateralVelocity) * 0.1f;
			}
		}
		else
		{
			TiresScreechingAS.volume = Mathf.Lerp(TiresScreechingAS.volume, 0, Time.deltaTime * 10f);
		}
	}
}
