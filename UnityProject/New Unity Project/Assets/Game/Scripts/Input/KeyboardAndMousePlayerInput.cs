using UnityEngine;
using System.Collections;

public class KeyboardAndMousePlayerInput : PlayerInput 
{
	#region Methods

	public override void UpdateInput(Character character)
	{
		_isJumpButtonDown = Input.GetKey(KeyCode.Space);

		UpdateRotation();
		UpdateMovement();
	}

	private void UpdateRotation()
	{
		_targetRotation.y += Input.GetAxis("Mouse X") * Constants.MOUSE_ROTATION_SPEED;
		_targetRotation.z -= Input.GetAxis("Mouse Y") * Constants.MOUSE_ROTATION_SPEED;
	}
	
	private void UpdateMovement()
	{
		bool isWPressed = Input.GetKey(KeyCode.W);
		bool isAPressed = Input.GetKey(KeyCode.A);
		bool isSPressed = Input.GetKey(KeyCode.S);
		bool isDPressed = Input.GetKey(KeyCode.D);

		float rotationMovement = 0;

		if(isWPressed && isAPressed)
			rotationMovement = _targetRotation.y + 315;
		else if(isWPressed && isDPressed)
			rotationMovement = _targetRotation.y + 45;
		else if(isSPressed && isAPressed)
			rotationMovement = _targetRotation.y + 225;
		else if(isSPressed && isDPressed)
			rotationMovement = _targetRotation.y + 135;
		else
		{
			if(isWPressed)
				rotationMovement = _targetRotation.y;

			if(isSPressed)
				rotationMovement = _targetRotation.y + 180;

			if(isAPressed)
				rotationMovement = _targetRotation.y + 270;

			if(isDPressed)
				rotationMovement = _targetRotation.y + 90;
		}

		if(rotationMovement != 0)
		{
			Vector2 movement2D = MathUtils.GetPointAtDistance(Vector2.zero, 1, rotationMovement);
			_targetMovement 		   = new Vector3(movement2D.y, 0, movement2D.x);
		}
		else
			_targetMovement = Vector3.zero;
	}

	public override PlayerInput Detect()
	{
		if(Input.GetAxis("Mouse X") != 0)
			return new KeyboardAndMousePlayerInput();

		return null;
	}

	#endregion
}
