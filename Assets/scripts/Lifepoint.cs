using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lifepoint : MonoBehaviour
{
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
