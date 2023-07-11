using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class HitZoneSplit
{
    public List<GameObject>[] zones;
    public bool[] hitZones;
    protected AttachmentType attachmentType;

    public HitZoneSplit(AttachmentType attachmentType)
    {
        this.attachmentType = attachmentType;
    }

    /// <summary>
    /// Ordnet die Fragmente den HitZones zu
    /// </summary>
    /// <param name="fragmentParent">Gruppierungsobjekt der Fragmente</param>
    /// <param name="original">Gameobjekt der noch nicht zerstörten scheibe</param>
    public abstract void Split(GameObject fragmentParent, GameObject original);

    /// <summary>
    /// Sorgt dafür, dass je nach Trefferposition, bestimmte Bereiche der Scheibe herunterfallen
    /// </summary>
    /// <param name="hitPoint">Position, an der die Scheibe getroffen wurde</param>
    /// <param name="glass">Gruppierungsobjekt der Fragmente</param>
    /// <returns></returns>
    public abstract List<GameObject> RegisterHit(Vector3 hitPoint, GameObject glass);
}