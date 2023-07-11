using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Scriptable Object zur Darstellung eines Levels
/// </summary>
[CreateAssetMenu(fileName = "Level_", menuName = "ScriptableObject/Level")]
public class LevelSO : ScriptableObject {

    /// <summary>
    /// Transform der Ziele im Level
    /// </summary>
    public Transform[] targets;
    public Transform levelSeperator;

    /// <summary>
    /// Anzahl der Ziele im Level
    /// </summary>
    /// <returns></returns>
    public int Length() {
        return targets.Length;
    }

    /// <summary>
    /// Getter für ein zufälliges Ziel im Lvel
    /// </summary>
    /// <returns>Transform des ausgewählten Ziels</returns>
    public Transform GetTarget() {
        return targets[UnityEngine.Random.Range(0, targets.Length)];
    }


}
