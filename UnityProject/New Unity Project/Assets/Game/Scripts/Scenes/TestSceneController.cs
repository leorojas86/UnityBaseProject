using UnityEngine;
using System.Collections;

public class TestSceneController : MonoBehaviour 
{
	#region Methods
	
	void Start() 
	{
		Application.targetFrameRate = 60;
		Time.fixedDeltaTime 		= 1f / 60f;
	}

	#endregion
}
