using UnityEngine;
using System.Collections;

public class KeyboardCharacterInput : CharacterInput 
{
	#region Methods

	public override void UpdateInput()
	{
		_isJumpButtonDown = Input.GetKey(KeyCode.Space);

		UpdateRotation();
		UpdateMovement();
	}

	private void UpdateRotation()
	{
		if(Input.GetKey(KeyCode.A))
			_yRotation -= Constants.KEYBOARD_ROTATION_SPEED;
		
		if(Input.GetKey(KeyCode.D))
			_yRotation += Constants.KEYBOARD_ROTATION_SPEED;
	}
	
	private void UpdateMovement()
	{
		if(Input.GetKey(KeyCode.W))
		{
			Vector2 movement2D = MathUtils.GetPointAtDistance(Vector2.zero, 1, _yRotation);
			_movement 		   = new Vector3(movement2D.y, 0, movement2D.x);
		}
		else if(Input.GetKey(KeyCode.S))
		{
			Vector2 movement2D = MathUtils.GetPointAtDistance(Vector2.zero, 1, _yRotation - 180);
			_movement 		   = new Vector3(movement2D.y, 0, movement2D.x);
		}
		else
			_movement = Vector3.zero;
	}

	#endregion
}
