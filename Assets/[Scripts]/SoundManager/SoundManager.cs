using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField]
    public AudioSource soundSource;

    [SerializeField]
    List<AudioClip> dialogueSoundClips;

    // This is a general template of how to use sound clips. Just make sure to reference the audio source in 
    // the scripts you want to call the sounds in.
    //
    //soundManager.soundSource.clip = ____SoundClips[element number in list];
    //soundManager.soundSource.pitch = 1.0f + Random.Range(-pitchVariance, pitchVariance);
    //soundManager.soundSource.Play();

    private void Start()
    {
        StartCoroutine(LookAtAllThatGas());
    }

    private void Update()
    {
        

    }


    public IEnumerator LookAtAllThatGas()
    {
        yield return new WaitForSeconds(2.5f);
        soundSource.clip = dialogueSoundClips[0];
        soundSource.Play();
    }
    

}
