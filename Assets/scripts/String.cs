using UnityEngine;

public class String : MonoBehaviour {

    /// <summary>
    /// L�st das Event des Pfeil anlegens aus, wenn die Sehne
    /// mit einer Hand kollidiert, die einen Pfeil tr�gt
    /// </summary>
    /// <param name="other">Collider der mit der Sehne kollidiert</param>
    private void OnTriggerEnter(Collider other) {
        if (Hands.instance.IsArrowInAnyHand()) {
            if (other.CompareTag("Arrow") 
                && Bow.instance.BowState == BowState.IDLE
                && Quiver.instance.CurrentArrow.ArrowState == ArrowState.INHAND) {
                EventManager.current.TriggerOnArrowNock();

            }
        }
    }
}
