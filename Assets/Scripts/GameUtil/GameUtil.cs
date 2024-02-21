
using UnityEngine;

public class GameUtil
{
    private static Camera CameraCache = Camera.main;
    private static Vector3 _lastPointPosition;
    public static Vector3 MouseProjection()
    {
        return _lastPointPosition;
        
    }

    public static bool IsCursorGround()
    {
        if (CameraCache == null)
            CameraCache = Camera.main;
        if (Physics.Raycast(CameraCache.ScreenPointToRay(Input.mousePosition),
                out RaycastHit hit, Mathf.Infinity,
                GameMasks.GroundLayer))
        {
            _lastPointPosition = hit.point;
            return true;
        }
        return false;
    }

}
