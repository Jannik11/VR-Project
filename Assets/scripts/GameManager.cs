using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour {

    [SerializeField] int startLives;
    [SerializeField] HighscoreHandler highscoreHandler;
    [SerializeField] TextMeshProUGUI score;
    [SerializeField] LivesGui livesGui;

    private int currLives;

    private bool running = false;
    // Start is called before the first frame update
    void Start() {
        EventManager.current.OnGamePause += PauseGame;
        EventManager.current.OnGameStart += StartGame;
        EventManager.current.OnGameEnd += EndGame;
        EventManager.current.OnPlayerHit += PlayerHit;
        currLives = startLives;
    }

    void StartGame() {
        running = true;
        highscoreHandler.ResumeTimer();
        livesGui.UpdateLives(startLives);
    }

    void PauseGame() {
        running = false;
        highscoreHandler.PauseTimer();
    }
    void EndGame() {
        running = false;
        float endScore = highscoreHandler.EndGame();
        score.text = endScore.ToString();
        livesGui.EndGame();
    }
    void PlayerHit() {
        currLives--;
        livesGui.UpdateLives(currLives);
        if(currLives <= 0) {
            EventManager.current.TriggerOnGameEnd();
        }
    }

    // Update is called once per frame
    void Update() {
        if(running) { 
            score.text = highscoreHandler.GetScore().ToString();
        }
    }
}
