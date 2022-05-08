using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartScene : MonoBehaviour
{
    [Header("Player")]
    public GameObject player;
    public Transform popupPos;

    [Header("Audio")]
    public AudioManager audioManager;
    public AudioClip startSceneInfo;
    public AudioClip entryZone;
    public AudioClip select;

    void Start()
    {
        StartCoroutine(SceneInfo());
    }

    IEnumerator SceneInfo()
    {
        this.transform.position = popupPos.position;

        audioManager.PlayAudioOnce(startSceneInfo);

        while (audioManager.source.isPlaying)
        {
            yield return null;
        }

        // yield return new WaitForSeconds(6.0f);

        this.transform.GetComponentInChildren<Text>().text = 
            " �޼��� ���̽�ƽ�� �����¿�� ������ �̵��Ͻʽÿ�. \n ���鿡 ���̴� ���� �̵� �� ü���� ���� �˴ϴ�.";

        audioManager.PlayAudioOnce(entryZone);

        this.transform.GetChild(2).gameObject.SetActive(true);
        this.transform.GetChild(3).gameObject.SetActive(true);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("ControllerLeft") ||
            other.CompareTag("ControllerRight"))
        {
            audioManager.PlayAudioOnce(select);

            player.GetComponent<CharacterController>().enabled = true;

            Destroy(this.gameObject);
        }
    }
}
