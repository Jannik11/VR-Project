using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Split2by3 : HitZoneSplit {

    private float topBorderY;
    private float botBorderY;

    public Split2by3(AttachmentType attachmentType) : base(attachmentType) { }

    public override void Split(Transform fragmentParent, Transform original) {

        zones = new List<Transform>[] {
            new List<Transform>(),
            new List<Transform>(),
            new List<Transform>(),
            new List<Transform>(),
            new List<Transform>(),
            new List<Transform>()};

        hitZones = new bool[] { false, false, false, false, false, false };

        MeshRenderer mr = original.GetComponent<MeshRenderer>();

        float sectionHeightY = (mr.bounds.max.y - mr.bounds.min.y) / 3.0f;

        topBorderY = mr.bounds.max.y - sectionHeightY;
        botBorderY = mr.bounds.min.y + sectionHeightY;

        Vector3 center = fragmentParent.position;

        foreach (Transform fragment in fragmentParent) {

            Vector3 fragCenter = fragment.GetComponent<MeshRenderer>().bounds.center;

            if (fragCenter.x < center.x) {
                if (fragCenter.y > topBorderY) {
                    zones[0].Add(fragment);
                }
                else if (fragCenter.y > botBorderY) {
                    zones[2].Add(fragment);
                }
                else {
                    zones[4].Add(fragment);
                }
            }
            else {
                if (fragCenter.y > topBorderY) {
                    zones[1].Add(fragment);
                }
                else if (fragCenter.y > botBorderY) {
                    zones[3].Add(fragment);
                }
                else {
                    zones[5].Add(fragment);
                }
            }

        }
    }

    public override List<Transform> RegisterHit(Vector3 hitPoint, Transform transform) {
        Vector3 center = transform.position;
        List<Transform> someList = new List<Transform>();

        if (hitPoint.x < center.x) {

            if (hitPoint.y > topBorderY) {
                hitZones[0] = true;
                someList.AddRange(zones[0]);

                if (hitZones[1] || hitZones[3]) {
                    someList.AddRange(zones[2]);
                    someList.AddRange(zones[3]);
                    someList.AddRange(zones[4]);
                    someList.AddRange(zones[5]);
                }

            }
            else if (hitPoint.y > botBorderY) {
                hitZones[2] = true;
                someList.AddRange(zones[2]);

                if (hitZones[3] || hitZones[1]) {
                    someList.AddRange(zones[3]);
                    someList.AddRange(zones[4]);
                    someList.AddRange(zones[5]);
                }
            }
            else {
                hitZones[4] = true;
                someList.AddRange(zones[4]);
            }
        }
        else {

            if (hitPoint.y > topBorderY) {
                hitZones[1] = true;
                someList.AddRange(zones[1]);

                if (hitZones[0] || hitZones[2]) {
                    someList.AddRange(zones[2]);
                    someList.AddRange(zones[3]);
                    someList.AddRange(zones[4]);
                    someList.AddRange(zones[5]);
                }

            }
            else if (hitPoint.y > botBorderY) {
                hitZones[3] = true;
                someList.AddRange(zones[3]);

                if (hitZones[2] || hitZones[0]) {
                    someList.AddRange(zones[2]);
                    someList.AddRange(zones[4]);
                    someList.AddRange(zones[5]);
                }
            }
            else {
                hitZones[5] = true;
                someList.AddRange(zones[5]);
            }
        }

        return someList;
    }
}

