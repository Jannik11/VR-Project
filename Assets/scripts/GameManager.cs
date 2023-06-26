using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class GameManager : MonoBehaviour {

    [SerializeField] int startLives;
    [SerializeField] HighscoreHandler highscoreHandler;
    [SerializeField] TextMeshProUGUI score;
    [SerializeField] LivesGui2 livesGui;
    [SerializeField] LevelGenerator levelGenerator;
    [SerializeField] float immuneTime;

    private int currLives;

    public bool Paused { private set; get; } = false;
    private bool activeGame = false;

    private float currImmuneTime = 0.0f;

    // Start is called before the first frame update
    void Start() {
        EventManager.current.OnBowGrab += BowGrabbed;
        EventManager.current.OnBowRelease += BowReleased;

        EventManager.current.OnPlayerDeath += EndGame;
        EventManager.current.OnPlayerHit += PlayerHit;
        EventManager.current.OnPlayerGetLifepoint += PlayerGetLifepoint;
        currLives = startLives;

        Debug.Log("CURRLIVES: " + currLives);
    }

    private void BowReleased() {
        if (activeGame) {
            Paused = true;
            levelGenerator.PauseGame();
        }
    }

    void BowGrabbed(Side ignored) {
        highscoreHandler.ResumeTimer();
        livesGui.UpdateLives(currLives);
        livesGui.StartGame();

        Debug.Log("bin ich pausiert: " + Paused);
        if (activeGame) {
            Debug.Log("resume game");
            levelGenerator.ResumeGame();
        } else {
            Debug.Log("neustart");
            activeGame = true;
            levelGenerator.StartGame();
        }
        Paused = false;

    }
    void EndGame() {
        activeGame = false;

        float endScore = highscoreHandler.EndGame();
        score.text = endScore.ToString();
        //livesGui.EndGame();
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

    private void PlayerGetLifepoint()
    {
        if(currLives < startLives)
        {
            currLives++;
        }
    }

    // Update is called once per frame
    void Update() {
        if(!Paused && activeGame) { 
            score.text = highscoreHandler.GetScore().ToString();
        }

        currImmuneTime -= Time.deltaTime;
    }
}
