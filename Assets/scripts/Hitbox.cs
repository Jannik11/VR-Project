using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hitbox : MonoBehaviour
{
    private void OnTriggerEnter(Collider other) {
        Debug.Log("OnTriggerEnter Hitbox");
        if(other.CompareTag("Glass")) {
            Destroy(other.gameObject);
            EventManager.current.TriggerOnPlayerHit();
        }
    }
}
