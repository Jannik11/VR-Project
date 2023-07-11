using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Ziel, welches dem Spieler ein Leben wiedergibt
/// </summary>
public class Lifepoint : MonoBehaviour
{
    /// <summary>
    /// Wird das Ziel von einem Pfeil getroffen,
    /// wird das entsprechende Event ausgelöst
    /// und Pfeil und Ziel zerstört
    /// </summary>
    /// <param name="other">Collider, der mit dem Lebensziel kollidiert</param>
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Arrow"))
        {
            Destroy(other.gameObject);
            EventManager.current.TriggerOnPlayerGetLifepoint();
            Destroy(gameObject);
        }
    }
}
