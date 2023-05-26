using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleTimer : MonoBehaviour {
    public float Timer{ get; private set;} = 0.0f ;

    private bool running = false;

    // Update is called once per frame
    void Update()
    {
        if (running)
        {
            Timer += Time.deltaTime;
        }
    }

    public void ResumeTimer()
    {
        running = true;
    }
    public void PauseTimer()
    {
        running = false;
    }

    public float EndTimer()
    {
        running = false;
        float toReturn = Timer;
        Timer = 0.0f;
        return toReturn;
    }
}