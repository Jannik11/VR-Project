using UnityEngine;

/// <summary>
/// Bewegungsskript zum Aufstellen einer Glasscheibe
/// </summary>
public class ObstacleMovementRotateUp : MovementParent {

    /// <summary>
    /// Geschwindigkeit des Aufrichtens
    /// Distanz bei der die Scheibe aufgerichtet wird
    /// Spielerposition z-Koordinate
    /// Punkt, um den die Scheibe um 90 Grad rotiert wird
    /// </summary>
    [SerializeField] float speed;
    [SerializeField] float triggerDistance;
    [SerializeField] float playerPosZ = 0.0f;
    [SerializeField] Transform pivotPoint;

    float angle = 0.0f;
    bool wasTriggered = false;

    /// <summary>
    /// Rotiert die Glasscheibe um 90 Grad in eine aufgerichtete Position
    /// </summary>
    public override void UpdateMovement() {
        if (!wasTriggered && Mathf.Abs(transform.position.z - playerPosZ) < triggerDistance) {
            float movement = speed * Time.deltaTime;
            transform.RotateAround(pivotPoint.position, new Vector3(1, 0, 0), -movement);

            angle += movement;

            wasTriggered = angle > 90.0f;
        }
    }
}
