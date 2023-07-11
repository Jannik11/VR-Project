using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VirtualGrasp;

/// <summary>
/// Die Logic wie der Bogen Funktioniert.
/// </summary>
public class Bow : MonoBehaviour {
    public static Bow instance;

    [SerializeField] private Transform top;
    [SerializeField] private Transform mid;
    [SerializeField] private Transform bot;

    [SerializeField] private Transform hand;

    [SerializeField] private Transform forceTarget;

    [SerializeField] private GameManager gameManager;

    private LineRenderer lineRenderer;

    private float midDefaultOffset;
    private bool stringGrabbed = false;

    public BowState BowState { get; private set; } = BowState.IDLE;
    private void Awake() {
        instance = this;
    }

    // Start is called before the first frame update
    void Start() {
        lineRenderer = GetComponent<LineRenderer>();

        midDefaultOffset = mid.localPosition.x;

        VG_Controller.OnObjectFullyReleased.AddListener(ReleaseObject);
        VG_Controller.OnObjectGrasped.AddListener(GrabObject);

        EventManager.current.OnArrowNock += NockArrow;
        EventManager.current.OnArrowShoot += ShootArrow;


    }

    private void FixedUpdate() {
        if (stringGrabbed) {
            mid.localPosition = new Vector3(Mathf.Clamp(mid.localPosition.x, midDefaultOffset, 60.0f), transform.position.y, transform.position.z);

        } else {
            mid.localPosition = new Vector3(midDefaultOffset, transform.position.y, transform.position.z);
        }        
    }


    // Update is called once per frame
    void Update() {

        Vector3[] positions = new Vector3[] { top.position, mid.position, bot.position };
        lineRenderer.SetPositions(positions);
    }

    /// <summary>
    /// wird aufgerufen wenn die Hände ein Objekt komplett loslassen.
    /// dann wird geprüft ob es der Bogen war oder die Sehne, und die entsprechenden Events getriggert.
    /// </summary>
    private void ReleaseObject(VG_HandStatus arg0) {
        if (arg0.m_selectedObject.CompareTag("String")) {
            if(!gameManager.Paused) {
                if (BowState == BowState.AIMING && Hands.instance.IsStringInAnyHand() 
                    && Quiver.instance.CurrentArrow && Quiver.instance.CurrentArrow.ArrowState == ArrowState.INBOW) { 

                    EventManager.current.TriggerOnArrowShoot();
                }
                BowState = BowState.IDLE;
            }

            EventManager.current.TriggerOnStringRelease();
            stringGrabbed = false;
            
        } else if (arg0.m_selectedObject.CompareTag("Bow")) {
            EventManager.current.TriggerOnBowRelease();
        }
    }

    /// <summary>
    /// wird aufgerufen wenn die Hände ein Objekt greifen.
    /// Der Bogen prüft dann, ob es sich um die Sehen oder den Bogen handelt und triggert die entsprechenden Events.
    /// </summary>
    private void GrabObject(VG_HandStatus arg0) {

        if (arg0.m_selectedObject.CompareTag("String")) {

            if (arg0.m_side == VG_HandSide.RIGHT) {
                EventManager.current.TriggerOnStringGrab(Side.RIGHT);
            } else if (arg0.m_side == VG_HandSide.LEFT) {
                EventManager.current.TriggerOnStringGrab(Side.LEFT);
            }
            stringGrabbed = true;
        } else if(arg0.m_selectedObject.CompareTag("Bow")) {
            if (arg0.m_side == VG_HandSide.RIGHT) {
                EventManager.current.TriggerOnBowGrab(Side.RIGHT);
            } else if (arg0.m_side == VG_HandSide.LEFT) {
                EventManager.current.TriggerOnBowGrab(Side.LEFT);
            }
        }
    }

    private void NockArrow() {
        BowState = BowState.AIMING;
    }

    private void ShootArrow() {
        BowState = BowState.IDLE;
    }
}
