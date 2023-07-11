using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// Repräsentation des Köchers
/// </summary>
public class Quiver : MonoBehaviour {

    /// <summary>
    /// Transforms des Pfeils und der beiden Hände
    /// </summary>
    [SerializeField] Transform arrow;
    [SerializeField] Transform rightHand;
    [SerializeField] Transform leftHand;
    [SerializeField] XRIDefaultInputActions input;

    /// <summary>
    /// Transforms des Sehnenmittelpunkts und dem Griff
    /// zur Berechnung der Schussrichtung
    /// </summary>
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

    }

    /// <summary>
    /// Überprüft, welche Hand gerade in den Köcher greift
    /// </summary>
    /// <param name="other">Collider der mit dem Köcher kollidiert</param>
    private void OnTriggerEnter(Collider other) {
        leftHandEnteredQuiver = other.CompareTag("leftHand");
        rightHandEnteredQuiver = other.CompareTag("rightHand");
    }

    /// <summary>
    /// Überprüft, ob eine Hand den Köcher wieder verlassen hat
    /// </summary>
    /// <param name="other">Collider der mit dem Köcher kollidiert</param>
    private void OnTriggerExit(Collider other) {
        if (leftHandEnteredQuiver && other.CompareTag("leftHand"))
        leftHandEnteredQuiver = false;
        if (rightHandEnteredQuiver && other.CompareTag("rightHand"))
        rightHandEnteredQuiver = false;
    }

    /// <summary>
    /// Wird ausgelöst, wenn die rechte Hand einen Gegenstand greift.
    /// Überprüft, ob die Hand im Köcher ist und aktuell kein Pfeil gegriffen wird.
    /// </summary>
    /// <param name="obj">Callback Context der Hand</param>
    private void GrabArrowRight(InputAction.CallbackContext obj) {
        if (rightHandEnteredQuiver && Bow.instance.BowState == BowState.IDLE && !Hands.instance.IsArrowInAnyHand()) {
            GrabArrow(rightHand);
            EventManager.current.TriggerOnArrowGrab(Side.RIGHT);
        }
    }

    /// <summary>
    /// Wird ausgelöst, wenn die linke Hand einen Gegenstand greift.
    /// Überprüft, ob die Hand im Köcher ist und aktuell kein Pfeil gegriffen wird.
    /// </summary>
    /// <param name="obj">Callback Context der Hand</param>
    private void GrabArrowLeft(InputAction.CallbackContext obj) {
        if (leftHandEnteredQuiver && Bow.instance.BowState == BowState.IDLE && !Hands.instance.IsArrowInAnyHand()) {
            GrabArrow(leftHand);
            EventManager.current.TriggerOnArrowGrab(Side.LEFT);
        }
    }

    /// <summary>
    /// Instantiert und initialisiert einen Pfeil
    /// </summary>
    /// <param name="hand">Transform der Hand, die den Pfeil hält</param>
    private void GrabArrow(Transform hand) {
        if (Bow.instance.BowState == BowState.IDLE) {
            Transform arrow = Instantiate(this.arrow, hand.position, hand.rotation);
            CurrentArrow = arrow.GetComponent<Arrow>();
            CurrentArrow.Init(currentArrowId++, hand, bowString, aimTarget);
        }
    }
}
