using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Bewegungsskript für horizontale Bewegung zweier verbundener Glasscheiben
/// </summary>
public class ObstacleMovementLinkedHorizontal : MovementParent
{
    /// <summary>
    /// Linke und rechte Grenze der Bewegung
    /// Abstand der zwei Scheiben zueinander
    /// Geschwindigkeit
    /// </summary>
    [SerializeField] float leftMaxDistance;
    [SerializeField] float rightMaxDistance;
    [SerializeField] float distanceBetweenTargets;
    [SerializeField] float speed;

    /// <summary>
    /// Transforms der Glasscheiben
    /// </summary>
    [SerializeField] Transform leftTarget;
    [SerializeField] Transform rightTarget;

    bool movementAlternater = true;

    private float leftTargetX = 0.0f;
    private float rightTargetX = 0.0f;

    /// <summary>
    /// Startposition und Richtung werden zufällig initialisiert
    /// </summary>
    public override void StartScript() {
        movementAlternater = Random.Range(0, 2) == 0;
        float spawnMiddlePoint = Random.Range(leftMaxDistance + distanceBetweenTargets / 2.0f,
            rightMaxDistance - distanceBetweenTargets / 2.0f);

        leftTargetX = spawnMiddlePoint - distanceBetweenTargets / 2.0f;
        leftTarget.position = new Vector3(leftTargetX, leftTarget.position.y, leftTarget.position.z);

        rightTargetX = spawnMiddlePoint + distanceBetweenTargets / 2.0f;
        rightTarget.position = new Vector3(rightTargetX, rightTarget.position.y, rightTarget.position.z);
    }

    /// <summary>
    /// Glasscheibe wird translatiert
    /// </summary>
    public override void UpdateMovement() {
        float movement = (movementAlternater ? speed : -speed) * Time.deltaTime;

        if (leftTarget != null) {
            leftTarget.Translate(new Vector3(movement, 0, 0));
        }

        if (rightTarget != null) {
            rightTarget.Translate(new Vector3(movement, 0, 0));
        }

        leftTargetX += movement;
        rightTargetX += movement;

        if ((movementAlternater && rightTargetX > rightMaxDistance) || (!movementAlternater && leftTargetX < leftMaxDistance)) {
            movementAlternater = !movementAlternater;
        }
    }
    
}
