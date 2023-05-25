using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleTimer : MonoBehaviour
{ 
    private float timer = 0.0f;

    private bool running = false;

    // Update is called once per frame
    void Update()
    {
        if (running)
        {
            timer += Time.deltaTime;
        }
    }

    public void StartTimer()
    {
        running = true;
    }

    public float EndTimer()
    {
        running = false;
        float toReturn = timer;
        timer = 0.0f;
        return toReturn;
    }
}