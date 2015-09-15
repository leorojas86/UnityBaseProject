using UnityEngine;
using System.Collections;

public class Character : MonoBehaviour 
{
	#region Variables

	public Camera firstPersonCamera = null;

	private CharacterMovementController _movementController = null;
    private CharacterStatsController _statsController       = null;
    private CharacterPhysicsController _physicsController   = null;
	private PlayerInput _input 								= null;

	#endregion

	#region Properties

	public PlayerInput Input
	{
		get { return _input; }
        set 
        {
            if (_input != value)
            {
                if (_input != null)
                    _input.Character = null;

                _input = value;

                if (_input != null)
                    _input.Character = this;
            }
        }
	}

    public CharacterPhysicsController PhysicsController
    {
        get { return _physicsController; }
    }

    public Quaternion Rotation
    {
        get { return _physicsController.RigidBody.rotation; }
        set { _physicsController.RigidBody.rotation = value; }
    }

    public Quaternion CameraRotation
    {
        get { return firstPersonCamera != null ? firstPersonCamera.transform.localRotation : Quaternion.identity; }
        set 
        {
            if(firstPersonCamera != null)
                firstPersonCamera.transform.localRotation = value;
        }
    }

    public Vector3 Velocity
    {
        get { return _physicsController.RigidBody.velocity; }
        set { _physicsController.RigidBody.velocity = value; }
    }

    public CharacterMovementController MovementController
    {
        get { return _movementController; }
    }

    public CharacterStatsController StatsController
    {
        get { return _statsController; }
    }
	
	#endregion

	#region Methods

	void Awake()
	{
        _statsController    = new CharacterStatsController(this);
        _physicsController  = new CharacterPhysicsController(this);
        _movementController = new CharacterMovementController(this);
	}

	void Update()
	{
        if(_input != null)
            _input.Update();

        _physicsController.Update();
        _statsController.Update();
        _movementController.Update();
	}

	public void Reset()
	{
        _statsController.Reset();
        _physicsController.Reset();
		_movementController.Reset();
	}

	void OnCollisionEnter(Collision collision)
	{
        _physicsController.CheckForTerrainCollision(collision);
	}

    void OnCollisionStay(Collision collision)
    {
        _physicsController.CheckForTerrainCollision(collision);
    }

    public bool CanBend()
    {
        return _movementController.CanBend();
    }

    public bool CanJump()
    {
        return _movementController.CanJump();
    }

	#endregion
}
