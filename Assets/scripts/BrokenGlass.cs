using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrokenGlass : MonoBehaviour {

    private List<Transform> topLeftFragments = new List<Transform>(),
        topRightFragments = new List<Transform>(),
        botLeftFragments = new List<Transform>(),
        botRightFragments = new List<Transform>();

    private bool topLeft, topRight, botLeft, botRight;

    // Start is called before the first frame update
    void Start() {
        Vector3 center = transform.position;

        foreach (Transform fragment in transform) {

            Vector3 fragCenter = fragment.GetComponent<MeshRenderer>().bounds.center;

            if (fragCenter.x > center.x) {
                if (fragCenter.y > center.y) {
                    topRightFragments.Add(fragment);
                } else {
                    botRightFragments.Add(fragment);
                }
            } else {

                if (fragCenter.y > center.y) {
                    topLeftFragments.Add(fragment);
                } else {
                    botLeftFragments.Add(fragment);
                }
            }
        }
    }

    public void RegisterHit(Vector3 hitPoint) {
        Vector3 center = transform.position;
        List<Transform> someList = new List<Transform>();


        if (hitPoint.x > center.x) {
            if (hitPoint.y > center.y) {
                topRight = true;
                someList.AddRange(topRightFragments);

                if(topLeft) {
                    someList.AddRange(botLeftFragments);
                    someList.AddRange(botRightFragments);
                } else if (botLeft) {
                    someList.AddRange(botRightFragments);
                }

            } else {
                botRight = true;
                someList.AddRange(botRightFragments);

                if (topLeft) {
                    someList.AddRange(botLeftFragments);
                }

            }
        } else {

            if (hitPoint.y > center.y) {
                topLeft = true;
                someList.AddRange(topLeftFragments);

                if (topRight) {
                    someList.AddRange(botLeftFragments);
                    someList.AddRange(botRightFragments);
                } else if (botRight) {
                    someList.AddRange(botLeftFragments);
                }

            } else {
                botLeft = true;
                someList.AddRange(botLeftFragments);

                if (topRight) {
                    someList.AddRange(botRightFragments);
                }
            }
        }



        foreach (Transform fragment in someList) {
            fragment.GetComponent<MeshCollider>().enabled = true;

            Rigidbody rb = fragment.GetComponent<Rigidbody>();
            rb.isKinematic = false;
            rb.detectCollisions = true;
            rb.velocity = Vector3.forward * 5.0f;
        }

    }
}