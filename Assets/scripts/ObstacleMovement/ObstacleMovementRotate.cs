using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleMovementRotate : MovementParent
{
    [SerializeField] float speed;
    [SerializeField] Transform finalTransform;
    [SerializeField] List<Transform> rotationObjects;

    public override void UpdateMovement()
    {
        float movement = speed * Time.deltaTime;

        foreach (Transform rotationObj in rotationObjects)
        {
            rotationObj.RotateAround(finalTransform.position, new Vector3(0, 0, 1), -movement);
        }
    }
}
