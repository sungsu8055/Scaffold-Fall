﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnterExperienceZone : MonoBehaviour
{
    public Transform zoneEntryUI;
    public GameObject player;
    public Transform originPos;
    public Transform popupPos;

    public GameObject nextStep;

    [Header("Audio")]
    public AudioManager audioManager;
    public AudioClip entryZone;
    public AudioClip select;

    public void ZoneEntry()
    {
        // 체험 소개 및 진행 유무 선택 UI 출력
        zoneEntryUI.gameObject.SetActive(true);

        // 체험 존 소개 UI 위치를 Popup position 으로 재설정
        zoneEntryUI.SetPositionAndRotation(popupPos.position, popupPos.rotation);

        // 체험존 입장 안내 재생
        audioManager.PlayAudioOnce(entryZone);

        // UI 내용 확인 및 집중을 위해 이동 기능 정지(초기 위치 원복을 위해 함수 호출 시 CC가 활성화 상태이면 작동하지 않음)
        player.GetComponent<CharacterController>().enabled = false;
    }

    // 아래 Y/N 버튼 선택 기능은 추후 오큘러스 컨트롤러 터치 시 해당 버튼에서 충돌 체크 후 아래 함수 호출
    // 위와 같이 각 구역별 UI를 따로 식별해 작동 시키기 위함
    public void ExitZone()
    {
        // 초기 위치로 원복
        player.transform.SetPositionAndRotation(originPos.position, originPos.rotation);
        // Character Controller 활성
        player.GetComponent<CharacterController>().enabled = true;
        // 체험 소개 및 진행 유무 UI 비활성
        zoneEntryUI.gameObject.SetActive(false);
        // zone 진입 기능 재활성화를 위해 idle로 변경
        player.GetComponent<PlayerCtrl>().p_State = PlayerCtrl.PlayerState.Idle;
    }

    public void StartExperience(Transform startPos)
    {
        // Button - OnClick 옵션에서 포인터한 오브젝트를 파라미터로 받아 위치 수정 시 이용
        player.transform.SetPositionAndRotation(startPos.position, startPos.rotation);

        // 체험 소개 및 진행 유무 UI 비활성
        zoneEntryUI.gameObject.SetActive(false);

        // 선택 효과음 재생
        audioManager.PlayAudioOnce(select);

        nextStep.SetActive(true);
    }
}
