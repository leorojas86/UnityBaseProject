using UnityEngine;
using System.Collections;

public class Game : MonoBehaviour 
{
	#region Methods

	void Awake() 
	{
		InputManager.Instance.RegisteredInputs.Add(new KeyboardAndMousePlayerInput());
		InputManager.Instance.RegisteredInputs.Add(new KeyboardPlayerInput());
		InputManager.Instance.RegisteredInputs.Add(new PS3PlayerInput());
	}

	#endregion
}
