using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighscoreHandler : MonoBehaviour {
    [SerializeField] private SimpleTimer timer;
    private List<HighscoreElement> highscoreElements = new List<HighscoreElement>();
    [SerializeField] int maxHighscores = 5;
    [SerializeField] string filename;
    [SerializeField] HighscoreUi highscoreUi;

    private void Start() {
        LoadHighscore();
        highscoreUi.UpdateUI(highscoreElements);
    }

    public void ResumeTimer() {
        timer.ResumeTimer();
    }

    public void PauseTimer() {
        timer.PauseTimer();
    }

    public float EndGame() {
        float score = timer.EndTimer();
        string date = System.DateTime.Now.ToString();
        bool newScore = AddHighscoreIfPossible(new HighscoreElement(date, score));
        highscoreUi.UpdateUI(highscoreElements);
        return score;
    }

    private void LoadHighscore() {
        highscoreElements = FileHandler.ReadListFromJSON<HighscoreElement>(filename);
        while (highscoreElements.Count > maxHighscores) {
            highscoreElements.RemoveAt(maxHighscores);
        }
    }

    public float GetScore() {
        return timer.Timer;
    }

    private void SaveHighscore() {
        FileHandler.SaveToJSON<HighscoreElement>(highscoreElements, filename);
    }

    public bool AddHighscoreIfPossible(HighscoreElement element) {
        Boolean added = false;
        Debug.Log("AddHighscoreIfPossible: ");
        for (int i = 0; i < maxHighscores; i++) {
            if (i >= highscoreElements.Count || element.score > highscoreElements[i].score) {
                added = true;
                highscoreElements.Insert(i, element);
                while (highscoreElements.Count > maxHighscores) {
                    highscoreElements.RemoveAt(maxHighscores);
                }
                Debug.Log("AddHighscoreIfPossible: jetzt speicher ich");
                foreach(HighscoreElement e in highscoreElements) {
                    Debug.Log("ich speicher jetzt element" + e.date + "Score" + e.score);
                }
                SaveHighscore();
                break;
            }
        }
        return added;
    }
}