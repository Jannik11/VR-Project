using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LivesGui : MonoBehaviour {
    [SerializeField] TextMeshProUGUI livesText;
    [SerializeField] GameObject gameOverGrafik;
    public void UpdateLives(int lives) {
        livesText.text = "Du hast noch " + lives + "Leben";
    }

    public void EndGame() {
        livesText.text = "Du hast verloren.\n nehme den Bogen erneut auf um neu zu starten.";
        gameOverGrafik.SetActive(true);
    }

    public void StartGame() {
        gameOverGrafik.SetActive(false);
    }

    //Hier könnte nun auch das Armband benutzt werden.
}
