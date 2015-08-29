using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FPSCounter : MonoBehaviour 
{
	#region Variables

	private Text _text    	= null;
	private int _frames     = 0;
	private int _lastSecond = 0;

	#endregion

	#region Methods

	void Awake()
	{
		_text = GetComponent<Text>();
	}

	void Update() 
	{
		_frames++;

		if(Time.time > _lastSecond + 1)
		{
			_text.text  = "FPS: " + _frames;
			_frames     = 0;
			_lastSecond = (int)Time.time;
		}
	}

	#endregion
}
