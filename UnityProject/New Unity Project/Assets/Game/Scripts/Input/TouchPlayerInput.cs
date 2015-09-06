using UnityEngine;
using System.Collections;

public class TouchPlayerInput : PlayerInput 
{
	#region Variables

	private Vector3 _lastTouchPosition = Vector3.zero;

	#endregion

	#region Methods

	public override void UpdateInput(Vector3 currentCharacterPosition)
	{
		CheckForNewTouch();

		if(_lastTouchPosition != Vector3.zero)
		{
			Vector3 relativePosition = _lastTouchPosition - currentCharacterPosition;

			if(relativePosition.magnitude > 2)//Do not move if the target is less that 2 meters distance
			{
				UpdateRotation(currentCharacterPosition, relativePosition.normalized);
				UpdateMovement(currentCharacterPosition, relativePosition.normalized);
			}
			else
				CleanLastInput();
		}

		_isJumpButtonDown = Input.touchCount > 1 || Input.GetMouseButton(1);

		if(_isJumpButtonDown)
			CleanLastInput();
	}

	private void CleanLastInput()
	{
		_lastTouchPosition = Vector3.zero;
		_targetMovement    = Vector3.zero;
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

	private void UpdateRotation(Vector3 currentCharacterPosition, Vector3 relativePosition)
	{
		_targetRotation.y = Quaternion.LookRotation(relativePosition).eulerAngles.y;
	}
	
	private void UpdateMovement(Vector3 currentCharacterPosition, Vector3 relativePosition)
	{
		relativePosition.y = 0;
		_targetMovement    = relativePosition;
	}

	public override PlayerInput Detect()
	{
		if(Input.GetMouseButton(0))
			return new TouchPlayerInput();

		return null;
	}

	#endregion
}
