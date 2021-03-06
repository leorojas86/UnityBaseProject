﻿using UnityEngine;
using System.Collections;

public static class Constants 
{
	#region Constants

	public const float CHARACTER_LANDED_Y_VELOCITY_THRESHOLD 			= 2.5f;
	public const float CHARACTER_DEFAULT_HEALTH 			   	= 1000;
	public const float CHARACTER_DEFAULT_SPEED			   		= 10.0f;
	public const float CHARACTER_MOVEMENT_FLYING_MULTIPLIER  	= 0.1f;
    public const float CHARACTER_MOVEMENT_BEND_MULTIPLIER       = 0.4f;
	public const float CHARACTER_MOVEMENT_LERP_SPEED	   		= 0.1f;
    public const float CHARACTER_BENDING_SPEED	   		        = 3f;
	public const float CHARACTER_JUMP_FORCE 			   		= 10f;
    public const float CHARACTER_STAND_X_ROTATION               = 0f;
    public const float CHARACTER_BEND_X_ROTATION                = 90f;
    public const float CHARACTER_BREAK_LERP                     = 0.1f;
    public const float CHARACTER_MAX_FALLING_Y_VELOCITY         = 20f;

	public const float KEYBOARD_ROTATION_SPEED  = 2f;
	public const float MOUSE_ROTATION_SPEED	    = 8f;

    public const float TOUCH_INPUT_SWIPE_GESTURE_MIN_DISTANCE   = 15f;
    public const float TOUCH_INPUT_MIN_TOUCH_DISTANCE           = 2f;
    public const float TOUCH_INPUT_MAX_RAYCAST_DISTANCE         = 10000f;

	#endregion
}
