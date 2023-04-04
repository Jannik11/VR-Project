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
    }


    private void OnTriggerEnter(Collider other) {
        leftHandEnteredQuiver = other.CompareTag("leftHand");
        rightHandEnteredQuiver = other.CompareTag("rightHand");
    }
    private void OnTriggerExit(Collider other) {
        leftHandEnteredQuiver = !other.CompareTag("leftHand");
        rightHandEnteredQuiver = !other.CompareTag("rightHand");
    }

    private void GrabArrowRight(InputAction.CallbackContext obj) {
        if (rightHandEnteredQuiver && Hands.instance.Right == HandState.NONE) {
            GrabArrow(rightHand);
            EventSystem.current.TriggerOnArrowGrab(Side.RIGHT);
        }
    }

    private void GrabArrowLeft(InputAction.CallbackContext obj) {
        if (leftHandEnteredQuiver && Hands.instance.Left == HandState.NONE) {
            GrabArrow(leftHand);
            EventSystem.current.TriggerOnArrowGrab(Side.LEFT);
        };
    }
    private void GrabArrow(Transform hand) {
        if (Bow.instance.BowState == BowState.IDLE) {
            Transform arrow = Instantiate(this.arrow, hand.position, hand.rotation);
            Arrow arrowScript = arrow.GetComponent<Arrow>();
            arrowScript.Init(hand, bowString, aimTarget);
        }
    }
}
