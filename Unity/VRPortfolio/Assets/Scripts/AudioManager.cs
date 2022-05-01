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
        // �Լ��� ���� ����Ǿ� ���ο� Ŭ���� ���� �� ���� Ŭ�� ��� ����
        //source.Stop();

        // �Ķ���ͷ� ���� Ŭ������ ��ü �� ���
        source.clip = clip;
        source.Play();
        
        // ����� ��� �߿� Destory�� �Ѿ�� �ʵ��� while�� ����
        while (source.isPlaying)
        {
            yield return null;
        }
    }
}
