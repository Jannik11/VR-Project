using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEffects : MonoBehaviour
{
    [SerializeField] AudioSource audioSrc;

    [SerializeField] AudioClip arrowShootSound;

    // Start is called before the first frame update
    void Start()
    {
        EventManager.current.OnArrowShoot += PlayArrowShootSound;
    }

    private void PlayArrowShootSound()
    {
        audioSrc.clip = arrowShootSound;
        audioSrc.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
