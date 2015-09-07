using UnityEngine;
using System.Collections;

[RequireComponent(typeof(CharacterMovementController))]
public class Character : MonoBehaviour 
{
	#region Variables

	public Camera firstPersonCamera = null;

	private int _health 									= Constants.CHARACTER_DEFAULT_HEALTH;
	private int _score  									= 0;
	private CharacterMovementController _movementController = null;
	private PlayerInput _input 								= null;
	private Rigidbody _rigidBody 							= null;
    private CapsuleCollider _capsule                        = null;

	private bool _isGoingDown	= false;
	private bool _isGoingUp		= false;
	private bool _isLanded 		= false;

	#endregion

	#region Properties

	public Rigidbody RigidBody
	{
		get { return _rigidBody; }
	}

    public CapsuleCollider Capsule
    {
        get { return _capsule; }
    }
	
	public int Health
	{
		get { return _health; }
		set { _health = value; }
	}
	
	public int Score
	{
		get { return _score; }
		set { _score = value; }
	}

	public bool IsDead
	{
		get { return _health == 0; }
	}

	public PlayerInput Input
	{
		get { return _input; }
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
	
	#endregion

	#region Methods

	void Awake()
	{
		_rigidBody 			= GetComponentInChildren<Rigidbody>();
		_movementController = GetComponent<CharacterMovementController>();
        _capsule            = GetComponentInChildren<CapsuleCollider>();
	}

	void Update()
	{
		if(_input != null)
			_input.UpdateInput(transform.position);
		else
		{
			_input = InputManager.Instance.DetectNewCharacterInput();

			if(_input != null)
				_movementController.OnPlayerInputDetected();
		}

		UpdateLandedFlag();
	}

	public void TakeDamage(int damage, Object damageOwner)
	{
		if(!IsDead)
		{
			_health -= damage;
			
			if(_health <= 0)
				_health = 0;
		}
	}

	public void Reset()
	{
		_health = Constants.CHARACTER_DEFAULT_HEALTH;
		_score 	= 0;

		_movementController.Reset();
	}

	/*void OnGUI()
	{
		GUI.Label(new Rect(100,0,1000,1000), "_rigidBody.velocity.y = " + _rigidBody.velocity.y + " _isLanded = " + _isLanded +  " _isGoingDown = " + _isGoingDown + " _isGoingUp = " + _isGoingUp);
	}*/

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

	#endregion
}
