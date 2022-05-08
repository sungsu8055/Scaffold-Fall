using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Step2_ClimbLadderCtrl : MonoBehaviour
{
    public GameObject climbInstruction;
    public Transform popupPos;
    public Transform startPos;

    public GameObject player;

    public GameObject approachGuide;

    [Header("Audio")]
    public AudioManager audioManager;
    public AudioClip moveToLadder;
    public AudioClip select;

    void Start()
    {
        // Invoke("StartClimbLadder", 1.0f);
    }

    public void StartClimbLadder()
    {
        player.GetComponent<CharacterController>().enabled = false;

        player.transform.SetPositionAndRotation(startPos.position, startPos.rotation);

        climbInstruction.SetActive(true);

        climbInstruction.transform.SetPositionAndRotation(popupPos.position, popupPos.rotation);
        // 안내 음성 재생
        audioManager.PlayAudioOnce(moveToLadder);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("ControllerLeft") ||
            other.CompareTag("ControllerRight"))
        {
            ApproachLadder();
        }
    }

    public void ApproachLadder()
    {
        player.GetComponent<CharacterController>().enabled = true;

        climbInstruction.gameObject.SetActive(false);

        // 선택 효과음 재생
        audioManager.PlayAudioOnce(select);

        approachGuide.SetActive(true);
    }
}
