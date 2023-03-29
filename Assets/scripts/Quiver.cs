using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using VirtualGrasp;

public class Quiver : MonoBehaviour {

    [SerializeField] Transform arrow;
    [SerializeField] Transform rightHand;
    [SerializeField] XRIDefaultInputActions input;

    private bool enteredQuiver = false;

    Transform newArrow;

    void Awake()
    {
        input = new();
        input.XRIRightHandInteraction.Enable();
        input.XRIRightHandInteraction.Select.performed += GrabArrow;
        input.XRIRightHandInteraction.Select.canceled += ReleaseArrow;

        EventSystem.current.OnArrowNock += DestroyArrow;

    }


    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        enteredQuiver = true;
    }
    private void OnTriggerExit(Collider other)
    {
        enteredQuiver = false;
    }

    private void GrabArrow(InputAction.CallbackContext obj)
    {
        if (enteredQuiver && Bow.instance.BowState == BowState.IDLE)
        {
            newArrow = Instantiate(arrow, rightHand.position, rightHand.rotation, rightHand);

            EventSystem.current.TriggerOnArrowTake();
        }
    }
    private void ReleaseArrow(InputAction.CallbackContext obj)
    {
        //Destroy(newArrow.gameObject);
    }

    private void DestroyArrow()
    {
        Destroy(newArrow.gameObject);
    }
}
