using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour {

    [SerializeField] int lives = 3;
    [SerializeField] HighscoreHandler highscoreHandler;
    [SerializeField] TextMeshProUGUI score;
    private bool running = false;
    // Start is called before the first frame update
    void Start() {
        EventManager.current.OnGamePause += PauseGame;
        EventManager.current.OnGameStart += StartGame;
        EventManager.current.OnGameEnd += EndGame;
    }

    void StartGame() {
        running = true;
        highscoreHandler.ResumeTimer();
    }

    void PauseGame() {
        running = false;
        highscoreHandler.PauseTimer();
    }
    void EndGame() {
        float score = highscoreHandler.EndGame();
    }

    // Update is called once per frame
    void Update() {
        score.text = highscoreHandler.GetScore().ToString();
    }
}
