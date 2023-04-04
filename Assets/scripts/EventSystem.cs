using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventSystem : MonoBehaviour
{
    public static EventSystem current;

    private void Awake()
    {
        current = this;
    }

    public event Action OnArrowNock;
    public void TriggerOnArrowNock()
    {
        if (OnArrowNock != null)
        {
            OnArrowNock();
        }
    }

    public event Action OnArrowShoot;
    public void TriggerOnArrowShoot()
    {
        if (OnArrowShoot != null)
        {
            OnArrowShoot();
        }
    }

    public event Action<Collider> OnArrowHit;
    public void TriggerOnArrowHit(Collider collider)
    {
        if (OnArrowHit != null)
        {
            OnArrowHit(collider);
        }
    } 
    
    public event Action<Side> OnArrowGrab;
    public void TriggerOnArrowGrab(Side side)
    {
        if (OnArrowGrab != null)
        {
            OnArrowGrab(side);
        }
    }


}
