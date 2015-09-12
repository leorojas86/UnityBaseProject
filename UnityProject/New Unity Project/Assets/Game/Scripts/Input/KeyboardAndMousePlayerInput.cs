using UnityEngine;
using System.Collections;

public class KeyboardAndMousePlayerInput : PlayerInput 
{
	#region Methods

	public override void UpdateInput()
	{
		_isJumpTriggered = Input.GetKeyDown(KeyCode.Space);

        if(Input.GetKeyDown(KeyCode.Q))
            _isBendToogle = !_isBendToogle;

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

		Vector2 movementVector = Vector2.zero;

		if(isWPressed)
			movementVector.y += 1;

		if(isSPressed)
			movementVector.y -= 1;

		if(isDPressed)
			movementVector.x += 1;

		if(isAPressed)
			movementVector.x -= 1;

		Vector2 rotatedMovement = MathUtils.GetRotatedPointAroundPivot(Vector3.zero, movementVector, -_targetRotation.y);
		_targetMovement         = new Vector3(rotatedMovement.x,0, rotatedMovement.y);
	}

	public override PlayerInput Detect()
	{
		if(Input.GetAxis("Mouse X") != 0)
			return new KeyboardAndMousePlayerInput();

		return null;
	}

	#endregion
}
