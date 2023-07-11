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
    [SerializeField] GameObject startInstruction;
    [SerializeField] GameObject gameOverBanner;
    [SerializeField] TextMeshProUGUI currentTime;
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

    /// <summary>
    /// Pausiert das Spiel (Timer und den Levelgenerator) wenn n�tig.
    /// </summary>
    private void BowReleased() {
        startInstruction.SetActive(true);
        if (activeGame) {
            highscoreHandler.PauseTimer();
            Paused = true;
            levelGenerator.PauseGame();
        }
    }

    /// <summary>
    /// startet ein neues Spiel oder resumed es.
    /// </summary>
    void BowGrabbed(Side ignored) {
        startInstruction.SetActive(false);
        highscoreHandler.ResumeTimer();
        Debug.Log("bin ich pausiert: " + Paused);
        if (activeGame) {
            Debug.Log("resume game");
            levelGenerator.ResumeGame();
        } else {
            Debug.Log("neustart");
            gameOverBanner.SetActive(false);
            activeGame = true;
            levelGenerator.StartGame();
            livesGui.StartGame();
        }
        Paused = false;

    }
    /// <summary>
    /// beendet ein Spiel wenn der Spieler verloren hat.
    /// </summary>
    void EndGame() {
        activeGame = false;

        float endScore = highscoreHandler.EndGame();
        score.text = endScore.ToString();
        levelGenerator.ResetLevel();
        currLives = startLives;
        gameOverBanner.SetActive(true);
    }

    /// <summary>
    ///  zieht leben ab und checkt ob der Spieler verloren hat.
    /// </summary>
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

    /// <summary>
    /// gibt dem Spieler ein Leben zur�ck, wenn ein entsprechendes Ziel getroffen wurde.
    /// </summary>
    private void PlayerGetLifepoint()
    {
        if(currLives < startLives)
        {
            currLives++;
            livesGui.UpdateLives(currLives);
        }
    }

    // Update is called once per frame
    void Update() {
        if(!Paused && activeGame) { 
            currentTime.text = Math.Round(highscoreHandler.GetScore(), 2).ToString();
        }

        currImmuneTime -= Time.deltaTime;
    }
}
