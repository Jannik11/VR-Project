using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Split2by2 : HitZoneSplit {

    public Split2by2(AttachmentType attachmentType) : base(attachmentType) { }

    public override void Split(Transform fragmentParent, Transform original) {

        zones = new List<Transform>[] {
            new List<Transform>(),
            new List<Transform>(),
            new List<Transform>(),
            new List<Transform>()};

        hitZones = new bool[] { false, false, false, false };

        Vector3 center = fragmentParent.position;

        foreach (Transform fragment in fragmentParent) {

            Vector3 fragCenter = fragment.GetComponent<MeshRenderer>().bounds.center;

            if (fragCenter.x > center.x) {
                if (fragCenter.y > center.y) {
                    zones[1].Add(fragment);
                }
                else {
                    zones[3].Add(fragment);
                }
            }
            else {

                if (fragCenter.y > center.y) {
                    zones[0].Add(fragment);
                }
                else {
                    zones[2].Add(fragment);
                }
            }
        }
    }

    public override List<Transform> RegisterHit(Vector3 hitPoint, Transform transform) {
        Vector3 center = transform.position;
        List<Transform> someList = new List<Transform>();


        if (hitPoint.x > center.x) {
            if (hitPoint.y > center.y) {
                hitZones[1] = true;
                someList.AddRange(zones[1]);

                if (hitZones[0]) {
                    someList.AddRange(zones[2]);
                    someList.AddRange(zones[3]);
                }
                else if (hitZones[2]) {
                    someList.AddRange(zones[3]);
                }

            }
            else {
                hitZones[3] = true;
                someList.AddRange(zones[3]);

                if (hitZones[0]) {
                    someList.AddRange(zones[2]);
                }

            }
        }
        else {

            if (hitPoint.y > center.y) {
                hitZones[0] = true;
                someList.AddRange(zones[0]);

                if (hitZones[1]) {
                    someList.AddRange(zones[2]);
                    someList.AddRange(zones[3]);
                }
                else if (hitZones[3]) {
                    someList.AddRange(zones[2]);
                }

            }
            else {
                hitZones[2] = true;
                someList.AddRange(zones[2]);

                if (hitZones[1]) {
                    someList.AddRange(zones[3]);
                }
            }
        }

        return someList;
    }
}
