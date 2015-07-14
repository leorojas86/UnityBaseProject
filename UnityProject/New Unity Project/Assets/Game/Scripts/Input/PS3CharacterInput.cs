using UnityEngine;
using System.Collections;

public class PS3CharacterInput : CharacterInput 
{
	#region Variables

	private float _lastMovementRotation = 0;

	#endregion

	#region Methods

	public override void UpdateInput()
	{
		_isJumpingButtonDown = Input.GetKeyDown(KeyCode.Space);
		
		//UpdateRotation();
		//UpdateMovement();
	}
	
	/*public override Vector3 GetMovement()
	{
		float moveHorizontal = Input.GetAxis("Horizontal");
		float moveVertical   = Input.GetAxis("Vertical");
		
		return new Vector3(moveHorizontal, 0.0f, moveVertical);
	}

	public override bool IsJumpButtonDown()
	{
		return Input.GetKeyDown(KeyCode.JoystickButton14);
	}

	public override void SetMovementRotation(float rotation)
	{

	}*/
	
	#endregion
}
