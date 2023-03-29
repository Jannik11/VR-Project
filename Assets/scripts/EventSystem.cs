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

    public event Action OnArrowTake;
    public void TriggerOnArrowTake()
    {
        if (OnArrowTake != null)
        {
            OnArrowTake();
        }
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

    public event Action OnArrowHit;
    public void TriggerOnArrowHit()
    {
        if (OnArrowHit != null)
        {
            OnArrowHit();
        }
    }
}
