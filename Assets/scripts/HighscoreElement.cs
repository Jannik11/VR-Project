using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Datenhaltungsobjekt, dieses Objekt wird als JSON gespeichtert. 
/// </summary>
[Serializable]
public class HighscoreElement {
    public string date;
    public float score;

    public HighscoreElement(string date, float score)
    {
        this.date = date;
        this.score = score;
    }
}
