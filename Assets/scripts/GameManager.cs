using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour {

    [SerializeField] int lives = 3;
    [SerializeField] HighscoreHandler highscoreHandler;
    [SerializeField] TextMeshProUGUI score;
    [SerializeField] GameObject PauseScreen;
    [SerializeField] TextMeshProUGUI livesText;

    private bool running = false;
    // Start is called before the first frame update
    void Start() {
        EventManager.current.OnGamePause += PauseGame;
        EventManager.current.OnGameStart += StartGame;
        EventManager.current.OnGameEnd += EndGame;
        EventManager.current.OnPlayerHit += PlayerHit;
    }

    void StartGame() {
        running = true;
        highscoreHandler.ResumeTimer();
        PauseScreen.SetActive(false);

        livesText.text = "Leben: " + lives;
    }

    void PauseGame() {
        running = false;
        highscoreHandler.PauseTimer();
        PauseScreen.SetActive(true);
    }
    void EndGame() {
        Debug.Log("WHYYYYYY!");
        lives = 3;
        float endScore = highscoreHandler.EndGame();
        score.text = endScore.ToString();
        highscoreHandler.EndGame();
        PauseScreen.SetActive(true);

        livesText.text = "GAME OVER!!!\nRelease the bow to see your results.";
    }
    void PlayerHit() {
        lives--;
        livesText.text = "Leben: " + lives.ToString();
        Debug.Log("Leben übrig: " + lives + " " + (lives <= 0));
        if(lives <= 0) {
            Debug.Log("Trigger event");

            EventManager.current.TriggerOnGameEnd();
        }
    }

    // Update is called once per frame
    void Update() {
        score.text = highscoreHandler.GetScore().ToString();
    }
}
