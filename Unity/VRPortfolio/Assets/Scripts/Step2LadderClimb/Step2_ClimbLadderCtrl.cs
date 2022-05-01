using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Step2_ClimbLadderCtrl : MonoBehaviour
{
    public GameObject climbInstruction;
    public Transform popupPos;

    public GameObject player;

    public GameObject approachGuide;

    [Header("Audio")]
    public AudioManager audioManager;
    public AudioClip moveToLadder;
    public AudioClip select;

    void Start()
    {
        Invoke("StartClimbLadder", 1.0f);
    }

    void StartClimbLadder()
    {
        climbInstruction.SetActive(true);

        climbInstruction.transform.SetPositionAndRotation(popupPos.position, popupPos.rotation);
        // �ȳ� ���� ���
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

        // ���� ȿ���� ���
        audioManager.PlayAudioOnce(select);

        approachGuide.SetActive(true);
    }
}
