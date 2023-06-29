using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Split1by3 : HitZoneSplit {

    private float fstBorder;
    private float sndBorder;

    public Split1by3(AttachmentType attachmentType) : base(attachmentType) { }

    public override void Split(GameObject fragmentParent, GameObject original) {

        zones = new List<GameObject>[] {
            new List<GameObject>(),
            new List<GameObject>(),
            new List<GameObject>()};

        MeshRenderer mr = original.GetComponent<MeshRenderer>();

        switch(attachmentType) {
            case AttachmentType.TOP:
            case AttachmentType.BOTTOM:
            case AttachmentType.VERTICAL:
                float sectionHeightY = (mr.bounds.max.y - mr.bounds.min.y) / 5.0f;

                fstBorder = mr.bounds.max.y - sectionHeightY;
                sndBorder = mr.bounds.min.y + sectionHeightY;

                foreach (Transform fragmentChild in fragmentParent.transform) {

                    GameObject fragment = fragmentChild.gameObject;

                    Vector3 fragCenter = fragment.GetComponent<MeshRenderer>().bounds.center;

                    if (fragCenter.y > fstBorder) {
                        zones[0].Add(fragment);
                    }
                    else if (fragCenter.y > sndBorder) {
                        zones[1].Add(fragment);
                    }
                    else {
                        zones[2].Add(fragment);
                    }
                }
                break;

            case AttachmentType.LEFT:
            case AttachmentType.RIGHT:
            case AttachmentType.HORIZONTAL:
                float sectionHeightX = (mr.bounds.max.x - mr.bounds.min.x) / 5.0f;

                fstBorder = mr.bounds.max.x - sectionHeightX;
                sndBorder = mr.bounds.min.x + sectionHeightX;

                foreach (Transform fragmentChild in fragmentParent.transform) {

                    GameObject fragment = fragmentChild.gameObject;

                    Vector3 fragCenter = fragment.GetComponent<MeshRenderer>().bounds.center;

                    if (fragCenter.x > fstBorder) {
                        zones[2].Add(fragment);
                    }
                    else if (fragCenter.x > sndBorder) {
                        zones[1].Add(fragment);
                    }
                    else {
                        zones[0].Add(fragment);
                    }
                }
                break;
            default:
                break;
        }
    }

    public override List<GameObject> RegisterHit(Vector3 hitPoint, GameObject glass) {
        Vector3 center = glass.transform.position;
        List<GameObject> someList = new List<GameObject>();

        switch (attachmentType) {
            case AttachmentType.TOP:
                if (hitPoint.y > fstBorder) {
                    someList.AddRange(zones[0]);
                    someList.AddRange(zones[1]);
                    someList.AddRange(zones[2]);
                }
                else if (hitPoint.y > sndBorder) {
                    someList.AddRange(zones[1]);
                    someList.AddRange(zones[2]);
                }
                else {
                    someList.AddRange(zones[2]);
                }
                break;

            case AttachmentType.BOTTOM:
                if (hitPoint.y > fstBorder) {
                    someList.AddRange(zones[0]);
                }
                else if (hitPoint.y > sndBorder) {
                    someList.AddRange(zones[1]);
                    someList.AddRange(zones[0]);
                }
                else {
                    someList.AddRange(zones[2]);
                    someList.AddRange(zones[1]);
                    someList.AddRange(zones[0]);
                }
                break;

            case AttachmentType.VERTICAL:
                if (hitPoint.y > fstBorder) {
                    someList.AddRange(zones[0]);
                    someList.AddRange(zones[1]);
                }
                else if (hitPoint.y > sndBorder) {
                    someList.AddRange(zones[1]);
                }
                else {
                    someList.AddRange(zones[2]);
                    someList.AddRange(zones[1]);
                }
                break;

            case AttachmentType.LEFT:
                if (hitPoint.x > fstBorder) {
                    someList.AddRange(zones[2]);
                }
                else if (hitPoint.x > sndBorder) {
                    someList.AddRange(zones[1]);
                    someList.AddRange(zones[2]);
                }
                else {
                    someList.AddRange(zones[0]);
                    someList.AddRange(zones[1]);
                    someList.AddRange(zones[2]);
                }
                break;

            case AttachmentType.RIGHT:
                if (hitPoint.x > fstBorder) {
                    someList.AddRange(zones[2]);
                    someList.AddRange(zones[1]);
                    someList.AddRange(zones[0]);
                }
                else if (hitPoint.x > sndBorder) {
                    someList.AddRange(zones[1]);
                    someList.AddRange(zones[0]);
                }
                else {
                    someList.AddRange(zones[0]);
                }
                break;

            case AttachmentType.HORIZONTAL:
                if (hitPoint.x > fstBorder) {
                    someList.AddRange(zones[2]);
                    someList.AddRange(zones[1]);
                }
                else if (hitPoint.x > sndBorder) {
                    someList.AddRange(zones[1]);
                }
                else {
                    someList.AddRange(zones[0]);
                    someList.AddRange(zones[1]);
                }
                break;

            default:
                break;
        }

        return someList;
    }
}
