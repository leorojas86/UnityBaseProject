using UnityEngine;
using System.Collections;

public class Game : MonoBehaviour 
{
	#region Variables

	public Transform touchGuide = null;

	private static Game _instance = null;

	#endregion

	#region Properties

	public static Game Instance
	{
		get { return _instance; }
	}

	#endregion

	#region Methods

	void Awake() 
	{
		//InputManager.Instance.RegisteredInputs.Add(new KeyboardAndMousePlayerInput());
		//InputManager.Instance.RegisteredInputs.Add(new KeyboardPlayerInput());
		//InputManager.Instance.RegisteredInputs.Add(new PS3PlayerInput());
		//InputManager.Instance.RegisteredInputs.Add(new XboxPlayerInput());
		InputManager.Instance.RegisteredInputs.Add(new TouchPlayerInput());

		Application.targetFrameRate = 120;

		_instance = this;
	}

	void Update()
	{
		InputManager.Instance.Update();
	}

	void OnDestroy()
	{
		_instance = null;
	}

	#endregion
}
