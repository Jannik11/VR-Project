using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstableMovementVertical : MonoBehaviour {

    [SerializeField] float top;
    [SerializeField] float bottom;
    [SerializeField] float speed;

    bool movementAlternater = true;
    // Start is called before the first frame update
    void Start() {
        movementAlternater = Random.Range(0, 2) == 0;
        transform.position = new Vector3(0, Random.Range(bottom,top), transform.position.z);
    }

    // Update is called once per frame
    void Update() {
        float movement = (movementAlternater ? speed : -speed) * Time.deltaTime;
        transform.Translate(new Vector3(0, movement, 0));
        if (transform.position.y > top || transform.position.y < bottom) {
            movementAlternater = !movementAlternater;
        }
    }
}
