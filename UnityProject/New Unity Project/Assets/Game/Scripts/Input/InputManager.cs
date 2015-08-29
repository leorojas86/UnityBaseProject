using UnityEngine;
using System.Collections;

public class InputManager 
{
	#region Variables

	private static InputManager _instance = null;

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

	#endregion

	#region Constructors

	private InputManager()
	{
	}

	#endregion

	#region Methods

	public CharacterInput DetectNewCharacterInput()
	{
		return null;
	}

	#endregion
}
