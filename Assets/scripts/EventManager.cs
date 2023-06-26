using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    public static EventManager current;

    private void Awake()
    {
        current = this;
    }

    private void Update() {
        //Debug.Log("Bow: " + Bow.instance.BowState + "\t\t\tArrow: " + Quiver.instance.CurrentArrow?.ArrowState + "\t\t\tHands: " + Hands.instance.Left + " " + Hands.instance.Right);
    }

    #region Arrow
    
    public event Action<Side> OnArrowGrab;
    public void TriggerOnArrowGrab(Side side)
    {
        if (OnArrowGrab != null)
        {
            Debug.Log("ArrowGrab " + side);
            OnArrowGrab(side);
        }
    } 

    public event Action OnArrowNock;
    public void TriggerOnArrowNock()
    {
        if (OnArrowNock != null)
        {
            Debug.Log("ArrowNock");
            OnArrowNock();
        }
    }

    public event Action OnArrowShoot;
    public void TriggerOnArrowShoot()
    {
        if (OnArrowShoot != null)
        {
            Debug.Log("ArrowShoot");
            OnArrowShoot();
        }
    }

    public event Action<int, Collider> OnArrowHit;
    public void TriggerOnArrowHit(int id, Collider collider)
    {
        if (OnArrowHit != null)
        {
            Debug.Log("ArrowHit");
            OnArrowHit(id, collider);
        }
    } 
    public event Action OnArrowHitGlass;
    public void TriggerOnArrowHitGlass()
    {
        if (OnArrowHitGlass != null)
        {
            OnArrowHitGlass();
        }
    } 

    #endregion

    #region Bow
    public event Action<Side> OnStringGrab;
    public void TriggerOnStringGrab(Side side) {
        if (OnStringGrab != null) {
            Debug.Log("StringGrab");
            OnStringGrab(side);
        }
    }

    public event Action OnStringRelease;
    public void TriggerOnStringRelease() {
        if (OnStringRelease != null) {
            Debug.Log("StringRelease");
            OnStringRelease();
        }
    }

    public event Action<Side> OnBowGrab;
    public void TriggerOnBowGrab(Side side) {
        if (OnBowGrab != null) {
            Debug.Log("StringGrab");
            OnBowGrab(side);
        }
    }


    public event Action OnBowRelease;
    public void TriggerOnBowRelease() {
        if (OnBowRelease != null) {
            Debug.Log("StringRelease");
            OnBowRelease();
        }
    }
    #endregion
    
    #region Highscore
    public event Action OnGameStart;
    public void TriggerOnGameStart() {
        if (OnGameStart != null)
        {
            OnGameStart();
        }
    }   
    
    public event Action OnGameResume;
    public void TriggerOnGameResume() {
        if (OnGameResume != null)
        {
            OnGameResume();
        }
    } 

    public event Action OnPlayerDeath;
    public void TriggerOnPlayerDeath() {
        if (OnPlayerDeath != null)
        {
            OnPlayerDeath();
        }
    }   
    public event Action OnGamePause;
    public void TriggerOnGamePause() {
            OnGamePause?.Invoke();
    }  
    public event Action OnPlayerHit;
    public void TriggerOnPlayerHit() {
        OnPlayerHit?.Invoke();
    }

    public event Action OnPlayerGetLifepoint;
    public void TriggerOnPlayerGetLifepoint()
    {
        if(OnPlayerGetLifepoint != null)
        {
            OnPlayerGetLifepoint();
        }
    }


    #endregion

    #region Level

    public event Action OnLevelSwitch;
    public void TriggerOnLevelSwitch() {
            OnLevelSwitch?.Invoke();
    }

    #endregion
}
