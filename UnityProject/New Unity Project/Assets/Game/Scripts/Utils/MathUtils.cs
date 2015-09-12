using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public static class MathUtils
{		
	#region Constants
	
	/// <summary>
	/// Maximum allowed interpolation time.
	/// </summary>
	public const float MAX_INTERPOLATION_TIME = 1.0f;
	
	/// <summary>
	/// Minimum allowed interpolation time.
	/// </summary>
	public const float MIN_INTERPOLATION_TIME = 0.0f;
	
	#endregion

	#region Methods
	
	/// <summary>
	/// Gets a point roated around a pivot.
	/// </summary>
	/// <returns>
	/// The point rotated aruond a the pivot.
	/// </returns>
	/// <param name='pivot'>
	/// Pivot from where to execute the rotation.
	/// </param>
	/// <param name='point'>
	/// Point to be rotated.
	/// </param>
	/// <param name='angle'>
	/// Angle by which to rotate the point.
	/// </param>
	public static Vector2 GetRotatedPointAroundPivot(Vector2 pivot, Vector2 point, float angle)
	{
	    float currentAngle = GetAngleFromPivot(pivot, point);
	    currentAngle      += angle;
	    float radius 	   = Vector2.Distance(pivot, point);
		
	    return GetPointAtDistance(pivot, radius, currentAngle);
	}
	
	/// <summary>
	/// Gets the angle between a point and a pivot. Assumes 0 degrees when point.x < pivot.x and point.y == pivot.y.
	/// </summary>
	/// <returns>
	/// The angle that forms between the point and the pivot.
	/// </returns>
	/// <param name='pivot'>
	/// Pivot.
	/// </param>
	/// <param name='point'>
	/// Point.
	/// </param>
	public static float GetAngleFromPivot(Vector2 pivot, Vector2 point)
	{
		float a = point.y - pivot.y;
   		float b = point.x - pivot.x;
		
   		return (float)RadiansToDegrees( Mathf.Atan2(a,b));
	}
	
	/// <summary>
	/// Gets the point that resides <c>distance</c> units away from <c>center</c> at <c>angle</c> degrees.
	/// </summary>
	/// <returns>
	/// The resulting point.
	/// </returns>
	/// <param name='center'>
	/// The pivot from where to calculate the point.
	/// </param>
	/// <param name='distance'>
	/// The calculated distance from the pivot.
	/// </param>
	/// <param name='angle'>
	/// Angle from the pivot at which the point resides.
	/// </param>
	public static Vector2 GetPointAtDistance(Vector2 center, float distance, float angle)
	{
		float angleInRadians  = (float)DegreesToRadians(angle);
	    float xOffset 		  = Mathf.Cos(angleInRadians) * distance;
	    float yOffset 	      = Mathf.Sin(angleInRadians) * distance;
		
	    return new Vector2(center.x + xOffset,center.y + yOffset);
	}
	
	/// <summary>
	/// Gets the distance between two points.
	/// </summary>
	/// <returns>
	/// The distance between both points.
	/// </returns>
	/// <param name='origin'>
	/// Origin point.
	/// </param>
	/// <param name='other'>
	/// Destination point.
	/// </param>
	public static float GetDistance(Vector2 origin,Vector2 other)
	{
	     float xDistance = GetDistance(origin.x, other.x);
	     float yDistance = GetDistance(origin.y, other.y);
	
	     //C2=A2+B2;
	     return Mathf.Sqrt((xDistance * xDistance) + (yDistance * yDistance));
	}
	
	/// <summary>
	/// Gets the distance that exists between two points in one dimension.
	/// </summary>
	/// <returns>
	/// The resulting distance.
	/// </returns>
	/// <param name='x'>
	/// First point.
	/// </param>
	/// <param name='x2'>
	/// Second point.
	/// </param>
	public static float GetDistance(float x, float x2)
	{
	    return Mathf.Max(x, x2) - Mathf.Min(x, x2);
	}
	
	/// <summary>
	/// Converts angles measured in radians to angles measured in degrees.
	/// </summary>
	/// <returns>
	/// The angle, measured in degrees.
	/// </returns>
	/// <param name='radians'>
	/// The angle measured in radians.
	/// </param>
	public static double RadiansToDegrees(double radians)
	{
	    return Mathf.Rad2Deg * radians;
	}
	
	/// <summary>
	/// Converts angles measured in radians to angles measured in degrees.
	/// </summary>
	/// <returns>
	/// The angle measured in radians.
	/// </returns>
	/// <param name='degrees'>
	/// The angle measured in degrees.
	/// </param>
	public static double DegreesToRadians(double degrees)
	{
	    return Mathf.Deg2Rad * degrees;
	}

	/// <summary>
	/// Calculates a point following a quadratic Bezier curve, during time <c>t</c>.
	/// </summary>
	/// <returns>
	/// The resulting point.
	/// </returns>
	/// <param name='p0'>
	/// Origin point.
	/// </param>
	/// <param name='handle'>
	/// Handle point.
	/// </param>
	/// <param name='p1'>
	/// Destination point.
	/// </param>
	/// <param name='t'>
	/// Time.
	/// </param>
	public static Vector3 QuadraticBezier3D(Vector3 p0, Vector3 handle, Vector3 p1, float t)
	{
		float newX = QuadraticBezier(p0.x, handle.x, p1.x, t);
		float newY = QuadraticBezier(p0.y, handle.y, p1.y, t);
		float newZ = QuadraticBezier(p0.z, handle.z, p1.z, t);
		
		return new Vector3(newX, newY, newZ);
	}

	/// <summary>
	/// Calculates a point following a quadratic Bezier curve, during time <c>t</c>.
	/// </summary>
	/// <returns>
	/// The resulting point.
	/// </returns>
	/// <param name='p0'>
	/// Origin point.
	/// </param>
	/// <param name='handle'>
	/// Handle point.
	/// </param>
	/// <param name='p1'>
	/// Destination point.
	/// </param>
	/// <param name='t'>
	/// Time.
	/// </param>
	public static Vector2 QuadraticBezier2D(Vector2 p0, Vector2 handle, Vector2 p1, float t)
	{
		float newX = QuadraticBezier(p0.x, handle.x, p1.x, t);
		float newY = QuadraticBezier(p0.y, handle.y, p1.y, t);
		
		return new Vector2(newX, newY);
	}
		
	//http://en.wikipedia.org/wiki/Bézier_curve	
	//p0 = p0, p1 = handle, p2 = p1
	
	/// <summary>
	/// Generates a point on a quadratic Bezier curve in one dimension, during time <c>t</c>.
	/// </summary>
	/// <returns>
	/// The resulting value.
	/// </returns>
	/// <param name='p0'>
	/// Origin point.
	/// </param>
	/// <param name='handle'>
	/// Handle point.
	/// </param>
	/// <param name='p1'>
	/// Destination point.
	/// </param>
	/// <param name='t'>
	/// Time.
	/// </param>
	public static float QuadraticBezier(float p0,float handle, float p1, float t)
	{
		return Mathf.Pow(1.0f - t,2.0f) * p0 + 
			   2.0f * t * (1.0f - t) * handle + 
			   Mathf.Pow(t, 2.0f) * p1;
	}
	
	/// <summary>
	/// Calculates a point following a cubic Bezier curve, during time <c>t</c>.
	/// </summary>
	/// <returns>
	/// The resulting point.
	/// </returns>
	/// <param name='p0'>
	/// Origin point.
	/// </param>
	/// <param name='handle0'>
	/// First control point.
	/// </param>
	/// <param name='handle1'>
	/// Second control point.
	/// </param>
	/// <param name='p1'>
	/// Destination point.
	/// </param>
	/// <param name='t'>
	/// Time.
	/// </param>
	public static Vector2 CubicBezier(Vector2 p0, Vector2 handle0, Vector2 handle1, Vector2 p1, float t)
	{
		float newX = CubicBezier(p0.x, handle0.x, handle1.x, p1.x,  t);
		float newY = CubicBezier(p0.y, handle0.y, handle1.y, p1.y, t);
		
		return new Vector2(newX, newY);
	}
	
	//http://en.wikipedia.org/wiki/B%C3%A9zier_curve
	//p0 = p0, p1 = handle0, p2 = handle1, p3 = p1
	
	/// <summary>
	/// Generates a point on a cubic Bezier curve in one dimension, during time <c>t</c>.
	/// </summary>
	/// <returns>
	/// The resulting value.
	/// </returns>
	/// <param name='p0'>
	/// Origin point.
	/// </param>
	/// <param name='handle0'>
	/// First control point.
	/// </param>
	/// <param name='handle1'>
	/// Second control point.
	/// </param>
	/// <param name='p1'>
	/// Destination point.
	/// </param>
	/// <param name='t'>
	/// Time.
	/// </param>
	public static float CubicBezier(float p0, float handle0, float handle1, float p1, float t)
	{
		return (Mathf.Pow(1.0f - t, 3.0f)) * p0  +   
			    3.0f * Mathf.Pow(1.0f - t, 2.0f) * t * handle0  +  
			    3.0f * (1.0f - t) * Mathf.Pow(t, 2.0f) * handle1  +  
			    Mathf.Pow(t, 3.0f) * p1;
	}
	
	/// <summary>
	/// Calculates a point following a linear Bezier curve, during time <c>t</c>.
	/// </summary>
	/// <returns>
	/// The resulting point.
	/// </returns>
	/// <param name='p0'>
	/// Origin point.
	/// </param>
	/// <param name='p1'>
	/// Destination point.
	/// </param>
	/// <param name='t'>
	/// Time.
	/// </param>
	public static Vector2 LinearBezier(Vector2 p0, Vector2 p1, float t)
	{
		return new Vector2(LinearBezier(p0.x, p1.x, t), LinearBezier(p0.y, p1.y, t)); 
	}
	
	//http://en.wikipedia.org/wiki/B%C3%A9zier_curve
	/// <summary>
	/// Generates a point on a linear Bezier curve in one dimension, during time <c>t</c>.
	/// </summary>
	/// <returns>
	/// The resulting value.
	/// </returns>
	/// <param name='p0'>
	/// Origin point.
	/// </param>
	/// <param name='p1'>
	/// Destination point.
	/// </param>
	/// <param name='t'>
	/// Time.
	/// </param>
	public static float LinearBezier(float p0, float p1, float t)
	{
		return p0 + t  * ( p1 - p0 ); 
	}
	
	//http://en.wikipedia.org/wiki/B%C3%A9zier_curve
	
	/// <summary>
	/// Generates a point on an inverse linear Bezier curve in one dimension, during time <c>t</c>.
	/// </summary>
	/// <returns>
	/// The resulting value.
	/// </returns>
	/// <param name='n'>
	/// Time.
	/// </param>
	/// <param name='p0'>
	/// Origin point.
	/// </param>
	/// <param name='p1'>
	/// Destination point.
	/// </param>
	public static float InverseLinearBezier(float n, float p0, float p1)
	{
		//Debug.Log(" n = " + n + " p0 = " + p0 + " p1 = " + p1);
		return (n - p0) / (p1 - p0); 
	}
	
	/// <summary>
	/// Transforms a <see cref="Vector3" /> into a human-readable string.
	/// </summary>
	/// <returns>
	/// A string representing the inut vector.
	/// </returns>
	/// <param name='vector'>
	/// The vector to be converted.
	/// </param>
	public static string Vector3ToString(Vector3 vector)
	{
		return "[" + vector.x +"," + vector.y + "," + vector.z + "]";
	}

	public static float Round(float value, float digits)
	{
		return Mathf.Round(value * digits) / digits;
	}
	
	//This comes from iTween
	/// <summary>
	/// Generates intermediate points that pass through the points denoted at <c>pts</c>.
	/// </summary>
	/// <param name='pts'>
	/// List of points used as a reference.
	/// </param>
	/// <param name='t'>
	/// Time step.
	/// </param>
	/// <returns>
	/// The calculated intermediate point.
	/// </returns>
	public static Vector3 Interp(List<Vector3> pts, float t) 
	{
		int numSections = pts.Count - 3;
		int currPt 		= Mathf.Min(Mathf.FloorToInt(t * (float) numSections), numSections - 1);
		float u 		= t * (float) numSections - (float) currPt;
				
		Vector3 a = pts[currPt];
		Vector3 b = pts[currPt + 1];
		Vector3 c = pts[currPt + 2];
		Vector3 d = pts[currPt + 3];
		
		return .5f * (
			(-a + 3f * b - 3f * c + d) * (u * u * u)
			+ (2f * a - 5f * b + 4f * c - d) * (u * u)
			+ (-a + c) * u
			+ 2f * b
		);
	}
	
	public static List<Vector3> GeneratePointsAtDistance(List<Vector3> points, float distance)
	{
		if(points.Count > 3)
		{
			float estimatedDistance 			 = CalculateDistance(points);
			float timeIncrementPerPointsDistance = distance / estimatedDistance;
			float currentTime 					 = 0;
			List<Vector3> smoothedPoint 		 = new List<Vector3>();
		
			while(currentTime < 1)
			{
				Vector3 newPoint = Interp(points, currentTime);
				smoothedPoint.Add(newPoint);
			
				currentTime += timeIncrementPerPointsDistance;
			}
		
			return smoothedPoint;
		}
		
		return points;
	}
	
	private static float CalculateDistance(List<Vector3> points)
	{
		float distance = 0;
		
		if(points.Count > 0)
		{
			Vector3 previousPoint = points[0];
			
			for(int x = 1; x < points.Count; x++) 
			{
				Vector3 currentPoint = points[x];
				distance			+= Vector3.Distance(previousPoint, currentPoint);
				previousPoint        = currentPoint;
			}
		}
		
		return distance;
	}
	
	public static void RenderGuiznosPath(List<Vector3> points, Color color)
	{
		if(points.Count > 0)
		{
			Vector3 previousPoint = points[0];
			Gizmos.color          = color;
			
			for(int x = 1; x < points.Count; x++) 
			{
				Vector3 currentPoint = points[x];
				Gizmos.DrawLine(previousPoint, currentPoint);
				previousPoint        = currentPoint;
			}
		}
	}

	public static bool Approximately(Vector2 a, Vector2 b, float offset)
	{
		float xOffset = Mathf.Abs(a.x - b.x);
		float yOffset = Mathf.Abs(a.y - b.y);

		return xOffset <= offset && yOffset <= offset;
	}

    public static bool Approximately(float a, float b, float offset)
    {
        return Mathf.Abs(a - b) <= offset;
    }
	
	#endregion
}
