using UnityEngine;
using System.Collections;

public class KeyboardCharacterInput : CharacterInput 
{
	#region Variables

	private float _lastMovementRotation = 0;

	#endregion

	#region Methods
	
	public override Vector3 GetMovement()
	{
		if(Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
		{
			float distance = Input.GetKey(KeyCode.W) ? 1f : 0.001f;

			if(Input.GetKey(KeyCode.A))
				_lastMovementRotation += Constants.KEYBOARD_ROTATION_SPEED;
			
			if(Input.GetKey(KeyCode.D))
				_lastMovementRotation -= Constants.KEYBOARD_ROTATION_SPEED;

			Vector2 movement2D = MathUtils.GetPointAtDistance(Vector2.zero, distance, _lastMovementRotation);

			return new Vector3(movement2D.x, 0, movement2D.y);
		}

		return Vector3.zero;
	}

	public override void SetMovementRotation(float rotation)
	{
		_lastMovementRotation = rotation + 90;//Rotation 0 is right
	}

	public override bool IsJumpButtonDown()
	{
		return Input.GetKeyDown(KeyCode.Space);
	}
	
	#endregion
}
