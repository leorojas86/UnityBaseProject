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
		float rotationMovement = 0;

		if(Input.GetKey(KeyCode.W))
			rotationMovement = _rotation;

		if(Input.GetKey(KeyCode.A))
			rotationMovement = _rotation - 90;

		if(Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.A))
			rotationMovement = _rotation - 45;
		 
		if(Input.GetKey(KeyCode.S))
			rotationMovement = _rotation - 180;



		if(Input.GetKey(KeyCode.D))
			rotationMovement = _rotation + 90;

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
