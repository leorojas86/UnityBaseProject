using UnityEngine;
using System.Collections;

public class SwipeGesture
{
	#region Enums
	
	/// <summary>
	/// Enumeration that defines the directions at which a swipe may be performed.
	/// </summary>
	public enum SwipeDirections
	{
		None,
		Left,
		Right,
		Up,
		Down,
		Horizontal,
		Vertical,
		All
	}
	
	#endregion
	
	#region Variables
	
	/// <summary>
	/// The minimum distance to be travelled before detecting a swipe.
	/// </summary>
	[SerializeField] private float _minSwipeDistance = 2f;

	/// <summary>
	/// The detect on drag distance.
	/// </summary>
	[SerializeField] private float _detectOnDragDistance = 10f;
	
	/// <summary>
	/// Determines at which directions a swipe may be detected. The default is detecting a swipe in all directions.
	/// </summary>
	[SerializeField] private SwipeDirections _detectSwipeDirection = SwipeDirections.All;	
	
	/// <summary>
	/// Method to be called whenever a swipe is detected.
	/// </summary>
    public System.Action<SwipeGesture> OnSwipe = null;
	
	/// <summary>
	/// The orientation of the last detected swipe.
	/// </summary>
	private SwipeDirections _lastDetectedSwipe = SwipeDirections.None;

	/// <summary>
	/// The _swipe start position.
	/// </summary>
	private Vector3 _swipeStartPosition = Vector3.zero;

    private bool _isActive = false;

	#endregion
	
	#region Properties

	/// <summary>
	/// Gets the orientation of the last detected swipe.
	/// </summary>
	/// <value>
	/// The orientation of the last detected swipe.
	/// </value>
	public SwipeDirections LastDetectedSwipe
	{
		get { return _lastDetectedSwipe; }
	}
	
	#endregion
	
	#region Methods

	/// <summary>
	/// Method used to update the swipe gesture.
	/// </summary>
	public void Update()
	{
        if (!_isActive)
			CheckInput();
		else
			CheckSwipes();
	}

	/// <summary>
	/// Checks the inputs. If it's a screen input (touches) or a controller input (buttons or keys)
	/// </summary>
	private void CheckInput()
	{
		bool isPressed = Input.GetMouseButton(0);
			
		if(isPressed)
		{
			_lastDetectedSwipe  = SwipeDirections.None;
			_swipeStartPosition = Input.mousePosition;
            _isActive           = true;
		}
	}

	/// <summary>
	/// Checks the swipes. It if was a swipe using the screen input or using the controller input
	/// </summary>
	private void CheckSwipes()
	{
        Vector2 mousePosition = Input.mousePosition;
        bool isScreenReleased = !Input.GetMouseButton(0);
			
		if(isScreenReleased)
		{
            if (CheckSwipe(mousePosition, _minSwipeDistance))
                NotifyOnSwipeEvent(true);
            else
                _isActive = false;
		}
		else
		{
			if(CheckSwipe(mousePosition, _detectOnDragDistance))
				NotifyOnSwipeEvent(false);
		}
	}

	/// <summary>
	/// Method used to notify if a swiping event.
	/// </summary>
	protected virtual void NotifyOnSwipeEvent(bool deactivate)
	{
        if (deactivate)
            _isActive = false;

		if(OnSwipe != null)
			OnSwipe(this);
	}
	
	/// <summary>
	/// Checks whether a swipe has been performed.
	/// </summary>
	/// <returns>
	/// <c>true</c> if a swipe had been performed; otherwise <c>false</c>.
 	/// </returns>
	/// <param name='pLastSwipePosition'>
	/// Position where the last swipe had been performed.
	/// </param>
	/// <param name='swipeDistance'>
	/// Swipe distance.
	/// </param>
	protected bool CheckSwipe(Vector3 pLastSwipePosition, float swipeDistance)
	{
		float xDistance    = _swipeStartPosition.x - pLastSwipePosition.x;
		float yDistance    = _swipeStartPosition.y - pLastSwipePosition.y;
		float xDistanceAbs = Mathf.Abs(xDistance);
		float yDistanceAbs = Mathf.Abs(yDistance);
		
		switch(_detectSwipeDirection)
		{
			case SwipeDirections.Left:
				return CheckLeftSwipe(xDistanceAbs, xDistance, swipeDistance);
			case SwipeDirections.Right:
				return CheckRightSwipe(xDistanceAbs, xDistance, swipeDistance);
			case SwipeDirections.Up:
				return CheckUpSwipe(yDistanceAbs, yDistance, swipeDistance);
			case SwipeDirections.Down:
				return CheckDownSwipe(yDistanceAbs, yDistance, swipeDistance);
			case SwipeDirections.Horizontal:
				return CheckLeftSwipe(xDistanceAbs, xDistance, swipeDistance) || CheckRightSwipe(xDistanceAbs, xDistance, swipeDistance);
			case SwipeDirections.Vertical:
				return CheckUpSwipe(yDistanceAbs, yDistance, swipeDistance) || CheckDownSwipe(yDistanceAbs, yDistance, swipeDistance);
			case SwipeDirections.All:
				if(xDistanceAbs > yDistanceAbs)
					return CheckLeftSwipe(xDistanceAbs, xDistance, swipeDistance) || CheckRightSwipe(xDistanceAbs, xDistance, swipeDistance);
				else
					return CheckUpSwipe(yDistanceAbs, yDistance, swipeDistance) || CheckDownSwipe(yDistanceAbs, yDistance, swipeDistance);
			default:
				return false;
		}
	}

	/// <summary>
	/// Checks whether a swipe to the left had been performed.
	/// </summary>
	/// <returns>
	/// <c>true</c> if a swipe to the left had been made; otherwise <c>false</c>.
	/// </returns>
	/// <param name='xDistanceAbs'>
	/// Absolute value of the travelled distance in x.
	/// </param>
	/// <param name='xDistance'>
	/// Travelled distance in x.
	/// </param>
	/// <param name='swipeDistance'>
	/// Swipe's distance.
	/// </param>
	private bool CheckLeftSwipe(float xDistanceAbs, float xDistance, float swipeDistance)
	{
		if(xDistanceAbs > swipeDistance && xDistance > 0 && _lastDetectedSwipe != SwipeDirections.Left)
		{
			_lastDetectedSwipe = SwipeDirections.Left;
			return true;
		}
		
		return false;
	}
	
	/// <summary>
	/// Checks whether a swipe to the right had been performed.
	/// </summary>
	/// <returns>
	/// <c>true</c> if a swipe to the right had been made; otherwise <c>false</c>.
	/// </returns>
	/// <param name='xDistanceAbs'>
	/// Absolute value of the travelled distance in x.
	/// </param>
	/// <param name='xDistance'>
	/// Travelled distance in x.
	/// </param>
	/// <param name='swipeDistance'>
	/// Swipe's distance.
	/// </param>
	private bool CheckRightSwipe(float xDistanceAbs, float xDistance, float swipeDistance)
	{
		if(xDistanceAbs > swipeDistance && xDistance < 0 && _lastDetectedSwipe != SwipeDirections.Right)
		{
			_lastDetectedSwipe = SwipeDirections.Right;
			return true;
		}
		
		return false;
	}
	
	/// <summary>
	/// Checks whether an upwards swipe had been performed.
	/// </summary>
	/// <returns>
	/// <c>true</c> if an upwards swipe had been made; otherwise <c>false</c>.
	/// </returns>
	/// <param name='yDistanceAbs'>
	/// Absolute value of the travelled distance in y.
	/// </param>
	/// <param name='yDistance'>
	/// Travelled distance in y.
	/// </param>
	/// <param name='swipeDistance'>
	/// Swipe's distance.
	/// </param>
	private bool CheckUpSwipe(float yDistanceAbs, float yDistance, float swipeDistance)
	{
		if(yDistanceAbs > swipeDistance && yDistance < 0 && _lastDetectedSwipe != SwipeDirections.Up)
		{
			_lastDetectedSwipe = SwipeDirections.Up;
			return true;
		}
		
		return false;
	}
	
	/// <summary>
	/// Checks whether a downwards swipe had been performed.
	/// </summary>
	/// <returns>
	/// <c>true</c> if a downwards swipe had been made; otherwise <c>false</c>.
	/// </returns>
	/// <param name='yDistanceAbs'>
	/// Absolute value of the travelled distance in y.
	/// </param>
	/// <param name='yDistance'>
	/// Travelled distance in y.
	/// </param>
	/// <param name='swipeDistance'>
	/// Swipe's distance.
	/// </param>
	private bool CheckDownSwipe(float yDistanceAbs, float yDistance, float swipeDistance)
	{
		if(yDistanceAbs > swipeDistance && yDistance > 0 && _lastDetectedSwipe != SwipeDirections.Down)
		{
			_lastDetectedSwipe = SwipeDirections.Down;
			return true;
		}
		
		return false;
	}

	#endregion
}
