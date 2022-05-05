using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Step3_RemoveCast : MonoBehaviour
{
    public ControllerInputManager CIM;
    public PlayerCtrl player;

    public GameObject instructionUI;
    public Text instructionText;
    public Transform popupPos;
    public Transform removeSecondFormworkUIPos;
    public Vector3 instructionUIOriginPos;

    public GameObject ctrlGuideLeft;
    public GameObject ctrlGuideRight;
    public GameObject grabGuideIndicatorL;
    public GameObject grabGuideIndicatorR;
    public Material grab;
    public Material ungrab;
    public MeshRenderer formworkLoadingPlace;
    Color originColor;

    public bool isGrabLeft = false;
    public bool isGrabRight = false;

    [Header("Audio")]
    public AudioManager audioManager;
    public AudioClip startRemove;
    public AudioClip select;
    public AudioClip grabFormworkR;
    public AudioClip grabFormworkL;
    public AudioClip formworkRemove;
    public AudioClip loadingFormwork;
    public AudioClip removeSecondFormwork;

    private void Start()
    {
        originColor = ctrlGuideLeft.GetComponentInChildren<SkinnedMeshRenderer>().material.color;
        instructionText = instructionUI.transform.GetChild(1).GetComponent<Text>();
        instructionUIOriginPos = instructionUI.transform.position;
    }

    public void StartInstruction()
    {
        instructionUI.SetActive(true);

        // 안내 음성 재생
        audioManager.PlayAudioOnce(startRemove);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("ControllerLeft") ||
            other.CompareTag("ControllerRight"))
        {
            // 선택 효과음 재생
            audioManager.PlayAudioOnce(select);
            // OK 버튼 및 배경이미지 비활성화
            instructionUI.transform.GetChild(2).gameObject.SetActive(false);
            instructionUI.transform.GetChild(0).gameObject.SetActive(false);

            // 작업 절차 안내 실행 및 작업 상태로 전환
            Invoke("StartRemove", 1.0f); 
            player.p_State = PlayerCtrl.PlayerState.Working;
        }        
    }

    public void StartRemove()
    {
        // 오른손 작업 안내 음성 재생
        audioManager.PlayAudioOnce(grabFormworkR);

        // UI안내 문구 수정
        instructionText.text = "표시된 부분을 오른손으로 주먹을 쥐어 잡고 유지하십시오.";

        ctrlGuideRight.SetActive(true);

        grabGuideIndicatorR.SetActive(true);
    }

    public void GrabFormworkL(Collider controller)
    {
        if(controller.CompareTag("ControllerLeft") && CIM.isGetGripL && !isGrabLeft)
        {
            ctrlGuideLeft.transform.GetChild(0).gameObject.SetActive(false);

            grabGuideIndicatorL.GetComponent<Image>().material = grab;

            instructionText.text = "양손을 몸 쪽으로 세게 당겨 거푸집을 뜯어내십시오.";
            // 거푸집 제거 안내 재생
            audioManager.PlayAudioOnce(formworkRemove);

            isGrabLeft = true;
        }
    }

    public void UngrabFormworkL(Collider controller)
    {
        if (controller.gameObject.name != "LeftHandCollider" || !CIM.isGetGripL)
        {
            ctrlGuideLeft.transform.GetChild(0).gameObject.SetActive(true);

            grabGuideIndicatorL.GetComponent<Image>().material = ungrab;

            instructionText.text = "표시된 부분을 왼손으로 주먹을 쥐어 잡고 유지하십시오.";
            // 왼손 그랩 안내 재생
            audioManager.PlayAudioOnce(grabFormworkL);

            isGrabLeft = false;
        }
    }

    public void GrabFormworkR(Collider controller)
    {
        if (controller.CompareTag("ControllerRight") && CIM.isGetGripR && !isGrabRight)
        {
            ctrlGuideRight.transform.GetChild(0).gameObject.SetActive(false);

            grabGuideIndicatorR.GetComponent<Image>().material = grab;

            ctrlGuideLeft.SetActive(true);
            grabGuideIndicatorL.SetActive(true);

            if (!isGrabRight && !isGrabLeft)
            {
                instructionText.text = "표시된 부분을 왼손으로 주먹을 쥐어 잡고 유지하십시오.";
                // 왼손 그랩 안내 재생
                audioManager.PlayAudioOnce(grabFormworkL);
            }

            isGrabRight = true;

            if (isGrabLeft && isGrabRight)
            {
                instructionText.text = "양손을 몸쪽으로 세게 당겨 거푸집을 뜯어내십시오.";
                // 거푸집 제거 안내 재생
                audioManager.PlayAudioOnce(formworkRemove);
            }
        }
    }

    public void UngrabFormworkR(Collider controller)
    {
        if (controller.gameObject.name != "RightHandCollider" || !CIM.isGetGripR)
        {
            ctrlGuideRight.transform.GetChild(0).gameObject.SetActive(true);

            grabGuideIndicatorR.GetComponent<Image>().material = ungrab;

            if (!isGrabLeft)
            {
                ctrlGuideLeft.SetActive(false);
                grabGuideIndicatorL.SetActive(false);
            }

            instructionText.text = "표시된 부분을 오른손으로 주먹을 쥐어 잡고 유지하십시오.";
            // 오른손 그랩 안내 재생
            audioManager.PlayAudioOnce(grabFormworkR);

            isGrabRight = false;
        }
    }

    public IEnumerator PullFormwork()
    {
        if (player.p_State == PlayerCtrl.PlayerState.Working)
        {
            formworkLoadingPlace.enabled = true;

            // 다음 안내 음성 출력 대기 시간 동안, 이전 음성 출력 방지
            instructionText.text = null;
            audioManager.source.clip = null;

            // 컨트롤 가이드 UI 비활성
            grabGuideIndicatorL.SetActive(false);
            grabGuideIndicatorR.SetActive(false);

            ctrlGuideLeft.SetActive(false);
            ctrlGuideRight.SetActive(false);

            yield return new WaitForSeconds(1.0f);

            instructionText.text = "두 손으로 잡은 상태를 유지하며 왼쪽에 표시된 위치에 놓아주십시오.";
            // 거푸집 적재 안내 음성 재생
            audioManager.PlayAudioOnce(loadingFormwork);
        }
        else if (player.p_State == PlayerCtrl.PlayerState.Danger || player.p_State == PlayerCtrl.PlayerState.Safety)
        {
            // 안전 장비 착용 후 제거 단계를 위해 UI 상태 초기화
            instructionUI.transform.GetChild(2).gameObject.SetActive(true);
            instructionUI.transform.GetChild(0).gameObject.SetActive(true);

            instructionUI.SetActive(false);
        }
    }

    public IEnumerator RemoveSecondFormwork()
    {
        yield return new WaitForSeconds(2.0f);

        instructionUI.transform.position = removeSecondFormworkUIPos.position;
        instructionText.text = "앞서 진행한 작업과 동일한 방법으로 양손으로 거푸집을 잡아당겨 제거하십시오.";
        // 두번째 거푸집 제거 안내 음성 재생
        audioManager.PlayAudioOnce(removeSecondFormwork);
    }
}
