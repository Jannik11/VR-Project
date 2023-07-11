using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Bewegungsskript für vertikale Bewegung zweier verbundener Glasscheiben
/// </summary>
public class ObstacleMovementLinkedVertical : MovementParent
{
    /// <summary>
    /// Obere und untere Grenze der Bewegung
    /// Abstand der zwei Scheiben zueinander
    /// Geschwindigkeit
    /// </summary>
    [SerializeField] float topMaxDistance;
    [SerializeField] float bottomMaxDistance;
    [SerializeField] float distanceBetweenTargets;
    [SerializeField] float speed;

    /// <summary>
    /// Transforms der Glasscheiben
    /// </summary>
    [SerializeField] Transform topTarget;
    [SerializeField] Transform bottomTarget;

    bool movementAlternater = true;

    private float topTargetY = 0.0f;
    private float bottomTargetY = 0.0f;

    /// <summary>
    /// Startposition und Richtung werden zufällig initialisiert
    /// </summary>
    public override void StartScript() {
        movementAlternater = Random.Range(0, 2) == 0;
        float spawnMiddlePoint = Random.Range(bottomMaxDistance + distanceBetweenTargets / 2.0f,
            topMaxDistance - distanceBetweenTargets / 2.0f);

        topTargetY = spawnMiddlePoint + distanceBetweenTargets / 2.0f;
        topTarget.position = new Vector3(topTarget.position.x, topTargetY, topTarget.position.z);

        bottomTargetY = spawnMiddlePoint - distanceBetweenTargets / 2.0f;
        bottomTarget.position = new Vector3(bottomTarget.position.x, bottomTargetY, bottomTarget.position.z);
    }

    /// <summary>
    /// Glasscheibe wird translatiert
    /// </summary>
    public override void UpdateMovement() {
        float movement = (movementAlternater ? speed : -speed) * Time.deltaTime;

        if (topTarget != null) {
            topTarget.Translate(new Vector3(0, movement, 0));
        }

        if (bottomTarget != null) {
            bottomTarget.Translate(new Vector3(0, movement, 0));
        }

        topTargetY += movement;
        bottomTargetY += movement;

        if ((movementAlternater && topTargetY > topMaxDistance) || (!movementAlternater && bottomTargetY < bottomMaxDistance)) {
            movementAlternater = !movementAlternater;
        }
    }
}
