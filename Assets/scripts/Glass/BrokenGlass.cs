using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrokenGlass : MonoBehaviour
{
    private HitZoneSplit hitZoneSplit;

    /// <summary>
    /// Wählt die entspechende Klasse, je nach HitZoneType aus
    /// </summary>
    /// <param name="hitZoneType">Bestimmt den HitZoneType</param>
    /// <param name="attachmentType">Bestimmt den AttachmentType</param>
    /// <param name="originalGlass">Gameobjekt der noch nicht zerstörten scheibe</param>
    /// <param name="hitPoint">Position, an der die Scheibe getroffen wurde</param>
    public void Init(HitZoneType hitZoneType, AttachmentType attachmentType, GameObject originalGlass, Vector3 hitPoint)
    {
        switch (hitZoneType)
        {
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

    /// <summary>
    /// Führt alle Schritte aus, die zum Zerbrechen der Scheibe führen
    /// </summary>
    /// <param name="hitPoint">Position, an der die Scheibe getroffen wurde</param>
    public void RegisterHit(Vector3 hitPoint)
    {
        List<GameObject> hittenFragments = hitZoneSplit.RegisterHit(hitPoint, gameObject);

        foreach (GameObject fragment in hittenFragments)
        {
            if (!fragment.GetComponent<Fragment>().AlreadyHit)
            {
                Vector3 fragCenter = fragment.GetComponent<MeshRenderer>().bounds.center;

                fragment.GetComponent<MeshCollider>().enabled = true;

                fragment.transform.parent = fragment.transform.parent.parent.parent;

                fragment.gameObject.layer = LayerMask.NameToLayer("Fragment");
                fragment.tag = "Fragment";

                Rigidbody rb = fragment.GetComponent<Rigidbody>();
                rb.isKinematic = false;
                rb.detectCollisions = true;

                rb.AddForce(new Vector3(Random.Range(-2.0f, 2.0f), Random.Range(-2.0f, 2.0f), Random.Range(5.0f, 10.0f) * (1.0f - (Vector3.Distance(hitPoint, fragCenter) / 5.0f))));

                fragment.GetComponent<Fragment>().AlreadyHit = true;
            }
        }
    }
}