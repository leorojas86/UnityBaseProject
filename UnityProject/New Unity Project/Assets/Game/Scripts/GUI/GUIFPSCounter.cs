using UnityEngine;
using System.Collections;

public class GUIFPSCounter : MonoBehaviour 
{
	#region Variables

	private int _framesCount  = 0;
	private float _lastSecond = 0;
	private int _fps		  = 0;

	#endregion

	#region Methods

	void Update() 
	{
		_framesCount++;

		if(_lastSecond + 1 < Time.time)
		{
			_fps 		 = _framesCount;
			_framesCount = 0;
			_lastSecond  = Time.time;
		}
	}

	void OnGUI()
	{
		GUI.Label(new Rect(0, 0, 100, 100), _fps.ToString());
	}

	#endregion
}
