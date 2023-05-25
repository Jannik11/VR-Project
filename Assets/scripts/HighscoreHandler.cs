using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighscoreHandler : MonoBehaviour
{
    [SerializeField] private SimpleTimer timer;
    private List<HighscoreElement> _highscoreElements = new List<HighscoreElement>();
    [SerializeField] int maxHighscores = 7;
    [SerializeField] string filename;

    private void Start()
    {
        EventManager.current.OnGameStart += StartTimer;
        EventManager.current.OnGameEnd += EndTimer;
    }

    public void StartTimer()
    {
        timer.StartTimer();
    }

    public void EndTimer()
    {
        float score = timer.EndTimer();
        string date = "";
        AddHighscoreIfPossible(new HighscoreElement(date, score));
        SaveHighscore();
    }

    private void LoadHighscore()
    {
        _highscoreElements = FileHandler.ReadListFromJSON<HighscoreElement>(filename);
        while (_highscoreElements.Count > maxHighscores)
        {
            _highscoreElements.RemoveAt(maxHighscores);
        }
    }

    private void SaveHighscore()
    {
        FileHandler.SaveToJSON<HighscoreElement>(_highscoreElements, filename);
    }

    public void AddHighscoreIfPossible(HighscoreElement element)
    {
        for (int i = 0; i < maxHighscores; i++)
        {
            if (i > _highscoreElements.Count || element.score > _highscoreElements[i].score)
            {
                _highscoreElements.Insert(i,  element);
                while (_highscoreElements.Count > maxHighscores)
                {
                    _highscoreElements.RemoveAt(maxHighscores);
                }
                SaveHighscore();
                break;
            }

            
        }
    }
}