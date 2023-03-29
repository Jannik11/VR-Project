using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{

    Rigidbody rb;
    [SerializeField] Transform stickingArrow;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(transform.position + rb.velocity, Vector3.up);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.tag.Equals("String"))
        {
            return;
        }
        Debug.Log(collision.collider);
        Transform stickingArrowInstance = Instantiate(stickingArrow);
        stickingArrowInstance.forward = transform.forward;
        stickingArrowInstance.position = transform.position;
        stickingArrowInstance.localScale = transform.localScale;

        if (collision.collider.attachedRigidbody != null)
        {
            stickingArrowInstance.transform.SetParent(collision.collider.attachedRigidbody.transform, true);
        }
        Destroy(gameObject);
    }
}
