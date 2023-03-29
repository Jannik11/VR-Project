using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class String : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("NockingArrow")) { 
            EventSystem.current.TriggerOnArrowNock();
        }
    }
}
