using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class HitZoneSplit {

    public List<Transform>[] zones;
    public bool[] hitZones;

    public abstract void Split(Vector3 center, Transform transform);
    public abstract List<Transform> RegisterHit(Vector3 hitPoint, Transform transform);

    }
