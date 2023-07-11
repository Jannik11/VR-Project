using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Bewegungsskript für sprunghafte, horizontale Bewegung
/// </summary>
public class ObstacleMovementRapidHorizontal : MovementParent {

    /// <summary>
    /// Linke und rechte Grenze
    /// Geschwindigkeit
    /// Anzahl der Sprungintervalle
    /// Zeit zwischen den Sprüngen
    /// </summary>
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

    /// <summary>
    /// Translation der Scheiben
    /// </summary>
    public override void UpdateMovement() {
        if (moving) {
            float movement = (movementAlternater ? speed : -speed) * Time.deltaTime;
            transform.Translate(new Vector3(movement, 0, 0));

            if ((movementAlternater && transform.position.x > intervalPositions[currInterval] + num)
                || (!movementAlternater && transform.position.x < intervalPositions[currInterval] - num)) {
                moving = false;
                currInterval += movementAlternater ? 1 : -1;

                if (currInterval <= 0 || currInterval >= intervalCount - 1) {
                    movementAlternater = !movementAlternater;
                }
            }
        } else {
            timeSinceLastMove += Time.deltaTime;

            if (timeSinceLastMove > delay) {
                timeSinceLastMove = 0.0f;
                moving = true;
            }
        }
    }

    /// <summary>
    /// Initialisierung der Scheibe
    /// </summary>
    public override void StartScript() {
        intervalPositions = new float[intervalCount];

        num = (right - left) / (intervalCount - 1);
        currInterval = (intervalCount - 1) / 2;
        transform.position = new Vector3(left + currInterval * num, transform.position.y, transform.position.z);

        for (int i = 0; i < intervalCount; i++) {
            intervalPositions[i] = left + i * num;
        }
    }
}
