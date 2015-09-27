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

	public void Update()
	{
		//if(Debug.isDebugBuild)
		//	CheckInput();
	}

	private void CheckInput()
	{
		int keyCount = (int)KeyCode.Joystick4Button9;

		for(int keyCodeIndex = 0; keyCodeIndex < keyCount; keyCodeIndex++)
		{
			KeyCode currentKeyCode = (KeyCode)keyCodeIndex;

			if(Input.GetKey(currentKeyCode))
				Debug.Log("Get Key = " + currentKeyCode);
		}
	}

	public PlayerInput DetectNewCharacterInput()
	{
		for(int x = 0; x < _registeredInputs.Count; x++)
		{
			PlayerInput currentInput  = _registeredInputs[x];
			PlayerInput detectedInput = currentInput.Detect();

			if(detectedInput != null)
			{
				Debug.Log(detectedInput + " detected");

				return detectedInput;
			}
		}

		return null;
	}

	#endregion
}
