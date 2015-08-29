using UnityEngine;
using System.Collections;

[RequireComponent(typeof(CharacterMovementController))]
public class Character : MonoBehaviour 
{
	#region Variables

	private int _health 									= Constants.CHARACTER_DEFAULT_HEALTH;
	private int _score  									= 0;
	private CharacterMovementController _movementController = null;
	private PlayerInput _input 					= null;

	#endregion

	#region Properties
	
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
	
	#endregion

	#region Methods

	void Awake()
	{
		_movementController = GetComponent<CharacterMovementController>();
	}

	void Update()
	{
		if(_input == null)
			_input = InputManager.Instance.DetectNewCharacterInput();
	}

	public void TakeDamage(int damage, Object damageOwner)
	{
		if(!IsDead)
		{
			//Debug.Log("TakeDamage damage = " + damage);
			_health -= damage;
			
			if(_health <= 0)
			{
				_health = 0;
				
				/*Player damageOwnerPlayer = damageOwner as Player;//Killer
				
				if(damageOwnerPlayer != null && damageOwnerPlayer != this)
					damageOwnerPlayer._score++;
				else
					_score--;
				
				playerAnimation.Stop();
				StartCoroutine(DeadFallingCoroutine());*/
			}
		}
	}

	public void Reset()
	{
		_health = Constants.CHARACTER_DEFAULT_HEALTH;
		_score 	= 0;

		_movementController.Reset();
	}

	#endregion
}
