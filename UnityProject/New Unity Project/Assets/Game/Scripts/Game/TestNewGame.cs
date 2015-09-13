using UnityEngine;
using System.Collections;

public class TestNewGame : MonoBehaviour 
{
	#region Variables

	public Transform touchGuide     = null;
    public Character mainCharacter  = null;

	private static TestNewGame _instance = null;

	#endregion

	#region Properties

	public static TestNewGame Instance
	{
		get { return _instance; }
	}

	#endregion

	#region Methods

	void Awake() 
	{
		InputManager.Instance.RegisteredInputs.Add(new KeyboardAndMousePlayerInput());
		//InputManager.Instance.RegisteredInputs.Add(new KeyboardPlayerInput());
		//InputManager.Instance.RegisteredInputs.Add(new PS3PlayerInput());
		//InputManager.Instance.RegisteredInputs.Add(new XboxPlayerInput());
		//InputManager.Instance.RegisteredInputs.Add(new TouchPlayerInput());

		Application.targetFrameRate = 120;
        Time.fixedDeltaTime         = 1.0f / Application.targetFrameRate;
        Time.maximumDeltaTime       = Time.fixedDeltaTime;

		_instance = this;
	}

	void Update()
	{
		InputManager.Instance.Update();

        if (mainCharacter.Input == null)
        {
            PlayerInput input = InputManager.Instance.DetectNewCharacterInput();

            if (input != null)
                mainCharacter.Input = input;
        }
	}

	void OnDestroy()
	{
		_instance = null;
	}

	#endregion
}
