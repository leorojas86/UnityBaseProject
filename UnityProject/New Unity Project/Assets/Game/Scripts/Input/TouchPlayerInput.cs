using UnityEngine;
using System.Collections;

public class TouchPlayerInput : PlayerInput 
{
	#region Variables

	private Vector3 _lastTouchPosition = Vector3.zero;
    private SwipeGesture _swipeGesture = new SwipeGesture(5);

	#endregion

    #region Contructors

    public TouchPlayerInput()
    {
        _swipeGesture.OnSwipe = OnSwipe;
    }

    #endregion

    #region Events

    private void OnSwipe(SwipeGesture sender)
    {
        _isJumpButtonDown = sender.LastDetectedSwipe == SwipeGesture.SwipeDirections.Up;
    }

    #endregion

    #region Methods

    public override void UpdateInput(Vector3 currentCharacterPosition)
	{
        if(_isJumpButtonDown)
           _isJumpButtonDown = false;

        _swipeGesture.Update();

		CheckForNewTouch();

		if(_lastTouchPosition != Vector3.zero)
		{
			Vector3 relativePosition = _lastTouchPosition - currentCharacterPosition;

			if(relativePosition.magnitude > 2)//Do not move if the target is less that 2 meters distance
			{
				UpdateRotation(relativePosition);
				UpdateMovement(relativePosition);
			}
			else
				CleanLastInput();
		}

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
		if(Input.GetMouseButtonUp(0))
		{
			Ray touchRay      = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit[] hits = Physics.RaycastAll(touchRay);

			if(hits.Length > 0)
			{
				_lastTouchPosition 							= hits[0].point;
				TestNewGame.Instance.touchGuide.transform.position = _lastTouchPosition;
			}
			else
				_lastTouchPosition = touchRay.GetPoint(10000);
		}
	}

	private void UpdateRotation(Vector3 relativePosition)
	{
		_targetRotation.y = Quaternion.LookRotation(relativePosition).eulerAngles.y;
	}
	
	private void UpdateMovement(Vector3 relativePosition)
	{
		relativePosition.y = 0;
		_targetMovement    = relativePosition.normalized;
	}

	public override PlayerInput Detect()
	{
		if(Input.GetMouseButtonDown(0))
			return new TouchPlayerInput();

		return null;
	}

	#endregion
}
