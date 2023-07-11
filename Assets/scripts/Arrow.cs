using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour {

    private Rigidbody rb;
    private Transform hand;
    private Transform bowString;
    private Transform aimTarget;
    public int id;

    private Vector3 scale;

    private const float ARROWSPEED = 2000.0f;

    public ArrowState ArrowState { get; private set; } = ArrowState.NONE;

    public void Init(int id, Transform hand, Transform bowString, Transform aimTarget) {
        this.id = id;
        this.hand = hand;
        this.bowString = bowString;
        this.aimTarget = aimTarget;

        ArrowState = ArrowState.INHAND;

        transform.SetParent(hand, true);
    }


    // Start is called before the first frame update
    void Start() {
        EventManager.current.OnArrowNock += NockArrow;
        EventManager.current.OnArrowShoot += ShootArrow;
        EventManager.current.OnArrowHit += ArrowHit;

        scale = transform.localScale;
    }

    // Update is called once per frame
    void Update() {

        switch (ArrowState) {
            case ArrowState.INBOW:
                transform.LookAt(aimTarget.position, Vector3.up);
                break;
            case ArrowState.FLYING:
                transform.LookAt(transform.position + rb.velocity, Vector3.up);
                break;

            default:
                break;
        }
    }

    /// <summary>
    /// legt einen Pfeil der in der Hand des Spielers ist in den Bogen ein
    /// </summary>
    public void NockArrow() {

        EventManager.current.OnArrowNock -= NockArrow;
        ArrowState = ArrowState.INBOW;

        transform.position = bowString.position;
        transform.SetParent(bowString, true);
    }

    /// <summary>
    /// schießt einen Pfeil der im Bogen lag ab, indem ihm eine Kraft hinzugefügt wird.
    /// </summary>
    public void ShootArrow() {

        EventManager.current.OnArrowShoot -= ShootArrow;
        ArrowState = ArrowState.FLYING;

        rb = gameObject.AddComponent<Rigidbody>();

        rb.useGravity = true;
        rb.isKinematic = false;
        rb.detectCollisions = true;

        transform.parent = null;

        Vector3 forceVector = aimTarget.position - bowString.position;


        rb.AddForce(forceVector * ARROWSPEED);
    }

    /// <summary>
    /// Wenn der Pfeil ein Ziel trifft wird er zerstört
    /// </summary>
    /// <param name="collider"> collider mit dem kollidiert wird</param>
    public void ArrowHit(int id, Collider collider) {
        if (id != this.id || ArrowState != ArrowState.FLYING) {
            return;
        };

        EventManager.current.OnArrowHit -= ArrowHit;

        Destroy(gameObject);
    }

    /// <summary>
    /// hier werden die Kollosionen getriggert. Es wird geprüft um was für ein Objekt es sich handelt mit dem Kollidiert wird. 
    /// das entsprechende Event wird dann getriggert.
    /// </summary>
    /// <param name="collision">die Kollision</param>
    private void OnCollisionEnter(Collision collision) {
        Debug.Log("OnCollisionEnter triggered");
        if (collision.collider.CompareTag("Quiver") || collision.collider.CompareTag("String")) {
            return;
        }
        if (collision.collider.CompareTag("Glass")) {
            EventManager.current.TriggerOnArrowHitGlass();
        }

        if (ArrowState == ArrowState.FLYING) {
            EventManager.current.TriggerOnArrowHit(id, collision.collider);
        }
    }
}

