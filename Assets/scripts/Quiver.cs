using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using VirtualGrasp;

public class Quiver : MonoBehaviour {

    [SerializeField] Transform arrow;
    [SerializeField] Transform hand;
    [SerializeField] XRIDefaultInputActions input;
    [SerializeField] Transform aimTarget;
    [SerializeField] Transform bowString;

    private bool enteredQuiver = false;

    void Start() {
        input = new();
        input.XRIRightHandInteraction.Enable();
        input.XRIRightHandInteraction.Select.performed += GrabArrow;
        input.XRIRightHandInteraction.Select.canceled += ReleaseArrow;
    }


    private void OnTriggerEnter(Collider other) {
        enteredQuiver = true;
    }
    private void OnTriggerExit(Collider other) {
        enteredQuiver = false;
    }

    private void GrabArrow(InputAction.CallbackContext obj) {
        if (enteredQuiver && Bow.instance.BowState == BowState.IDLE) {
            Transform newArrow = Instantiate(arrow, hand.position, hand.rotation);
            newArrow.GetComponent<Arrow>().Init(hand, bowString, aimTarget);
            EventSystem.current.TriggerOnArrowTake();
        }
    }
    private void ReleaseArrow(InputAction.CallbackContext obj) {
        //Destroy(newArrow.gameObject);
    }
}
