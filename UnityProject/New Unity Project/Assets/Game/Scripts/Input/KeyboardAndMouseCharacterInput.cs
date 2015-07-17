using UnityEngine;
using System.Collections;

public class KeyboardAndMouseCharacterInput : CharacterInput 
{
	#region Methods

	public override void UpdateInput()
	{
		_isJumpButtonDown = Input.GetKeyDown(KeyCode.Space);

		UpdateRotation();
		UpdateMovement();
	}

	private void UpdateRotation()
	{
		_yRotation += Input.GetAxis("Mouse X") * Constants.MOUSE_ROTATION_SPEED;
	}
	
	private void UpdateMovement()
	{
		bool isWPressed = Input.GetKey(KeyCode.W);
		bool isAPressed = Input.GetKey(KeyCode.A);
		bool isSPressed = Input.GetKey(KeyCode.S);
		bool isDPressed = Input.GetKey(KeyCode.D);

		float rotationMovement = 0;

		if(isWPressed && isAPressed)
			rotationMovement = _yRotation + 315;
		else if(isWPressed && isDPressed)
			rotationMovement = _yRotation + 45;
		else if(isSPressed && isAPressed)
			rotationMovement = _yRotation + 225;
		else if(isSPressed && isDPressed)
			rotationMovement = _yRotation + 135;
		else
		{
			if(isWPressed)
				rotationMovement = _yRotation;

			if(isSPressed)
				rotationMovement = _yRotation + 180;

			if(isAPressed)
				rotationMovement = _yRotation + 270;

			if(isDPressed)
				rotationMovement = _yRotation + 90;
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
