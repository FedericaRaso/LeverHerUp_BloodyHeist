using UnityEngine;

public class SFXManager : MonoBehaviour
{
    [SerializeField] private AudioSource oneShotSource;
    [SerializeField] private AudioSource loopSource;

    [Header("SFX Clips")]
    public AudioClip transformToBatSound;
    public AudioClip transformBackSound;
    public AudioClip transformFailedSound;
    public AudioClip bloodSuckSound;
    public AudioClip interactSound;
    public AudioClip flySound;

    private void Play(AudioClip clip)
    {
        if (clip != null)
            oneShotSource.PlayOneShot(clip);
    }

    public void TransformToBat() => Play(transformToBatSound);
    public void TransformBack() => Play(transformBackSound);
    public void TransformFailed() => Play(transformFailedSound);
    public void BloodSuck() => Play(bloodSuckSound);
    public void Interact() => Play(interactSound);
    
    public void StartFlyLoop()
    {
        if (flySound == null) return;

        loopSource.clip = flySound;
        loopSource.loop = true;
        loopSource.Play();
    }

    public void StopFlyLoop()
    {
        if (loopSource.isPlaying)
            loopSource.Stop();
    }
}
