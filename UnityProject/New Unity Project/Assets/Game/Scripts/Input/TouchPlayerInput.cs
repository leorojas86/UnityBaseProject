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
        switch(sender.LastDetectedSwipe)
        {
            case SwipeGesture.SwipeDirections.Up:
                if (_isBendToogle && _character.CanBend())
                    _isBendToogle = false;//Get up, take stand position
                else
                    _isJumpTriggered = _character.CanJump();
            break;
            case SwipeGesture.SwipeDirections.Down: 
                if(_character.CanBend())
                    _isBendToogle = sender.LastDetectedSwipe == SwipeGesture.SwipeDirections.Down;
            break;
        }
    }

    #endregion

    #region Methods

    public override void Update()
	{
		CheckForNewTouch();

		if(_lastTouchPosition != Vector3.zero)
		{
			Vector3 relativePosition = _lastTouchPosition - _character.transform.position;

            if(relativePosition.magnitude > Constants.TOUCH_INPUT_MIN_TOUCH_DISTANCE)//Do not move if the target is less that 2 meters distance
			{
				UpdateRotation(relativePosition);
				UpdateMovement(relativePosition);

                //_isBreakToogle = false;
			}
			else
                ClearLastInput();
		}

        UpdateSwipeGesture();
	}

    private void UpdateSwipeGesture()
    {
        if (_isJumpTriggered)//Clean Trigger, only one frame trigger
            _isJumpTriggered = false;

        _swipeGesture.Update();
    }

    private void CheckForNewTouch()
	{
		if(Input.GetMouseButtonUp(0))
		{
            _lastTouchPosition                                  = PhysicsUtils.GetCurrentMousePositionRaycastHitPoint(Constants.TOUCH_INPUT_MAX_RAYCAST_DISTANCE);
            TestNewGame.Instance.touchGuide.transform.position  = _lastTouchPosition;
		}
	}

	private void UpdateRotation(Vector3 relativePosition)
	{
        float rotation    = Quaternion.LookRotation(relativePosition).eulerAngles.y - _character.MovementController.InitialRotation.eulerAngles.y;
        _targetRotation.x = rotation;
	}
	
	private void UpdateMovement(Vector3 relativePosition)
	{
		relativePosition.y = 0;
		_targetMovement    = relativePosition.normalized;
	}

    public override void ClearLastInput()
    {
        base.ClearLastInput();

        _lastTouchPosition = Vector3.zero;
    }

	public override PlayerInput Detect()
	{
		if(Input.GetMouseButtonDown(0))
			return new TouchPlayerInput();

		return null;
	}

	#endregion
}
