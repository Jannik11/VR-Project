using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementParent : MonoBehaviour
{
    bool running = true;
    // Start is called before the first frame update
    void Start()
    {
        EventManager.current.OnBowGrab += BowGrabbed;
        EventManager.current.OnBowRelease += BowReleased;
        StartScript();
    }

    public void BowReleased() {
        running = false;
    }

    public void BowGrabbed(Side obj) {
        Debug.Log("DEBUG: JA ICH MACHE HIER WAS");
        running = true;
    }

    public virtual void UpdateMovement() {

    }

    public virtual void StartScript() {

    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("DEBUG::: " + running);
        if (running) UpdateMovement();
    }
}
