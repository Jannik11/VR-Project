using UnityEngine;

public class SoundEffects : MonoBehaviour
{
    /// <summary>
    /// Soundquelle
    /// </summary>
    [SerializeField] AudioSource audioSrc;

    /// <summary>
    /// Soundclips
    /// </summary>
    [SerializeField] AudioClip arrowShootSound;
    [SerializeField] AudioClip[] glassBreakingSounds;


    // Start is called before the first frame update
    void Start()
    {
        EventManager.current.OnArrowShoot += PlayArrowShootSound;
        EventManager.current.OnArrowHitGlass += PlayGlasBreakingSound;
    }

    /// <summary>
    /// Spielt einen zufällig ausgewählten Glasszersplittern-Sound ab,
    /// wenn ein Pfeil eine Scheibe trifft
    /// </summary>
    private void PlayGlasBreakingSound()
    {
        audioSrc.clip = glassBreakingSounds[UnityEngine.Random.Range(0, glassBreakingSounds.Length)];
        audioSrc.Play();
    }

    /// <summary>
    /// Spielt den Pfeilschuss-Sound,
    /// wenn ein Pfeil abgeschossen wurde
    /// </summary>
    private void PlayArrowShootSound()
    {
        audioSrc.clip = arrowShootSound;
        audioSrc.Play();
    }
}
