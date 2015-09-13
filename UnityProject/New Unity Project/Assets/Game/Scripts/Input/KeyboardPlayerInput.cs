using UnityEngine;
using System.Collections;

public class KeyboardPlayerInput : PlayerInput 
{
	#region Methods

	public override void UpdateInput()
	{
		_isJumpTriggered = Input.GetKey(KeyCode.Space);

        if(Input.GetKeyDown(KeyCode.Q))
            _isBendToogle = !_isBendToogle;

		UpdateRotation();
		UpdateMovement();
	}

	private void UpdateRotation()
	{
		if(Input.GetKey(KeyCode.A))
            _targetRotation.y -= Constants.KEYBOARD_ROTATION_SPEED;
		
		if(Input.GetKey(KeyCode.D))
            _targetRotation.y += Constants.KEYBOARD_ROTATION_SPEED;
	}
	
	private void UpdateMovement()
	{
		if(Input.GetKey(KeyCode.W))
		{
            Vector2 movement2D = MathUtils.GetPointAtDistance(Vector2.zero, 1, _character.RigidBody.rotation.eulerAngles.y);
			_targetMovement    = new Vector3(movement2D.y, 0, movement2D.x);
		}
		else if(Input.GetKey(KeyCode.S))
		{
            Vector2 movement2D = MathUtils.GetPointAtDistance(Vector2.zero, 1, _character.RigidBody.rotation.eulerAngles.y - 180);
			_targetMovement    = new Vector3(movement2D.y, 0, movement2D.x);
		}
		else
			_targetMovement = Vector3.zero;
	}

	public override PlayerInput Detect()
	{
		if(Input.GetKey(KeyCode.Space))
			return new KeyboardPlayerInput();

		return null;
	}

	#endregion
}
