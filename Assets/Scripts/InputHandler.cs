using UnityEngine;

public class InputHandler : MonoBehaviour
{
    private Collider[] _sheeps;
    private Vector3 _projectionMouse;
    private const float _timeStep = .1f;
    private float _lastInput;

    void Update()
    {
        if (Input.GetMouseButton(0) && GameUtil.IsCursorGround())
        {
            if ((Time.time - _lastInput) < _timeStep) return;

            _lastInput = Time.time;

            _projectionMouse = GameUtil.MouseProjection();
            _projectionMouse.y = 1;

            _sheeps = Physics.OverlapSphere(_projectionMouse, 3f);

            foreach (var VARIABLE in _sheeps)
            {
                SheepMechanic sheepObject = VARIABLE.GetComponent<SheepMechanic>();
                if (sheepObject && !sheepObject.isInPaddock)
                {
                    Vector3 newTargetPosition = sheepObject.transform.position +
                        (sheepObject.transform.position - _projectionMouse).normalized * 1.3f;

                    if (!Physics.SphereCast(sheepObject.transform.position, 1.4f,
                        newTargetPosition - sheepObject.transform.position, out RaycastHit hit, 1.4f, GameMasks.ObstacleLayer | GameMasks.AnimalLayer))
                    {
                        Debug.DrawLine(sheepObject.transform.position, newTargetPosition, Color.green); // Линия зеленая
                        sheepObject.SetNewPosition(newTargetPosition);
                    }
                    //  else
                    //  {
                    //      if (!sheepObject.IsInPaddock) return;
                    //      Vector3 newPos = sheepObject.transform.position + -newTargetPosition.normalized;
                    //      if (!Physics.SphereCast(sheepObject.transform.position, 1.3f,
                    //              newPos - sheepObject.transform.position, out RaycastHit newHit, 1.2f,GameMasks.ObstacleLayer | GameMasks.AnimalLayer))
                    //      {
                    //          Debug.DrawLine(sheepObject.transform.position, newTargetPosition, Color.green); // Линия зеленая
                    //          
                    //          newPos.y = 1;
                    //          sheepObject.SetNewPosition(newPos);
                    //      }
                    //  }
                    //}
                }
            }
        }

    }
}