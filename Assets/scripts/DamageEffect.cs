using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DamageEffect : MonoBehaviour
{
    Image image;
    bool alphaIsZero = true;

    // Start is called before the first frame update
    void Start()
    {
        EventManager.current.OnPlayerHit += ShowDamageEffect;

        image = GetComponent<Image>();
    }

    private void ShowDamageEffect() {
        Color imageColor = image.color;
        imageColor.a = 0.2f;
        image.color = imageColor;
        alphaIsZero = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(!alphaIsZero) {
            Color imageColor = image.color;
            imageColor.a -= 0.002f;
            if(imageColor.a <= 0.0f) {
                alphaIsZero = true;
            }
            image.color = imageColor;
        }

    }
}
