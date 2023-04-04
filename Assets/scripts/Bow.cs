using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VirtualGrasp;

public class Bow : MonoBehaviour {
    public static Bow instance;

    [SerializeField] private Transform top;
    [SerializeField] private Transform mid;
    [SerializeField] private Transform bot;

    [SerializeField] private Transform hand;

    [SerializeField] private Transform forceTarget;

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

        VG_Controller.OnObjectFullyReleased.AddListener(ReleaseString);
        VG_Controller.OnObjectGrasped.AddListener(GrabString);

        EventSystem.current.OnArrowNock += NockArrow;
        EventSystem.current.OnArrowShoot += ShootArrow;

    }


    // Update is called once per frame
    void Update() {

        if (stringGrabbed) {
            mid.localPosition = new Vector3(Mathf.Clamp(mid.localPosition.x, midDefaultOffset, 60.0f), transform.position.y, transform.position.z);

        } else {
            mid.localPosition = new Vector3(midDefaultOffset, transform.position.y, transform.position.z);
        }

        Vector3[] positions = new Vector3[] { top.position, mid.position, bot.position };
        lineRenderer.SetPositions(positions);




    }

    private void ReleaseString(VG_HandStatus arg0) {
        if (arg0.m_selectedObject.CompareTag("String")) {
            if (BowState == BowState.AIMING) {
                stringGrabbed = false;
                EventSystem.current.TriggerOnArrowShoot();
            }
            BowState = BowState.IDLE;
        }
    }

    private void GrabString(VG_HandStatus arg0) {
        if (arg0.m_selectedObject.gameObject.CompareTag("String")) {
            stringGrabbed = true;
            BowState = BowState.AIMING;
        }
    }

    private void NockArrow() {
        BowState = BowState.AIMING;
    }

    private void ShootArrow() {
        BowState = BowState.IDLE;
    }
}
