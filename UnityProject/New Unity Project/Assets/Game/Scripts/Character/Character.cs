using UnityEngine;
using System.Collections;

public class Character : MonoBehaviour 
{
	#region Variables

	public Camera firstPersonCamera = null;

    private float _movementSpeed                            = Constants.CHARACTER_DEFAULT_SPEED;
	private CharacterMovementController _movementController = null;
    private CharacterStatsController _statsController       = null;
	private PlayerInput _input 								= null;
	private Rigidbody _rigidBody 							= null;
    private CapsuleCollider _capsule                        = null;

	private bool _isGoingDown	= false;
	private bool _isGoingUp		= false;
	private bool _isLanded 		= false;
    private bool _isBended      = false;

	#endregion

	#region Properties

    public float MovementSpeed
    {
        get { return _movementSpeed; }
        set { _movementSpeed = value;  }
    }

	public Rigidbody RigidBody
	{
		get { return _rigidBody; }
	}

    public CapsuleCollider Capsule
    {
        get { return _capsule; }
    }

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

    public Quaternion Rotation
    {
        get { return _rigidBody.rotation; }
        set { _rigidBody.rotation = value; }
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

	public bool IsFalling
	{
		get { return _isGoingDown; }
	}
	
	public bool IsJumping
	{
		get { return _isGoingUp; }
	}
	
	public bool IsLanded
	{
		get { return _isLanded; }
	}

    public bool IsBended
    {
        get { return _isBended;  }
        set { _isBended = value;  }
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
        _rigidBody = GetComponentInChildren<Rigidbody>();
        _capsule   = GetComponentInChildren<CapsuleCollider>();

        _statsController    = new CharacterStatsController(this);
        _movementController = new CharacterMovementController(this);
	}

	void Update()
	{
		if(_input != null)
			_input.UpdateInput();
		else
		{
			PlayerInput input = InputManager.Instance.DetectNewCharacterInput();

            if(input != null)
               Input = input;
		}

		UpdateLandedFlag();

        _movementController.Update();
	}

	public void Reset()
	{
        _statsController.Reset();
		_movementController.Reset();
	}

	private void UpdateLandedFlag()
	{
		if(_isLanded)
			_isLanded = Mathf.Abs(_rigidBody.velocity.y) < Constants.CHARACTER_MAX_LANDED_Y_VELOCITY;
		else
		{
			_isGoingUp   = _rigidBody.velocity.y > Constants.CHARACTER_MAX_LANDED_Y_VELOCITY;
			_isGoingDown = _rigidBody.velocity.y < -Constants.CHARACTER_MAX_LANDED_Y_VELOCITY;
		}
	}

	void OnCollisionEnter(Collision collision)
	{
		CheckForTerrainCollision(collision);
	}
	
	void OnCollisionStay(Collision collision)
	{
		CheckForTerrainCollision(collision);
	}
	
	private void CheckForTerrainCollision(Collision collision)
	{
		if(!_isLanded)
		{
			_isLanded = collision.collider.gameObject.layer == LayerMask.NameToLayer("Terrain");
			
			if(_isLanded)
			{
				_isGoingUp   = false;
				_isGoingDown = false;
			}
		}
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
