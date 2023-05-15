using UnityEngine;
using UnityEngine.Events;

public class UnfreezeFragment : MonoBehaviour {

    [Tooltip("If true, all sibling fragments will be unfrozen if the trigger conditions for this fragment are met.")]
    public bool unfreezeAll = true;

    [Tooltip("This callback is invoked when the fracturing process has been completed.")]
    public UnityEvent onFractureCompleted;

    // True if this fragment has already been unfrozen
    private bool isFrozen = true;

    void OnCollisionEnter(Collision collision) {
        if (!this.isFrozen) {
            return;
        }

        if (collision.contactCount > 0) {
            this.Unfreeze();
        }
    }

    void OnTriggerEnter(Collider collider) {
        if (!this.isFrozen) {
            return;
        }
        this.Unfreeze();
    }

    private void Unfreeze() {
        if (this.unfreezeAll) {
            foreach (UnfreezeFragment fragment in this.transform.parent.GetComponentsInChildren<UnfreezeFragment>()) {
                fragment.UnfreezeThis();
            }
        } else {
            UnfreezeThis();
        }

        if (this.onFractureCompleted != null) {
            this.onFractureCompleted.Invoke();
        }
    }

    private void UnfreezeThis() {
        this.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
        this.isFrozen = false;
    }
}
