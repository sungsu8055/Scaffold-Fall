using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private AudioSource source;

    private void Start()
    {
        source = gameObject.AddComponent<AudioSource>();
    }

    public void PlayAudioOnce(AudioClip clip)
    {
        // StartCoroutine(PlayVarietyAudioOnce(clip));
        source.clip = clip;
        source.Play();
    }

    IEnumerator PlayVarietyAudioOnce(AudioClip clip)
    {
        // 함수가 새로 실행되어 새로운 클립이 들어올 때 기존 클립 재생 정지
        //source.Stop();

        // 파라미터로 들어온 클립으로 교체 후 재생
        source.clip = clip;
        source.Play();
        
        // 오디오 재생 중에 Destory로 넘어가지 않도록 while문 돌림
        while (source.isPlaying)
        {
            yield return null;
        }
    }
}
