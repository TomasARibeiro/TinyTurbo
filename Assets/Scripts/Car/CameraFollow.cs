using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class CameraFollow : NetworkBehaviour
{
	public override void OnNetworkSpawn()
	{
		if (!IsOwner)
		{
			gameObject.SetActive(false);
		}
	}
	//[SerializeField] private Transform _player;
	//[SerializeField] private float _smoothTime = 0.1f;
	//[SerializeField] private Vector3 _offset = new Vector3(0, 1);
	//[SerializeField] private float _lookAheadDistance = 2;
	//[SerializeField] private float _lookAheadSpeed = 3;

	//private Vector3 _velOffset;
	//private Vector3 _vel;
	//private TopDownCarController _topDownCarController;
	//private Rigidbody2D _rigidBody2D;
	//private Vector3 _lookAheadVel;

	//public override void OnNetworkSpawn()
	//{
	//	if (!IsOwner)
	//	{
	//		gameObject.SetActive(false);
	//	}
	//}

	//private void Awake()
	//{
	//	_topDownCarController = GetComponentInParent<TopDownCarController>();
	//	_rigidBody2D = _topDownCarController.GetComponent<Rigidbody2D>();
	//}

	private void Update()
	{
		transform.rotation = Quaternion.Euler(0,0,0);
	}

	//private void LateUpdate()
	//{
	//	if (_topDownCarController != null && _rigidBody2D != null)
	//	{
	//		Debug.Log("AAAAAAAAAAA");
	//		var projectedPos = _rigidBody2D.velocity.normalized * _lookAheadDistance;
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
