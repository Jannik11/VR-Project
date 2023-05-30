using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleMovementRapidHorizontal : MonoBehaviour
{
    [SerializeField] float left;
    [SerializeField] float right;
    [SerializeField] float speed;
    [SerializeField] int intervalCount;
    [SerializeField] float delay;

    float[] intervalPositions;

    bool movementAlternater = true;
    float num = 0.0f;
    float timeSinceLastMove = 0.0f;
    bool moving = false;

    int currInterval = 0;
    // Start is called before the first frame update
    void Start()
    {
        intervalPositions = new float[intervalCount];

        num = (right - left) / (intervalCount - 1);
        currInterval = (intervalCount - 1) / 2;
        transform.position = new Vector3(left + currInterval * num, transform.position.y, transform.position.z);

        for (int i = 0; i < intervalCount + 1; i++)
        {
            intervalPositions[i] = left + i * num;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
        if(moving)
        {
            float movement = (movementAlternater ? speed : -speed) * Time.deltaTime;
            transform.Translate(new Vector3(movement, 0, 0));

            if((movementAlternater && transform.position.x > intervalPositions[currInterval] + num) 
                || (!movementAlternater && transform.position.x < intervalPositions[currInterval] - num))
            {
                moving = false;
                currInterval += movementAlternater ? 1 : -1;

                if(currInterval <= 0 || currInterval >= intervalCount - 1)
                {
                    movementAlternater = !movementAlternater;
                }
            }
        } else
        {
            timeSinceLastMove += Time.deltaTime;

            if(timeSinceLastMove > delay)
            {
                timeSinceLastMove = 0.0f;
                moving = true;
            }
        }

    }
}
