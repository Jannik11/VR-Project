using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ObstacleMovementRotateUp : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] float triggerDistance;
    [SerializeField] float playerPosZ = 0.0f;
    [SerializeField] Transform finalTransform;

    MeshRenderer meshRender;

    float angle = 0.0f;
    bool wasTriggered = false;

    Quaternion finalPosition;
    Quaternion originalPosition;

    // Start is called before the first frame update
    void Start()
    {
        meshRender = GetComponent<MeshRenderer>();
        Vector3 translate = new Vector3(0, -meshRender.bounds.extents.y, -meshRender.bounds.extents.z);

        originalPosition = transform.rotation;
        finalPosition = finalTransform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        if(!wasTriggered && Mathf.Abs(transform.position.z - playerPosZ) < triggerDistance)
        {
            float movement = speed * Time.deltaTime;
            transform.RotateAround(finalTransform.position, new Vector3(1, 0, 0), -movement);

            angle += movement;

            wasTriggered = angle > 90.0f;
        }
    }
}
