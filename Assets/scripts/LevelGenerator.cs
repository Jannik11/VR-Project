using System;
using System.Collections.Generic;
using UnityEngine;

//Größe der einezelnen Bausteine 5 x 5 x 15
/// <summary>
/// Generiert aus den Levels prozedural die einzelnen Level,
/// indem zufällige Levelobjekte aus den einzelnen Levels gespawnt werden.
/// </summary>
public class LevelGenerator : MonoBehaviour {


    //[SerializeField] Transform[] targets;


    [SerializeField] LevelSO[] levels;

    int STARTGAP = 3;

    [SerializeField] int lvlSize = 6;
    public int CurrLevel { get; private set; } = 0;
    private int currElementsInLvl = 1;

    Transform instantiated;

    //alle gespawnten Targets in der richtigen Reihenfolge
    //idx 0 ist das Target welches dem Spieler am nächsten ist
    List<Transform> spawnedTargets = new List<Transform>();

    [SerializeField] float speed = 5;

    float actualSpeed;

    [SerializeField] float speedIncrease = 5;

    float distanceTraveled;

    [SerializeField] int chunksToLoad;

    [SerializeField] int chunkSize;

    [SerializeField] float despawnPointZ;

    private bool running = false;


    // Start is called before the first frame update
    void Start() {
        actualSpeed = speed;
    }

    void GenerateLevel() {
        for (int i = 0; i < chunksToLoad + STARTGAP; i++) {
            generateTarget(i);
        }
    }

    /// <summary>
    /// Spawned ein Target an Stelle i, heißt chunksize * i hinter dem Startpunkt.
    /// </summary>
    private void generateTarget(int i) {
        bool lastLevel = CurrLevel >= levels.Length - 1;
        if (currElementsInLvl > lvlSize && !lastLevel) { //nicht endlosLevel und Level ist voll also Level-Trenner
            currElementsInLvl = 0;
            CurrLevel++;
            spawnedTargets.Add(Instantiate<Transform>(levels[CurrLevel].levelSeperator, new Vector3(0, 0, chunkSize * (i + 2)), Quaternion.identity));
        } else {
            currElementsInLvl++;
            spawnedTargets.Add(Instantiate<Transform>(levels[CurrLevel].GetTarget(), new Vector3(0, 0, chunkSize * (i + 2)), Quaternion.identity));
        }
    }

    /// <summary>
    /// entfernt alle erstellten Targets wieder, wenn der Spieler verloren hat.
    /// </summary>
    public void ResetLevel() {
        running = false;

        for (int i = 0; i < spawnedTargets.Count; i++) {
            Destroy(spawnedTargets[i].gameObject);
        }
        CurrLevel = 0;
        currElementsInLvl = 0;
        spawnedTargets = new List<Transform>();
        distanceTraveled = 0.0f;
        actualSpeed = speed;   
    }

    public void StartGame() {
        GenerateLevel();
        running = true;
    }

    public void PauseGame() {
        running = false;
    }

    public void ResumeGame() {
        running = true;
    }


    // Update is called once per frame
    void Update() {
        if (running) {
            
            //Geschwindigkeit steigt beim letzten Level
            if(CurrLevel >= levels.Length - 1) {
                actualSpeed += speedIncrease * Time.deltaTime;
            }
          
            float distance = -actualSpeed * Time.deltaTime;
            distanceTraveled += distance;

            if (spawnedTargets[0].position.z < despawnPointZ) {
                Destroy(spawnedTargets[0].gameObject);
                spawnedTargets.RemoveAt(0);
                distanceTraveled = 0;
                generateTarget(chunksToLoad - 1);
            }
            foreach (Transform target in spawnedTargets) {
                target.transform.Translate(new Vector3(0, 0, distance));
            }
        }
    }
}
