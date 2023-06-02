using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Level_", menuName = "ScriptableObject/Level")]
public class LevelSO : ScriptableObject
{
    [SerializeField] Transform[] level;
    [SerializeField] Transform levelSeperator;

    public int Length() {
        return level.Length;
    }

    public Transform GetTarget() {
        return level[UnityEngine.Random.Range(0, level.Length)];
    }

    public Transform GetSeperator() {
        return levelSeperator;
    }
   
}
