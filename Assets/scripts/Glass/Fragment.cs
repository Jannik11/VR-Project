using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fragment : MonoBehaviour {

    public bool AlreadyHit { get; set; }
    private void OnCollisionEnter(Collision collision) {

        if (collision.collider.CompareTag("Arrow")) {
            Transform parent = transform.parent;
            Debug.Log("Parent von Fragment " + parent);
            BrokenGlass bg = parent.GetComponent<BrokenGlass>();

            if (bg != null) {
                Debug.Log("Broken glass: " + bg);
                Vector3 hitPoint = collision.GetContact(0).point;
                bg.RegisterHit(hitPoint);
            }


        }
    }
}
