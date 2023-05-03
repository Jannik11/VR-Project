using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using VirtualGrasp;

public class Quiver : MonoBehaviour {

    [SerializeField] Transform arrow;
    [SerializeField] Transform rightHand;
    [SerializeField] Transform leftHand;
    [SerializeField] XRIDefaultInputActions input;
    [SerializeField] Transform aimTarget;
    [SerializeField] Transform bowString;

    private bool leftHandEnteredQuiver = false;
    private bool rightHandEnteredQuiver = false;

    public static Quiver instance;

    private int currentArrowId = 0;
    public Arrow CurrentArrow { get; private set; }

    private void Awake() {
        instance = this;
    }

    void Start() {
        input = new();
        input.XRIRightHandInteraction.Enable();
        input.XRILeftHandInteraction.Enable();
        input.XRIRightHandInteraction.Select.performed += GrabArrowRight;
        input.XRILeftHandInteraction.Select.performed += GrabArrowLeft;

        EventManager.current.OnArrowHit += ArrowHit;

    }


    private void OnTriggerEnter(Collider other) {
        leftHandEnteredQuiver = other.CompareTag("leftHand");
        rightHandEnteredQuiver = other.CompareTag("rightHand");
    }
    private void OnTriggerExit(Collider other) {
        if (leftHandEnteredQuiver && other.CompareTag("leftHand"))
        leftHandEnteredQuiver = false;
        if (rightHandEnteredQuiver && other.CompareTag("rightHand"))
        rightHandEnteredQuiver = false;
    }

    private void GrabArrowRight(InputAction.CallbackContext obj) {
        if (rightHandEnteredQuiver && Bow.instance.BowState == BowState.IDLE && !Hands.instance.IsArrowInAnyHand()) {
            GrabArrow(rightHand);
            EventManager.current.TriggerOnArrowGrab(Side.RIGHT);
        }
    }

    private void GrabArrowLeft(InputAction.CallbackContext obj) {
        if (leftHandEnteredQuiver && Bow.instance.BowState == BowState.IDLE && !Hands.instance.IsArrowInAnyHand()) {
            GrabArrow(leftHand);
            EventManager.current.TriggerOnArrowGrab(Side.LEFT);
        }
    }
    private void GrabArrow(Transform hand) {
        if (Bow.instance.BowState == BowState.IDLE) {
            Transform arrow = Instantiate(this.arrow, hand.position, hand.rotation);
            CurrentArrow = arrow.GetComponent<Arrow>();
            CurrentArrow.Init(currentArrowId++, hand, bowString, aimTarget);
        }
    }

    private void ArrowHit(int id, Collider collider) {
        Debug.Log("ArrowHit: Quiver");

        //CurrentArrow = null;
    }
}
