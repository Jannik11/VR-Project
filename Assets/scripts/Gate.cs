using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gate : MonoBehaviour
{
    public Transform left;
    public Transform right;
    public Transform up;
    public Transform down;

    public Color activationColor;

    public float speed = 5.0f;
    private bool targetHit = false;

    private float distanceTraveled = 0.0f;
    private float maxDistanceTraveled = 6.0f;

    private Renderer meshRenderer;

    private void Start()
    {
        meshRenderer = GetComponent<Renderer>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Arrow"))
        {
            targetHit = true;
            meshRenderer.material.color = activationColor;
        }
    }

    private void Update()
    {
        if(targetHit && distanceTraveled < maxDistanceTraveled)
        {
            float distance = speed * Time.deltaTime;
            distanceTraveled += distance;
            left.transform.Translate(new Vector3(-distance, 0.0f, 0.0f));
            right.transform.Translate(new Vector3(distance, 0.0f, 0.0f));
            up.transform.Translate(new Vector3(0.0f, distance, 0.0f));
            down.transform.Translate(new Vector3(0.0f, -distance, 0.0f));
        }
    }
}
