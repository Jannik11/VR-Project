using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighscoreHandler : MonoBehaviour {
    [SerializeField] private SimpleTimer timer;
    private List<HighscoreElement> _highscoreElements = new List<HighscoreElement>();
    [SerializeField] int maxHighscores = 7;
    [SerializeField] string filename;

    private void Start() {
    }

    public void ResumeTimer() {
        timer.ResumeTimer();
    }

    public void PauseTimer() {
        timer.PauseTimer();
    }

    public float EndGame() {
        Debug.Log("spiel wird beendet");
        float score = timer.EndTimer();
        string date = "";
        AddHighscoreIfPossible(new HighscoreElement(date, score));
        SaveHighscore();
        return score;
    }



    private void LoadHighscore() {
        _highscoreElements = FileHandler.ReadListFromJSON<HighscoreElement>(filename);
        while (_highscoreElements.Count > maxHighscores) {
            _highscoreElements.RemoveAt(maxHighscores);
        }
    }

    public float GetScore() {
        return timer.Timer;
    }

    private void SaveHighscore() {
        FileHandler.SaveToJSON<HighscoreElement>(_highscoreElements, filename);
    }

    public void AddHighscoreIfPossible(HighscoreElement element) {
        for (int i = 0; i < maxHighscores; i++) {
            if (i >= _highscoreElements.Count || element.score > _highscoreElements[i].score) {
                _highscoreElements.Insert(i, element);
                while (_highscoreElements.Count > maxHighscores) {
                    _highscoreElements.RemoveAt(maxHighscores);
                }
                SaveHighscore();
                break;
            }


        }
    }
}