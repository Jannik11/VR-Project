using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// Zeigt den Highscore auf der dafür vorgesehenden Fläche an.
/// </summary>
public class HighscoreUi : MonoBehaviour {
    [SerializeField] Transform scoresParent;
    [SerializeField] GameObject scorePrefab;
    [SerializeField] GameObject canvas;

    List<GameObject> uiElements = new List<GameObject>();

    /// <summary>
    /// bekommt alle Scores die angezeigt werden sollen.
    /// und schreibt sie auf dei Highscoretafel
    /// </summary>
    /// <param name="scores">alle Scores die auf der HighscoreTafel angezeigt werden sollen</param>
    public void UpdateUI(List<HighscoreElement> scores) {
        for (int i = 0; i < scores.Count; i++) {
            HighscoreElement el = scores[i];
            GameObject inst;
            if (el.score > 0) {
                if (i >= uiElements.Count) {
                    inst = Instantiate(scorePrefab, canvas.transform.position, Quaternion.identity);
                    inst.transform.SetParent(scoresParent);
                    uiElements.Add(inst);
                    inst.transform.localScale = new Vector3(8.0f,8.0f,8.0f);
                    inst.transform.rotation = canvas.transform.rotation;
                }
                var texts = uiElements[i].GetComponentsInChildren<TextMeshProUGUI>();
                texts[0].SetText(el.date);
                texts[1].SetText(el.score.ToString());
            }
        }
    }
}
