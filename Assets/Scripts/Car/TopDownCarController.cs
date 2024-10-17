using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class TopDownCarController : NetworkBehaviour
{
    [Header("Car Settings")]
    public float AccelerationFactor = 5.0f; //how fast the car accelerates
    public float TurnFactor = 3.5f; //how fast the car turns
    public float DriftFactor = 0.95f; //how much the car drifts
    public float MaxSpeed = 20.0f; //max speed the car can achieve

    private float _accelerationInput = 0f;
    private float _steeringInput = 0f;
    private float _rotationAngle = 0f;
    private float _velocityVsUp = 0f;

    //components
    public Rigidbody2D _carRigidBody2D;

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
		_carRigidBody2D = GetComponent<Rigidbody2D>();
	}

	private void FixedUpdate()
	{
        ApplyEngineForce();
        KillOrthogonalVelocity();
        ApplySteering();
	}

    private void ApplyEngineForce()
    {
        //calculate how "forward" the car is going in terms of the direction of our velocity
        _velocityVsUp = Vector2.Dot(transform.up, _carRigidBody2D.velocity);

        //limit so we cannot go faster than the max speed in the "forward" direction
        if (_velocityVsUp > MaxSpeed && _accelerationInput > 0f)
            return;

		//limit so we cannot go faster than the max speed in the "reverse" direction
		if (_velocityVsUp < -MaxSpeed * 0.5f && _accelerationInput < 0f) //only goes half as fast backwards
			return;

        //limit so we cannot go faster in any direction while accelerating
        if (_carRigidBody2D.velocity.sqrMagnitude > MaxSpeed * MaxSpeed && _accelerationInput > 0f)
            return;

		//apply drag if there is no accelerationInput so the car stops when the player lets go of the accelerator
		if (_accelerationInput == 0)
        {
            _carRigidBody2D.drag = Mathf.Lerp(_carRigidBody2D.drag, 2.0f, Time.fixedDeltaTime * 3);
        }
        else _carRigidBody2D.drag = 0;

        //create a force for the engine
        Vector2 engineForceVector = transform.up * _accelerationInput * AccelerationFactor;

        //apply force and pushes the car forward
        _carRigidBody2D.AddForce(engineForceVector, ForceMode2D.Force);
    }

    private void ApplySteering()
    {
		//limit the cars ability to turn when moving slowly
		float divisionFactor = 8f;
		float minSpeedForTurn = (_carRigidBody2D.velocity.magnitude / divisionFactor);
		minSpeedForTurn = Mathf.Clamp01(minSpeedForTurn); //clamp for a value between 0 and 1

		//update the rotation angle based on input
		_rotationAngle -= _steeringInput * TurnFactor * minSpeedForTurn;

        //apply steering by rotating the car object
        _carRigidBody2D.MoveRotation(_rotationAngle);
    }

    private void KillOrthogonalVelocity()
    {
        //value of the cars forward velocity
        Vector2 forwardVelocity = transform.up * Vector2.Dot(_carRigidBody2D.velocity, transform.up);

        //value of the cars sideways velocity
        Vector2 rightVelocity = transform.right * Vector2.Dot(_carRigidBody2D.velocity, transform.right);

        //lower drift factor = very tight, higher drift factor = very floaty
        _carRigidBody2D.velocity = forwardVelocity + rightVelocity * DriftFactor;
    }

    private float GetLateralVelocity()
    {
        //returns how fast the car is moving sideways
        return Vector2.Dot(transform.right, _carRigidBody2D.velocity);
    }

	public bool IsTiresScreeching(out float lateralVelocity, out bool isBraking)
	{
		lateralVelocity = GetLateralVelocity();
		isBraking = false;

		// Check if the player is braking while moving forward
		if (_accelerationInput < 0 && _velocityVsUp > 0)
		{
			isBraking = true;
			return true;
		}

		// Check if the car is drifting or sliding sideways
		float lateralThreshold = 1.0f; // Adjust threshold as needed
		if (Mathf.Abs(lateralVelocity) > lateralThreshold)
		{
			return true;
		}

		return false;
	}


	//[ServerRpc]
	/*private bool IsTiresScreechingServerRPC(out float lateralVelocity, out bool isBraking)
    {
		lateralVelocity = GetLateralVelocity();
        isBraking = false;

		//check if we are moving forward and if the player is hitting the brakes, if so the tires should screech
		if (_accelerationInput < 0 && _velocityVsUp > 0)
		{
			isBraking = true;
			return true;
		}

		//if we have a lot of side movement then the tires should be screeching
		float lateralThreshold = 2.0f;
		if (Mathf.Abs(GetLateralVelocity()) > lateralThreshold)
			return true;

		return false;
	}*/

	public void SetInputVector(Vector2 inputVector)
    {
		_steeringInput = inputVector.x; //accelerate or decelerate
		_accelerationInput = inputVector.y; //steer right or left
	}

    [ServerRpc]
    private void SetInputVectorServerRPC(Vector2 inputVector)
    {
		
	}
}
