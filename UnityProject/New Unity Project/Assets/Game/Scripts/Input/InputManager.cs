using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class InputManager 
{
	#region Variables

	private static InputManager _instance = null;

	private List<PlayerInput> _registeredInputs = new List<PlayerInput>();

	#endregion

	#region Properties

	public static InputManager Instance
	{
		get 
		{ 
			if(_instance != null)
				return _instance;


			_instance = new InputManager();
			return _instance;
		}
	}

	public List<PlayerInput> RegisteredInputs
	{
		get { return _registeredInputs; }
	}

	#endregion

	#region Constructors

	private InputManager()
	{
	}

	#endregion

	#region Methods

	public PlayerInput DetectNewCharacterInput()
	{
		for(int x = 0; x < _registeredInputs.Count; x++)
		{
			PlayerInput currentInput  = _registeredInputs[x];
			PlayerInput detectedInput = currentInput.Detect();

			if(detectedInput != null)
				return detectedInput;
		}

		return null;
	}

	#endregion
}
