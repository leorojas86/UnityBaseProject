using UnityEngine;
using System.Collections;

public class TouchPlayerInput : PlayerInput 
{
	#region Variables

	private Vector3 _lastTouchPosition = Vector3.zero;
    private SwipeGesture _swipeGesture = new SwipeGesture(Constants.TOUCH_INPUT_SWIPE_GESTURE_MIN_DISTANCE);

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
        switch (sender.LastDetectedSwipe)
        {
            case SwipeGesture.SwipeDirections.Up:
                if (_isBendToogle)
                    _isBendToogle = false;//Get up, take stand position
                else
                    _isJumpTriggered = true;
            break;
            case SwipeGesture.SwipeDirections.Down:
                _isBendToogle = sender.LastDetectedSwipe == SwipeGesture.SwipeDirections.Down;
            break;
        }
    }

    #endregion

    #region Methods

    public override void UpdateInput(Vector3 currentCharacterPosition)
	{
		CheckForNewTouch();

		if(_lastTouchPosition != Vector3.zero)
		{
			Vector3 relativePosition = _lastTouchPosition - currentCharacterPosition;

            if (relativePosition.magnitude > Constants.TOUCH_INPUT_MIN_TOUCH_DISTANCE)//Do not move if the target is less that 2 meters distance
			{
				UpdateRotation(relativePosition);
				UpdateMovement(relativePosition);
			}
			else
				CleanLastInput();
		}

        UpdateSwipeGesture();
	}

    private void UpdateSwipeGesture()
    {
        if (_isJumpTriggered)//Clean Trigger, only one frame trigger
            _isJumpTriggered = false;

        _swipeGesture.Update();

        //if (_isJumpTriggered)
           // CleanLastInput();
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
