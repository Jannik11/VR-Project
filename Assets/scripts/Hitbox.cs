using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hitbox : MonoBehaviour
{
    private void OnTriggerEnter(Collider other) {

        if(other.CompareTag("Glass")) {
            Destroy(other.gameObject);
            EventManager.current.TriggerOnPlayerHit();
        }

        if(other.CompareTag("Gate")) {
            Destroy(other.gameObject);
            EventManager.current.TriggerOnPlayerHit();
            EventManager.current.TriggerOnLevelSwitch();
        }

    }
}
