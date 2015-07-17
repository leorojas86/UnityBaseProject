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
		bool isWPressed = Input.GetKey(KeyCode.W);
		bool isAPressed = Input.GetKey(KeyCode.A);
		bool isSPressed = Input.GetKey(KeyCode.S);
		bool isDPressed = Input.GetKey(KeyCode.D);

		float rotationMovement = 0;

		if(isWPressed && isAPressed)
			rotationMovement = _rotation + 315;
		else if(isWPressed && isDPressed)
			rotationMovement = _rotation + 45;
		else if(isSPressed && isAPressed)
			rotationMovement = _rotation + 225;
		else if(isSPressed && isDPressed)
			rotationMovement = _rotation + 135;
		else
		{
			if(isWPressed)
				rotationMovement = _rotation;

			if(isSPressed)
				rotationMovement = _rotation + 180;

			if(isAPressed)
				rotationMovement = _rotation + 270;

			if(isDPressed)
				rotationMovement = _rotation + 90;
		}

		if(rotationMovement != 0)
		{
			Vector2 movement2D = MathUtils.GetPointAtDistance(Vector2.zero, 1, rotationMovement);
			_movement 		   = new Vector3(movement2D.y, 0, movement2D.x);
		}
		else
			_movement = Vector3.zero;
	}

	#endregion
}
