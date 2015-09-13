using UnityEngine;
using System.Collections;

public class PS3PlayerInput : PlayerInput 
{
	#region Methods

	public override void UpdateInput()
	{
		_isJumpTriggered = Input.GetKey(KeyCode.JoystickButton14);

		UpdateRotation();
		UpdateMovement();
	}

	private void UpdateRotation()
	{
        Vector3 eulerAngles = _targetRotation.eulerAngles;
        eulerAngles.y       += Input.GetAxis("Horizontal") * Constants.KEYBOARD_ROTATION_SPEED;
        _targetRotation     = Quaternion.Euler(eulerAngles);
	}

	private void UpdateMovement()
	{
		float verticalAxis = Input.GetAxis("Vertical");
		
		if(verticalAxis != 0)
		{
			Vector2 movement2D = MathUtils.GetPointAtDistance(Vector2.zero, verticalAxis, _targetRotation.eulerAngles.y);
			_targetMovement    = new Vector3(movement2D.y, 0, movement2D.x);
		}
		else
			_targetMovement = Vector3.zero;
	}

	public override PlayerInput Detect()
	{
		if(Input.GetKey(KeyCode.JoystickButton14))
			return new PS3PlayerInput();

		return null;
	}
	
	#endregion
}
