using UnityEngine;
using System.Collections;

public class TouchPlayerInput : PlayerInput 
{
	#region Variables

	private Vector3 _lastTouchPosition = Vector3.zero;

	#endregion

	#region Methods

	public override void UpdateInput()
	{
		_isJumpButtonDown = Input.touchCount > 1;

		CheckForNewTouch();

		UpdateRotation();
		UpdateMovement();
	}

	private void CheckForNewTouch()
	{
		if(Input.GetMouseButton(0))
		{
			Ray touchRay      = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit[] hits = Physics.RaycastAll(touchRay);

			if(hits.Length > 0)
				_lastTouchPosition = hits[0].point;
		}
	}

	private void UpdateRotation()
	{
		/*if(Input.GetKey(KeyCode.A))
			_targetRotation.y -= Constants.KEYBOARD_ROTATION_SPEED;
		
		if(Input.GetKey(KeyCode.D))
			_targetRotation.y += Constants.KEYBOARD_ROTATION_SPEED;*/
	}
	
	private void UpdateMovement()
	{
		/*if(Input.GetKey(KeyCode.W))
		{
			Vector2 movement2D = MathUtils.GetPointAtDistance(Vector2.zero, 1, _targetRotation.y);
			_targetMovement 		   = new Vector3(movement2D.y, 0, movement2D.x);
		}
		else if(Input.GetKey(KeyCode.S))
		{
			Vector2 movement2D = MathUtils.GetPointAtDistance(Vector2.zero, 1, _targetRotation.y - 180);
			_targetMovement 		   = new Vector3(movement2D.y, 0, movement2D.x);
		}
		else
			_targetMovement = Vector3.zero;*/
	}

	public override PlayerInput Detect()
	{
		//Debug.Log("Detect");

		if(Input.GetMouseButton(0))
		{
			//Debug.Log("Detected");
			return new TouchPlayerInput();
		}

		return null;
	}

	#endregion
}
