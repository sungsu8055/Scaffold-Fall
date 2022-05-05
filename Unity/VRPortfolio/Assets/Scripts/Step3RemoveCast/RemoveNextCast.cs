using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using DG.Tweening;

public class RemoveNextCast : MonoBehaviour
{
    public XRSocketInteractor SKInteractor;
    public RemovePlateInteractable RPI;
    public Transform player;
    public Transform nextCastPos;
    public DetectSafetyGear DSG;
    public ScaffoldFall SF;
    public Step3_RemoveCast RC;

    public bool nextCastPosComplete = false;

    [Header("Audio")]
    public AudioSource audioSource;

    void Update()
    {
        if (SKInteractor.hasSelection == true && nextCastPosComplete == false)
        {
            audioSource.Play();

            this.GetComponent<MeshRenderer>().enabled = false;
            player.DOMove(nextCastPos.position, 1.0f).SetDelay(1.0f);
            // RPI.restoreTrackOption = true;
            nextCastPosComplete = true;
            SF.isFellDown = false;

            StartCoroutine(RC.RemoveSecondFormwork());

            if (DSG.detectSafetyHat == false && DSG.detectSafetyBelt == false)
            {
                // ���� �屸 ������� ���� ���·� ��ȯ
                player.GetComponent<PlayerCtrl>().p_State = PlayerCtrl.PlayerState.Danger;

            }
            else if (DSG.detectSafetyHat == true && DSG.detectSafetyBelt == true)
            {
                // ���� �屸 ����� ���� ���·� ��ȯ
                player.GetComponent<PlayerCtrl>().p_State = PlayerCtrl.PlayerState.Safety;            
            }
        }      
    }
}
