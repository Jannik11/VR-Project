using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrokenGlass : MonoBehaviour {

    private HitZoneSplit hitZoneSplit;

    // Start is called before the first frame update
    public void Start() {
        Vector3 center = transform.position;

        hitZoneSplit = new Split2by2();
        hitZoneSplit.Split(center, transform);
    }
    
    public void Init(HitZoneType hitZoneType) {

        /*switch (hitZoneType) {
            case HitZoneType._2by2:

                hitZoneSplit = new Split2by2();

                break;
            case HitZoneType._1by3:
                break;
        }*/
    }


    public void RegisterHit(Vector3 hitPoint) {
        Vector3 center = transform.position;
        List<Transform> someList = hitZoneSplit.RegisterHit(hitPoint, transform);

        foreach (Transform fragment in someList) {

            Vector3 fragCenter = fragment.GetComponent<MeshRenderer>().bounds.center;

            fragment.GetComponent<MeshCollider>().enabled = true;

            Rigidbody rb = fragment.GetComponent<Rigidbody>();
            rb.isKinematic = false;
            rb.detectCollisions = true;

            rb.velocity = new Vector3(Random.Range(-2.0f, 2.0f), Random.Range(-2.0f, 2.0f), Random.Range(5.0f, 10.0f) * (1.0f - (Vector3.Distance(hitPoint, fragCenter) / 5.0f)) );
        }

    }
}