using UnityEngine;
using System.Collections;

public class XboxPlayerInput : PlayerInput 
{
	#region Methods

	public override void UpdateInput()
	{
		_isJumpButtonDown = Input.GetKey(KeyCode.JoystickButton2);

		UpdateRotation();
		UpdateMovement();
	}

	private void UpdateRotation()
	{
		_targetRotation.y += Input.GetAxis("Horizontal") * Constants.KEYBOARD_ROTATION_SPEED;
	}

	private void UpdateMovement()
	{
		float verticalAxis = Input.GetAxis("Vertical");
		
		if(verticalAxis != 0)
		{
			Vector2 movement2D = MathUtils.GetPointAtDistance(Vector2.zero, verticalAxis, _targetRotation.y);
			_targetMovement 		   = new Vector3(movement2D.y, 0, movement2D.x);
		}
		else
			_targetMovement = Vector3.zero;
	}

	public override PlayerInput Detect()
	{
		if(Input.GetKey(KeyCode.JoystickButton2))
			return new XboxPlayerInput();

		return null;
	}
	
	#endregion
}
