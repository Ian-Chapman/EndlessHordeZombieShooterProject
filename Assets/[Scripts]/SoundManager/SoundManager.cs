using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField]
    public AudioSource soundSource;

    [SerializeField]
    List<AudioClip> zombieSoundClips;

    [SerializeField]
    List<AudioClip> playerSoundClips;

    [SerializeField]
    List<AudioClip> dialogueSoundClips;

    [SerializeField]
    List<AudioClip> soundEffectClips;

    [SerializeField]
    public float pitchVariance = .15f;

    // This is a general template of how to use sound clips. Just make sure to reference the audio source in 
    // the scripts you want to call the sounds in.
    //
    //soundManager.soundSource.clip = ____SoundClips[element number in list];
    //soundManager.soundSource.pitch = 1.0f + Random.Range(-pitchVariance, pitchVariance);
    //soundManager.soundSource.Play();

    public void GunShotSound()
    {
        soundSource.clip = playerSoundClips[1];
        soundSource.Play();
    }

    public void ReloadSound()
    {
        soundSource.clip = playerSoundClips[0];
        soundSource.Play();
    }

}
