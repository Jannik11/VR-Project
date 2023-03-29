using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VirtualGrasp;

public class Bow : MonoBehaviour
{
    [SerializeField] private Transform top;
    [SerializeField] private Transform mid;
    [SerializeField] private Transform bot;
    
    [SerializeField] private Transform forceTarget;


    [SerializeField] private GameObject arrow;

    private LineRenderer lineRenderer;
    private Rigidbody rb;

    private float midDefaultOffset;
    private bool stringGrabbed = false;

    private GameObject currentArrow;

    private float arrowSpeed = 1500.0f;

    // Start is called before the first frame update
    void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();

        midDefaultOffset = mid.localPosition.x;

        VG_Controller.OnObjectFullyReleased.AddListener(ReleaseString);
        VG_Controller.OnObjectGrasped.AddListener(GrabString);

    }


    // Update is called once per frame
    void Update()
    {
       // if(currentArrow == null)
       // {
       //     return;
       // }

        if (stringGrabbed)
        {
            mid.localPosition = new Vector3(Mathf.Clamp(mid.localPosition.x, midDefaultOffset, 60.0f), transform.position.y, transform.position.z);
            currentArrow.transform.LookAt(forceTarget, Vector3.up);

        } else
        {
            mid.localPosition = new Vector3(midDefaultOffset, transform.position.y, transform.position.z);
        }

        Vector3[] positions = new Vector3[] { top.position, mid.position, bot.position };
        lineRenderer.SetPositions(positions);
        

    }

    private void ReleaseString(VG_HandStatus arg0)
    {
        stringGrabbed = false;

        Vector3 forceVector = forceTarget.position - mid.position;

        currentArrow.transform.parent = null;

        rb.AddForce(forceVector * arrowSpeed);
        rb.useGravity = true;
    }

    private void GrabString(VG_HandStatus arg0)
    {
        if(arg0.m_selectedObject.gameObject.tag.Equals("String"))
        {
            stringGrabbed = true;
            currentArrow = Instantiate(arrow, mid.position, Quaternion.FromToRotation(mid.position, forceTarget.position), mid);
            rb = currentArrow.GetComponent<Rigidbody>();
            currentArrow.transform.LookAt(forceTarget, Vector3.up);
        }
    }
}
