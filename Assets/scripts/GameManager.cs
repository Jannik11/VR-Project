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
    [SerializeField] float immuneTime;

    private int currLives;

    private bool paused = false;
    private bool activeGame = false;

    private float currImmuneTime = 0.0f;

    // Start is called before the first frame update
    void Start() {
        EventManager.current.OnBowGrab += BowGrabbed;
        EventManager.current.OnBowRelease += BowReleased;

        EventManager.current.OnPlayerDeath += EndGame;
        EventManager.current.OnPlayerHit += PlayerHit;
        currLives = startLives;

        Debug.Log("CURRLIVES: " + currLives);
    }

    private void BowReleased() {
        if (activeGame) {
            paused = true;
            levelGenerator.PauseGame();
        }
    }

    void BowGrabbed(Side ignored) {
        highscoreHandler.ResumeTimer();
        livesGui.UpdateLives(currLives);
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
        currLives = startLives;
    }
    void PlayerHit() {
        if(currImmuneTime <= 0.0f) {
            currLives--;
            currImmuneTime = immuneTime;
            livesGui.UpdateLives(currLives);
            if (currLives <= 0) {
                EventManager.current.TriggerOnPlayerDeath();
            }
        }
    }

    // Update is called once per frame
    void Update() {
        if(!paused && activeGame) { 
            score.text = highscoreHandler.GetScore().ToString();
        }

        currImmuneTime -= Time.deltaTime;
    }
}
