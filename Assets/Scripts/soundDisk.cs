using UnityEngine;

public class KeySoundPlayer : MonoBehaviour
{
    [Header("Audio")]
    public AudioSource audioSource;

    public AudioClip tKeySound;
    public AudioClip uKeySound;
    public AudioClip vKeySound;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            PlaySound(tKeySound);
        }

        if (Input.GetKeyDown(KeyCode.U))
        {
            PlaySound(uKeySound);
        }

        if (Input.GetKeyDown(KeyCode.V))
        {
            PlaySound(vKeySound);
        }
    }

    void PlaySound(AudioClip clip)
    {
        if (audioSource != null && clip != null)
        {
            audioSource.PlayOneShot(clip);
        }
    }
}
