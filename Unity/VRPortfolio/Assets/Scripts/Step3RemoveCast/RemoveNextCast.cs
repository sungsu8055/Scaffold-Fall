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

    public bool nextCastPosComplete = false;

    void Start()
    {
        
    }

    void Update()
    {
        if (SKInteractor.hasSelection == true && nextCastPosComplete == false)
        {
            player.DOMove(nextCastPos.position, 1.0f).SetDelay(1.0f);
            RPI.restoreTrackOption = true;
            nextCastPosComplete = true;

            // ���� �屸 ������� ���� ���·� ��ȯ
            player.GetComponent<PlayerCtrl>().p_State = PlayerCtrl.PlayerState.Danger;
        }
    }
}
