using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class HitZoneSplit {

    public List<GameObject>[] zones;
    public bool[] hitZones;
    protected AttachmentType attachmentType;

    public HitZoneSplit(AttachmentType attachmentType) {
        this.attachmentType = attachmentType;
    }

    public abstract void Split(GameObject fragmentParent, GameObject original);
    public abstract List<GameObject> RegisterHit(Vector3 hitPoint, GameObject glass);

    }
