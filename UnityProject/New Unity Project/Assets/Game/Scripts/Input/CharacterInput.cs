using UnityEngine;
using System.Collections;

public abstract class  CharacterInput 
{
	#region Constructors

	public CharacterInput()
	{
	}

	#endregion

	#region Methods

	public abstract Vector3 GetMovement();

	#endregion
}
