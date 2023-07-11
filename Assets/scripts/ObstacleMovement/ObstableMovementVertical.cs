using UnityEngine;

/// <summary>
/// Bewegungsskript für vertikale Bewegung
/// </summary>
public class ObstableMovementVertical : MovementParent {

    /// <summary>
    /// Obere und untere Grenze der Bewegung und Geschwindigkeit
    /// </summary>
    [SerializeField] float top;
    [SerializeField] float bottom;
    [SerializeField] float speed;

    bool movementAlternater = true;

    /// <summary>
    /// Startposition und Richtung werden zufällig initialisiert
    /// </summary>
    public override void StartScript() {
        movementAlternater = Random.Range(0, 2) == 0;
        transform.position = new Vector3(transform.position.x, Random.Range(bottom, top), transform.position.z);
    }

    /// <summary>
    /// Glasscheibe wird translatiert
    /// </summary>
    public override void UpdateMovement() {
        float movement = (movementAlternater ? speed : -speed) * Time.deltaTime;
        transform.Translate(new Vector3(0, movement, 0));
        if ((movementAlternater && transform.position.y > top) || (!movementAlternater && transform.position.y < bottom)) {
            movementAlternater = !movementAlternater;
        }
    }
}
