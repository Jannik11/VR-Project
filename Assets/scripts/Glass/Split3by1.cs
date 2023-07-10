using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Split3by1 : HitZoneSplit {

    private float rightBorderX;
    private float leftBorderX;

    public Split3by1(AttachmentType attachmentType) : base(attachmentType) { }

    public override void Split(GameObject fragmentParent, GameObject original) {

        zones = new List<GameObject>[] {
            new List<GameObject>(),
            new List<GameObject>(),
            new List<GameObject>()};

        hitZones = new bool[] { false, false, false };

        MeshRenderer mr = original.GetComponent<MeshRenderer>();

        float sectionwidthX = (mr.bounds.max.x - mr.bounds.min.x) / 5.0f;

        rightBorderX = mr.bounds.max.x - sectionwidthX;
        leftBorderX = mr.bounds.min.x + sectionwidthX;

        Vector3 center = fragmentParent.transform.position;

        foreach (Transform fragmentChild in fragmentParent.transform) {

            GameObject fragment = fragmentChild.gameObject;

            Vector3 fragCenter = fragment.GetComponent<MeshRenderer>().bounds.center;

            if (fragCenter.x > rightBorderX) {
                zones[2].Add(fragment);
            } else if (fragCenter.x > leftBorderX) {
                zones[1].Add(fragment);
            } else {
                zones[0].Add(fragment);
            }
        }
    }

    public override List<GameObject> RegisterHit(Vector3 hitPoint, GameObject glass) {
        Vector3 center = glass.transform.position;
        List<GameObject> someList = new List<GameObject>();


        if (hitPoint.x > rightBorderX) {
            hitZones[2] = true;
            someList.AddRange(zones[2]);

        } else if (hitPoint.x > leftBorderX) {
            hitZones[1] = true;
            someList.AddRange(zones[1]);

        } else {
            hitZones[0] = true;
            someList.AddRange(zones[0]);
        }

        return someList;
    }
}

