using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioSource effectAudioSource;
    [SerializeField] private AudioSource defaultAudioSource;
    [SerializeField] private AudioSource bossAudioSource;
    [SerializeField] private AudioClip shootClip;
    [SerializeField] private AudioClip reLoadClip;
    [SerializeField] private AudioClip enegyClip;

    public void PlayShootSound()
    {
        effectAudioSource.PlayOneShot(shootClip);
    }
    public void PlayReLoadSound()
    {
        effectAudioSource.PlayOneShot(reLoadClip);
    }
    public void PlayEnergySound()
    {
        effectAudioSource.PlayOneShot(enegyClip);
    }
    public void PlayDefaultAudio()
    {
        defaultAudioSource.Play();
        bossAudioSource.Stop();
    }
    public void PlayBossAudio()
    {
        bossAudioSource.Play();
        defaultAudioSource.Stop();
    }
    public void StopAudioGame()
    {
        defaultAudioSource.Stop();
        bossAudioSource.Stop();
        effectAudioSource.Stop();
    }

}
