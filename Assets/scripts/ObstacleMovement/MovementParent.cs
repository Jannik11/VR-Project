using UnityEngine;

/// <summary>
/// Oberklasse für alle Bewegungsskripte
/// </summary>
public class MovementParent : MonoBehaviour
{
    bool running = true;
    // Start is called before the first frame update
    void Start()
    {
        EventManager.current.OnBowGrab += BowGrabbed;
        EventManager.current.OnBowRelease += BowReleased;
        StartScript();
    }

    /// <summary>
    /// Pause-Status wird aktualisiert, wenn der Bogen gegriffen oder losgelassen wurde
    /// </summary>
    public void BowReleased() {
        running = false;
    }
    public void BowGrabbed(Side obj) {
        running = true;
    }

    /// <summary>
    /// Methoden werden von den erbenden Klassen implementiert
    /// </summary>
    public virtual void UpdateMovement() {

    }
    public virtual void StartScript() {

    }

    /// <summary>
    /// Wenn das Spiel nicht pausiert ist, werden die Bewegungsskripte aufgerufen
    /// </summary>
    void Update()
    {
        if (running) UpdateMovement();
    }
}
