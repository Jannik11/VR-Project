using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorTheme : MonoBehaviour
{

    [SerializeField] private GameObject[] emittingObjects;
    [SerializeField] private Material[] materials;

    public void Start() {
        EventManager.current.OnLevelSwitch += ChangeColorTheme;
    }

    private void ChangeColorTheme(int lvlNr) {
        foreach(GameObject go in emittingObjects) {
            go.GetComponent<MeshRenderer>().material = materials[lvlNr];
        }
    }
}
