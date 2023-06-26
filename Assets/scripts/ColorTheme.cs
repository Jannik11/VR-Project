using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorTheme : MonoBehaviour
{

    [SerializeField] private GameObject[] emittingObjects;
    [SerializeField] private Material[] materials;

    private int materialIdx = 0;

    public void Start() {
        EventManager.current.OnLevelSwitch += ChangeColorTheme;
        EventManager.current.OnGameStart += ResetColorTheme;
    }

    private void ResetColorTheme() {
        materialIdx = 0;
    }

    private void ChangeColorTheme() {
        foreach(GameObject go in emittingObjects) {
            go.GetComponent<MeshRenderer>().material = materials[materialIdx];
        }
        materialIdx++;
    }
}
