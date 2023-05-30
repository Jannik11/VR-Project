using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Split3by2 : HitZoneSplit {

    private float rightBorderX;
    private float leftBorderX;

    public Split3by2(AttachmentType attachmentType) : base(attachmentType) { }

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

        float sectionHeightX = (mr.bounds.max.x - mr.bounds.min.x) / 3.0f;

        rightBorderX = mr.bounds.max.x - sectionHeightX;
        leftBorderX = mr.bounds.min.x + sectionHeightX;

        Vector3 center = fragmentParent.position;

        foreach (Transform fragment in fragmentParent) {

            Vector3 fragCenter = fragment.GetComponent<MeshRenderer>().bounds.center;

            if(fragCenter.y > center.y) {
                if (fragCenter.x > rightBorderX) {
                    zones[2].Add(fragment);
                }
                else if (fragCenter.x > leftBorderX) {
                    zones[1].Add(fragment);
                }
                else {
                    zones[0].Add(fragment);
                }
            } else {
                if (fragCenter.x > rightBorderX) {
                    zones[5].Add(fragment);
                }
                else if (fragCenter.x > leftBorderX) {
                    zones[4].Add(fragment);
                }
                else {
                    zones[3].Add(fragment);
                }
            }

        }
    }

    public override List<Transform> RegisterHit(Vector3 hitPoint, Transform transform) {
        Vector3 center = transform.position;
        List<Transform> someList = new List<Transform>();

        if (hitPoint.y > center.y) {

            if (hitPoint.x > rightBorderX) {
                hitZones[2] = true;
                someList.AddRange(zones[2]);

                if(hitZones[0]) {
                    someList.AddRange(zones[1]);
                    someList.AddRange(zones[4]);
                }

            }
            else if (hitPoint.x > leftBorderX) {
                hitZones[1] = true;
                someList.AddRange(zones[1]);
                someList.AddRange(zones[4]);
            }
            else {
                hitZones[0] = true;
                someList.AddRange(zones[0]);

                if (hitZones[2]) {
                    someList.AddRange(zones[1]);
                    someList.AddRange(zones[4]);
                }
            }
        }
        else {

            if (hitPoint.x > rightBorderX) {
                hitZones[5] = true;
                someList.AddRange(zones[5]);

                if (hitZones[3]) {
                    someList.AddRange(zones[1]);
                    someList.AddRange(zones[4]);
                }

            }
            else if (hitPoint.x > leftBorderX) {
                hitZones[4] = true;
                someList.AddRange(zones[4]);
                someList.AddRange(zones[1]);
            }
            else {
                hitZones[3] = true;
                someList.AddRange(zones[3]);

                if (hitZones[5]) {
                    someList.AddRange(zones[1]);
                    someList.AddRange(zones[4]);
                }
            }
        }

        return someList;
    }
}

