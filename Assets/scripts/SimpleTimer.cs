using UnityEngine;

public class SimpleTimer : MonoBehaviour {
    public float Timer{ get; private set;} = 0.0f ;

    private bool running = false;

    /// <summary>
    /// Zählt den Timer hoch, wenn das Spiel nicht pausiert ist
    /// </summary>
    void Update()
    {
        if (running)
        {
            Timer += Time.deltaTime;
        }
    }

    /// <summary>
    /// Startet den Timer nachdem das Spiel nach einer Pause
    /// fortgesetzt wird
    /// </summary>
    public void ResumeTimer()
    {
        running = true;
    }

    /// <summary>
    /// Pausiert den Timer
    /// </summary>
    public void PauseTimer()
    {
        running = false;
    }

    /// <summary>
    /// Beendet den Timer
    /// </summary>
    /// <returns>Gibt die erreichte Zeit zurück</returns>
    public float EndTimer()
    {
        running = false;
        float toReturn = Timer;
        Timer = 0.0f;
        return Mathf.Round(toReturn * 100) / 100.0f;
    }
}