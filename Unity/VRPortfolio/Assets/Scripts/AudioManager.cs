using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource source;

    public void PlayAudioOnce(AudioClip clip)
    {
        source.clip = clip;
        source.Play();
    }

    public IEnumerator PlayAudioDelayed(AudioClip clip, float time)
    {
        source.clip = clip;

        yield return new WaitForSeconds(time);

        source.Play();
    }
}
