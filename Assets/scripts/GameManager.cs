using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class GameManager : MonoBehaviour {

    [SerializeField] int startLives;
    [SerializeField] HighscoreHandler highscoreHandler;
    [SerializeField] TextMeshProUGUI score;
    [SerializeField] LivesGui livesGui;
    [SerializeField] LevelGenerator levelGenerator;

    private int currLives;

    private bool paused = false;
    private bool activeGame = false;
    // Start is called before the first frame update
    void Start() {
        EventManager.current.OnBowGrab += BowGrabbed;
        EventManager.current.OnGamePause += BowReleased;

        EventManager.current.OnPlayerDeath += EndGame;
        EventManager.current.OnPlayerHit += PlayerHit;
        currLives = startLives;
    }

    private void BowReleased() {
        if (activeGame) {
            paused = true;
            levelGenerator.PauseGame();
        }
    }

    void BowGrabbed(Side ignored) {
        highscoreHandler.ResumeTimer();
        livesGui.UpdateLives(startLives);
        livesGui.StartGame();

        Debug.Log("bin ich pausiert: " + paused);
        if (activeGame) {
            Debug.Log("resume game");
            levelGenerator.ResumeGame();
        } else {
            Debug.Log("neustart");
            activeGame = true;
            levelGenerator.StartGame();
        }
        paused = false;

    }
    void EndGame() {
        activeGame = false;

        float endScore = highscoreHandler.EndGame();
        score.text = endScore.ToString();
        livesGui.EndGame();
        levelGenerator.ResetLevel();
    }
    void PlayerHit() {
        currLives--;
        livesGui.UpdateLives(currLives);
        if (currLives <= 0) {
            EventManager.current.TriggerOnPlayerDeath();
        }
    }

    // Update is called once per frame
    void Update() {
        if(!paused && activeGame) { 
            score.text = highscoreHandler.GetScore().ToString();
        }
    }
}
