using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LivesGui : MonoBehaviour {
    [SerializeField] GameObject[] goodLives;
    [SerializeField] GameObject[] badLives;
    // Start is called before the first frame update
    public void UpdateLives(int lives) {
        for (int i = 0; i < goodLives.Length; i++) {
            if (i < lives) {
                goodLives[i].SetActive(true);
                badLives[i].SetActive(false);
            }
            else {
                goodLives[i].SetActive(false);
                badLives[i].SetActive(true);
            }
        }
    }

    public void StartGame() {
        for (int i = 0; i < goodLives.Length; i++) {
            goodLives[i].SetActive(true);
            badLives[i].SetActive(false);
        }
    }
}
