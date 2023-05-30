using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Split1by3 : HitZoneSplit {

    private float topBorderY;
    private float botBorderY;

    public Split1by3(AttachmentType attachmentType) : base(attachmentType) { }

    public override void Split(Transform fragmentParent, Transform original) {

        zones = new List<Transform>[] {
            new List<Transform>(),
            new List<Transform>(),
            new List<Transform>()};

        hitZones = new bool[] { false, false, false };

        MeshRenderer mr = original.GetComponent<MeshRenderer>();

        float sectionHeightY = (mr.bounds.max.y - mr.bounds.min.y) / 3.0f;

        topBorderY = mr.bounds.max.y - sectionHeightY;
        botBorderY = mr.bounds.min.y + sectionHeightY;

        foreach (Transform fragment in fragmentParent) {

            Vector3 fragCenter = fragment.GetComponent<MeshRenderer>().bounds.center;

            if (fragCenter.y > topBorderY) {
                zones[0].Add(fragment);
            }
            else if (fragCenter.y > botBorderY) {
                zones[1].Add(fragment);
            }
            else {
                zones[2].Add(fragment);
            }
        }
    }

    public override List<Transform> RegisterHit(Vector3 hitPoint, Transform transform) {
        Vector3 center = transform.position;
        List<Transform> someList = new List<Transform>();

        switch (attachmentType) {
            case AttachmentType.TOP:
                if (hitPoint.y > topBorderY) {
                    hitZones[0] = true;
                    someList.AddRange(zones[0]);
                    someList.AddRange(zones[1]);
                    someList.AddRange(zones[2]);
                }
                else if (hitPoint.y > botBorderY) {
                    hitZones[1] = true;
                    someList.AddRange(zones[1]);
                    someList.AddRange(zones[2]);
                }
                else {
                    hitZones[2] = true;
                    someList.AddRange(zones[2]);
                }
                break;

            case AttachmentType.BOTTOM:
                if (hitPoint.y > topBorderY) {
                    hitZones[0] = true;
                    someList.AddRange(zones[0]);
                }
                else if (hitPoint.y > botBorderY) {
                    hitZones[1] = true;
                    someList.AddRange(zones[1]);
                    someList.AddRange(zones[0]);
                }
                else {
                    hitZones[2] = true;
                    someList.AddRange(zones[2]);
                    someList.AddRange(zones[1]);
                    someList.AddRange(zones[0]);
                }
                break;

            default:
                break;
        }




        return someList;
    }
}
