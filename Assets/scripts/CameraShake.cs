using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour {
    // Camera Information
    private Vector3 orignalCameraPos;

    // Shake Parameters
    [SerializeField] private float shakeDuration = 1.0f;
    [SerializeField] private float shakeAmount = 0.7f;

    private float _shakeTimer;

    private bool canShake;

    // Start is called before the first frame update
    void Start() {
        orignalCameraPos = transform.localPosition;
    }

    // Update is called once per frame
    void Update() {
        if (canShake) {
            StartCameraShakeEffect();
        }
    }

    public void ShakeCamera() {
        canShake = true;
        _shakeTimer = shakeDuration;
    }

    private void StartCameraShakeEffect() {
        if (_shakeTimer > 0) {
            transform.localPosition = orignalCameraPos + Random.insideUnitSphere * shakeAmount;
            _shakeTimer -= Time.deltaTime;
        }
        else {
            _shakeTimer = 0f;
            transform.position = orignalCameraPos;
            canShake = false;
        }
    }

}
