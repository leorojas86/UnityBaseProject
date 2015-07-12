using UnityEngine;
using System.Collections;

public class KeyboardCharacterInput : CharacterInput 
{
	#region Methods
	
	public override Vector3 GetMovement()
	{
		float moveHorizontal = Input.GetAxis("Horizontal");
		float moveVertical   = Input.GetAxis("Vertical");
		
		return new Vector3(moveHorizontal, 0.0f, moveVertical);
	}

	public override bool IsJumpButtonDown()
	{
		return Input.GetKeyDown(KeyCode.Space);
	}
	
	#endregion
}
