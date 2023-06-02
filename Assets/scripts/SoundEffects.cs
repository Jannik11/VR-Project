using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEffects : MonoBehaviour
{
    [SerializeField] AudioSource audioSrc;

    [SerializeField] AudioClip arrowShootSound;
    [SerializeField] AudioClip[] glassBreakingSounds;

    // Start is called before the first frame update
    void Start()
    {
        EventManager.current.OnArrowShoot += PlayArrowShootSound;
        EventManager.current.OnArrowHit += PlayGlasBreakingSound;
    }

    private void PlayGlasBreakingSound(int ignored1, Collider ignored2)
    {
        Debug.Log("ICH BIN GETRIGGERT! :(");
        audioSrc.clip = glassBreakingSounds[UnityEngine.Random.Range(0, glassBreakingSounds.Length)];
        audioSrc.Play();
    }

    private void PlayArrowShootSound()
    {
        audioSrc.clip = arrowShootSound;
        audioSrc.Play();
    }
}
