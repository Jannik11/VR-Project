using System;
using System.Collections.Generic;
using UnityEngine;

//Größe der einezelnen Bausteine 5 x 5 x 15
public class LevelGenerator : MonoBehaviour {


    [SerializeField] Transform[] targets;

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
        for (int i = 0; i < chunksToLoad; i++) {
            int idx = UnityEngine.Random.Range(0, targets.Length);
            spawnedTargets.Add(Instantiate<Transform>(targets[idx], new Vector3(0, 0, chunkSize * (i + 3)), Quaternion.identity));
        }
    }

    public void ResetLevel() {
        for(int i = 0; i < spawnedTargets.Count; i++) {
            Destroy(spawnedTargets[i].gameObject);
        }
        spawnedTargets = new List<Transform>();
        distanceTraveled = 0.0f;
        actualSpeed = speed;
        running = false;
    }

    public void StartGame() {
        Debug.Log("ich starte das Spiel Generator");
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
            actualSpeed += speedIncrease * Time.deltaTime;
            float distance = -actualSpeed * Time.deltaTime;
            distanceTraveled += distance;

            if (spawnedTargets[0].position.z < despawnPointZ) {
                Destroy(spawnedTargets[0].gameObject);
                spawnedTargets.RemoveAt(0);
                distanceTraveled = 0;
                spawnedTargets.Add(Instantiate<Transform>(targets[UnityEngine.Random.Range(0, targets.Length)], new Vector3(0, 0, chunkSize * (chunksToLoad - 1)), Quaternion.identity));
            }
            foreach (Transform target in spawnedTargets) {
                target.transform.Translate(new Vector3(0, 0, distance));
            }
        }
    }
}
