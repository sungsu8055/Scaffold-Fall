using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnterExperienceZone : MonoBehaviour
{
    public GameObject zoneEntryUI;
    public GameObject player;
    public Transform originPos;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void ZoneEntry(Transform enterUI)
    {
        // 체험 소개 및 진행 유무 선택 UI 출력
        enterUI.gameObject.SetActive(true);

        // UI 내용 확인 및 집중을 위해 이동 기능 정지(초기 위치 원복을 위해 함수 호출 시 CC가 활성화 상태이면 작동하지 않음)
        player.GetComponent<CharacterController>().enabled = false;
    }

    // 아래 Y/N 버튼 선택 기능은 추후 오큘러스 컨트롤러 터치 시 Other.gameobject.transform 값을 받아와 UI 활성 비활성 조정
    // 위와 같이 각 구역별 UI를 따로 식별해 작동 시키기 위함
    public void ExitZone()
    {
        // 초기 위치로 원복
        player.transform.SetPositionAndRotation(originPos.position, originPos.rotation);
        // Character Controller 활성
        player.GetComponent<CharacterController>().enabled = true;
        // 체험 소개 및 진행 유무 UI 비활성
        zoneEntryUI.SetActive(false);
        // zone 진입 기능 재활성화를 위해 idle로 변경
        player.GetComponent<PlayerCtrl>().p_State = PlayerCtrl.PlayerState.Idle;

        
    }

    public void StartExperience(Transform startPos)
    {
        // Button - OnClick 옵션에서 포인터한 오브젝트를 파라미터로 받아 위치 수정 시 이용
        player.transform.SetPositionAndRotation(startPos.position, startPos.rotation);

        // Character Controller 활성
        player.GetComponent<CharacterController>().enabled = true;
        // 체험 소개 및 진행 유무 UI 비활성
        zoneEntryUI.SetActive(false);
    }
}
