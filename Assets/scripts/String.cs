using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class String : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Arrow") && Bow.instance.BowState == BowState.NOCKING) { 
            EventSystem.current.TriggerOnArrowNock();
        }
    }
}
