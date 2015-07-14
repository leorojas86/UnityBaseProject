using UnityEngine;
using System.Collections;

public class PS3CharacterInput : CharacterInput 
{
	#region Methods

	public override void UpdateInput()
	{
		_isJumpingButtonDown = Input.GetKeyDown(KeyCode.JoystickButton14);
		_rotation 			+= Input.GetAxis("Horizontal") * Constants.KEYBOARD_ROTATION_SPEED;

		UpdateMovement();
	}

	private void UpdateMovement()
	{
		float verticalAxis = Input.GetAxis("Vertical");

		if(verticalAxis != 0)
		{
			Vector2 movement2D = MathUtils.GetPointAtDistance(Vector2.zero, verticalAxis, _rotation);
			_movement 		   = new Vector3(movement2D.y, 0, movement2D.x);
		}
		else
			_movement = Vector3.zero;
	}
	
	#endregion
}
