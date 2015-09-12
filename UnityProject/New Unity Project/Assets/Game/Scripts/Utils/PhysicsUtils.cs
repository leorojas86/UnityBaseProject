using UnityEngine;
using System.Collections;

public static class PhysicsUtils
{
    public static RaycastHit GetNearestHit(RaycastHit[] hits, Vector3 position)
    {
        RaycastHit nearestHit = hits[0];
        float nearestDistance = Vector3.Distance(nearestHit.point, position);
  
        for(int x = 1; x < hits.Length; x++)
        {
            RaycastHit currentHit = hits[x];
            float currentDistance  = Vector3.Distance(currentHit.point, position);

            if(nearestDistance > currentDistance)
            {
                nearestDistance = currentDistance;
                nearestHit      = currentHit;
            }
        }

        return nearestHit;
    }

    public static Vector3 GetCurrentMousePositionRaycastHitPoint(float maxRaycastDistance)
    {
        Ray touchRay      = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit[] hits = Physics.RaycastAll(touchRay);

        if(hits.Length > 0)
            return GetNearestHit(hits, Camera.main.transform.position).point;
        else
            return touchRay.GetPoint(maxRaycastDistance);
    }
}
