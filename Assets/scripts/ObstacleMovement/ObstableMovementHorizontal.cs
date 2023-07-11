using UnityEngine;

/// <summary>
/// Bewegungsskript für horizontale Bewegung
/// </summary>
public class ObstableMovementHorizontal : MovementParent {

    /// <summary>
    /// Linke und rechte Grenze der Bewegung und Geschwindigkeit
    /// </summary>
    [SerializeField] float left;
    [SerializeField] float right;
    [SerializeField] float speed;

    bool movementAlternater = true;

    /// <summary>
    /// Startposition und Richtung werden zufällig initialisiert
    /// </summary>
    public override void StartScript() {
        movementAlternater = Random.Range(0, 2) == 0;
        transform.position = new Vector3(Random.Range(left, right), transform.position.y, transform.position.z);
    }

    /// <summary>
    /// Glasscheibe wird translatiert
    /// </summary>
    public override void UpdateMovement() {
        float movement = (movementAlternater ? speed : -speed) * Time.deltaTime;
        transform.Translate(new Vector3(movement, 0, 0));
        if ((movementAlternater && transform.position.x > right) || (!movementAlternater && transform.position.x < left)) {
            movementAlternater = !movementAlternater;
        }
    }
}
