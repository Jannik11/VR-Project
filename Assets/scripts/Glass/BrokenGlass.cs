using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrokenGlass : MonoBehaviour {

    private HitZoneSplit hitZoneSplit;
    private GameObject originalGlass;

    // Start is called before the first frame update
    public void Start() {
    }

    public void Init(HitZoneType hitZoneType, AttachmentType attachmentType, GameObject originalGlass, Vector3 hitPoint) {

        this.originalGlass = originalGlass;

        switch (hitZoneType) {
            case HitZoneType._2by2:

                hitZoneSplit = new Split2by2(attachmentType);

                break;
            case HitZoneType._1by3:

                hitZoneSplit = new Split1by3(attachmentType);
                break;
            case HitZoneType._3by2:

                hitZoneSplit = new Split3by2(attachmentType);
                break;
            case HitZoneType._3by1:

                hitZoneSplit = new Split3by1(attachmentType);
                break;
        }

        hitZoneSplit.Split(gameObject, originalGlass);
        RegisterHit(hitPoint);
    }


    public void RegisterHit(Vector3 hitPoint) {
        List<GameObject> hittenFragments = hitZoneSplit.RegisterHit(hitPoint, gameObject);

        foreach (GameObject fragment in hittenFragments) {

            if(!fragment.GetComponent<Fragment>().AlreadyHit) {
                Vector3 fragCenter = fragment.GetComponent<MeshRenderer>().bounds.center;

                fragment.GetComponent<MeshCollider>().enabled = true;

                //Die losen Fragmente werden nicht an das Movement-Skript geparented
                //TODO Fragmente unterordnen

                fragment.transform.parent = fragment.transform.parent.parent.parent;

                fragment.gameObject.layer = LayerMask.NameToLayer("Fragment");
                fragment.tag = "Fragment";

                Rigidbody rb = fragment.GetComponent<Rigidbody>();
                rb.isKinematic = false;
                rb.detectCollisions = true;

                rb.AddForce(new Vector3(Random.Range(-2.0f, 2.0f), Random.Range(-2.0f, 2.0f), Random.Range(5.0f, 10.0f) * (1.0f - (Vector3.Distance(hitPoint, fragCenter) / 5.0f))));

                fragment.GetComponent<Fragment>().AlreadyHit = true;

                //Destroy(gameObject, 30.0f);
            }


        }

    }
}