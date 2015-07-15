using UnityEngine;
using System.Collections;

public class KeyboardAndMouseCharacterInput : CharacterInput 
{
	#region Methods

	public override void UpdateInput()
	{
		_isJumpingButtonDown = Input.GetKeyDown(KeyCode.Space);

		UpdateRotation();
		UpdateMovement();
	}

	private void UpdateRotation()
	{
		_rotation += Input.GetAxis("Mouse X") * Constants.MOUSE_ROTATION_SPEED;
	}
	
	private void UpdateMovement()
	{
		if(Input.GetKey(KeyCode.W))
		{
			Vector2 movement2D = MathUtils.GetPointAtDistance(Vector2.zero, 1, _rotation);
			_movement 		   = new Vector3(movement2D.y, 0, movement2D.x);
		}
		else if(Input.GetKey(KeyCode.S))
		{
			Vector2 movement2D = MathUtils.GetPointAtDistance(Vector2.zero, 1, _rotation - 180);
			_movement 		   = new Vector3(movement2D.y, 0, movement2D.x);
		}
		else
			_movement = Vector3.zero;
	}

	#endregion
}
