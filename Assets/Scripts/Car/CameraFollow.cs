using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class CameraFollow : MonoBehaviour //NetworkBehaviour
{
	/*public override void OnNetworkSpawn()
	{
		if (!IsOwner)
		{
			gameObject.SetActive(false);
		}
	}*/
	//[SerializeField] private Transform _player;
	//[SerializeField] private float _smoothTime = 0.3f;
	//[SerializeField] private Vector3 _offset = new Vector3(0, 1);
	//[SerializeField] private float _lookAheadDistance = 2;
	//[SerializeField] private float _lookAheadSpeed = 1;

	//private Vector3 _velOffset;
	//private Vector3 _vel;
	//private TopDownCarController _topDownCarController;
	//private Vector3 _lookAheadVel;

	//private void Awake() => _player.TryGetComponent(out _topDownCarController);

	//private void LateUpdate()
	//{
	//	if (_topDownCarController != null)
	//	{
	//		var projectedPos = _topDownCarController._carRigidBody2D.velocity.normalized * _lookAheadDistance;
	//		_velOffset = Vector3.SmoothDamp(_velOffset, projectedPos, ref _lookAheadVel, _lookAheadSpeed);
	//	}

	//	Step(_smoothTime);
	//}

	//private void OnValidate() => Step(0);

	//private void Step(float time)
	//{
	//	var goal = _player.position + _offset + _velOffset;
	//	goal.z = -10;
	//	transform.position = Vector3.SmoothDamp(transform.position, goal, ref _vel, time);
	//}
}
