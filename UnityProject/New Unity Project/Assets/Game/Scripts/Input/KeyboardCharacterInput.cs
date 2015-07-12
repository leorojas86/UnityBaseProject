using UnityEngine;
using System.Collections;

public class KeyboardCharacterInput : CharacterInput 
{
	#region Variables

	private Vector3 _lastMovement = Vector3.zero;

	#endregion

	#region Methods
	
	public override Vector3 GetMovement()
	{
		Vector3 movement = Vector3.zero;

		if(Input.GetKey(KeyCode.W))
		   movement += Vector3.forward;

		if(Input.GetKey(KeyCode.A))
		   movement += Vector3.left;

		if(Input.GetKey(KeyCode.D))
		   movement += Vector3.right;

		return movement;
	}

	public override bool IsJumpButtonDown()
	{
		return Input.GetKeyDown(KeyCode.Space);
	}
	
	#endregion
}
