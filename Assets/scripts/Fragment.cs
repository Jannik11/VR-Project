using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fragment : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision) {

        if(collision.collider.CompareTag("Arrow")){

            Transform parent = transform.parent;
            BrokenGlass bg = parent.GetComponent<BrokenGlass>();

            Vector3 parentCenter = parent.transform.position;
            Vector3 hitPoint = collision.GetContact(0).point;

            bg.RegisterHit(hitPoint);

        }


    }
}
