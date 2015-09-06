using UnityEngine;
using System.Collections;

public class KeyboardPlayerInput : PlayerInput 
{
	#region Methods

	public override void UpdateInput(Vector3 currentCharacterPosition)
	{
		_isJumpButtonDown = Input.GetKey(KeyCode.Space);

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
			Vector2 movement2D = MathUtils.GetPointAtDistance(Vector2.zero, 1, _targetRotation.y);
			_targetMovement 		   = new Vector3(movement2D.y, 0, movement2D.x);
		}
		else if(Input.GetKey(KeyCode.S))
		{
			Vector2 movement2D = MathUtils.GetPointAtDistance(Vector2.zero, 1, _targetRotation.y - 180);
			_targetMovement 		   = new Vector3(movement2D.y, 0, movement2D.x);
		}
		else
			_targetMovement = Vector3.zero;
	}

	public override PlayerInput Detect()
	{
		//Debug.Log("Detect");

		if(Input.GetKey(KeyCode.Space))
		{
			//Debug.Log("Detected");
			return new KeyboardPlayerInput();
		}

		return null;
	}

	#endregion
}
