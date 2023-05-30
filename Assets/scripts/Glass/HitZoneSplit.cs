using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class HitZoneSplit {

    public List<Transform>[] zones;
    public bool[] hitZones;
    protected AttachmentType attachmentType;

    public HitZoneSplit(AttachmentType attachmentType) {
        this.attachmentType = attachmentType;
    }

    public abstract void Split(Transform fragmentParent, Transform original);
    public abstract List<Transform> RegisterHit(Vector3 hitPoint, Transform transform);

    }
