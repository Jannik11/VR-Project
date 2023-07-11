using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hitbox : MonoBehaviour
{
    /// <summary>
    /// stellt die Hitbox des Spielers da, 
    /// wenn ein Spieler eine Scheibe oder einen Leveltrenner trifft wird das entsprechende Event ausgelöst.
    /// </summary>
    private void OnTriggerEnter(Collider other) {

        if(other.CompareTag("Glass")) {
            Destroy(other.gameObject);
            EventManager.current.TriggerOnPlayerHit();
        }

        if(other.CompareTag("Gate")) {
            other.transform.parent.gameObject.SetActive(false);
            EventManager.current.TriggerOnPlayerHit();
            EventManager.current.TriggerOnLevelSwitch();
        }

    }
}
