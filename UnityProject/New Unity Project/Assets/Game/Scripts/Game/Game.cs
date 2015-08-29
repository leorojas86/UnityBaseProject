using UnityEngine;
using System.Collections;

public class Game : MonoBehaviour 
{
	#region Methods

	void Awake() 
	{
		InputManager.Instance.RegisteredInputs.Add(new KeyboardAndMouseCharacterInput());
		InputManager.Instance.RegisteredInputs.Add(new KeyboardCharacterInput());
		InputManager.Instance.RegisteredInputs.Add(new PS3CharacterInput());
	}

	#endregion
}
