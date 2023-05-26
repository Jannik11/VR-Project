using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class String : MonoBehaviour {
    private void OnTriggerEnter(Collider other) {
       // Debug.Log("Tag: " + other.tag + "\t\tBowState: " + (Bow.instance.BowState) + "\t\tHands: " + Hands.instance.Left + " " + Hands.instance.Right + "currentArrow: " + Quiver.instance.CurrentArrow.id);
        if (Hands.instance.IsArrowInAnyHand()) {
            if (other.CompareTag("Arrow") 
                && Bow.instance.BowState == BowState.IDLE
                && Quiver.instance.CurrentArrow.ArrowState == ArrowState.INHAND) {
                EventManager.current.TriggerOnArrowNock();

            }
        }
    }
}
