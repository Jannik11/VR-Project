using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Verwaltet die Lebensanzeige
/// </summary>
public class LivesGui : MonoBehaviour {
    /// <summary>
    /// Liste der roten Herzen
    /// </summary>
    [SerializeField] GameObject[] goodLives;

    /// <summary>
    /// Liste der schwarzen Herzen
    /// </summary>
    [SerializeField] GameObject[] badLives;
    
    /// <summary>
    /// Updated die Lebensanzeige mit den Herzen
    /// </summary>
    /// <param name="lives">Anzahl der Leben des Spielers</param>
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

    /// <summary>
    /// Initialisiert die Lebensanzeige zu Beginn des Spiels
    /// </summary>
    public void StartGame() {
        for (int i = 0; i < goodLives.Length; i++) {
            goodLives[i].SetActive(true);
            badLives[i].SetActive(false);
        }
    }
}
