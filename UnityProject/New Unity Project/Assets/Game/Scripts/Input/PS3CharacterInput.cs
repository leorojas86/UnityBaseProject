using UnityEngine;
using System.Collections;

public class PS3CharacterInput : CharacterInput 
{
	#region Methods

	public override void UpdateInput()
	{
		_isJumpButtonDown = Input.GetKey(KeyCode.JoystickButton14);

		UpdateRotation();
		UpdateMovement();
	}

	private void UpdateRotation()
	{
		_yRotation 			+= Input.GetAxis("Horizontal") * Constants.KEYBOARD_ROTATION_SPEED;

		/*var horizontal = Input.GetAxis("Horizontal 2");
		var vertical   = Input.GetAxis("Vertical 2");

		_yRotation += horizontal * Constants.KEYBOARD_ROTATION_SPEED;*/
	}

	private void UpdateMovement()
	{
		/*float verticalAxis   = Input.GetAxis("Vertical");
		float horizontalAxis = Input.GetAxis("Horizontal");

		if(verticalAxis != 0)
		{
			Vector2 movementZ = MathUtils.GetPointAtDistance(Vector2.zero, verticalAxis, _yRotation);
			Vector2 movementX = MathUtils.GetPointAtDistance(Vector2.zero, horizontalAxis, _yRotation + 90);
			_movement 		  = new Vector3(movementX.y, 0, movementZ.x);
		}
		else
			_movement = Vector3.zero;*/


		/*bool isWPressed = Input.GetKey(KeyCode.W);
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
		}*/
		
		/*if(rotationMovement != 0)
		{
			Vector2 movement2D = MathUtils.GetPointAtDistance(Vector2.zero, 1, rotationMovement);
			_movement 		   = new Vector3(movement2D.y, 0, movement2D.x);
		}
		else
			_movement = Vector3.zero;*/


		float verticalAxis = Input.GetAxis("Vertical");
		
		if(verticalAxis != 0)
		{
			Vector2 movement2D = MathUtils.GetPointAtDistance(Vector2.zero, verticalAxis, _yRotation);
			_movement 		   = new Vector3(movement2D.y, 0, movement2D.x);
		}
		else
			_movement = Vector3.zero;
	}

	public override CharacterInput Detect()
	{
		if(Input.GetKey(KeyCode.JoystickButton14))
			return new PS3CharacterInput();

		return null;
	}
	
	#endregion
}
