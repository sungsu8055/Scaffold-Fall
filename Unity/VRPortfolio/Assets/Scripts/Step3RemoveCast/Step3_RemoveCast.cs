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
    Color originColor;

    public bool isGrabLeft = false;
    public bool isGrabRight = false;

    private void Start()
    {
        originColor = ctrlGuideLeft.GetComponentInChildren<SkinnedMeshRenderer>().material.color;
        instructionText = instructionUI.transform.GetChild(1).GetComponent<Text>();
        instructionUIOriginPos = instructionUI.transform.position;
    }

    public void StartInstruction()
    {
        instructionUI.SetActive(true);

        // instructionUI.transform.position = popupPos.position;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("ControllerLeft") ||
            other.CompareTag("ControllerRight"))
        {
            StartRemove();
            player.p_State = PlayerCtrl.PlayerState.Working;
        }        
    }

    public void StartRemove()
    {
        //instructionUI.SetActive(false);
        // OK 버튼 및 배경이미지 비활성화
        instructionUI.transform.GetChild(2).gameObject.SetActive(false);
        instructionUI.transform.GetChild(0).gameObject.SetActive(false);

        // UI안내 문구 수정
        instructionText.text = "표시된 부분을 오른손으로 주먹을 쥐어 잡고 유지하십시오.";

        // ctrlGuideLeft.SetActive(true);
        ctrlGuideRight.SetActive(true);

        // grabGuideIndicatorL.gameObject.SetActive(true);
        grabGuideIndicatorR.SetActive(true);
    }

    public void GrabFormworkL(Collider controller)
    {
        if(controller.CompareTag("ControllerLeft") && CIM.isGetGripL)
        {
            ctrlGuideLeft.transform.GetChild(0).gameObject.SetActive(false);
            //ctrlGuideLeft.transform.GetComponentInChildren<SkinnedMeshRenderer>().material.color = Color.white;

            grabGuideIndicatorL.GetComponent<Image>().material = grab;

            instructionText.text = "양손을 몸쪽으로 세게 당겨 거푸집을 뜯어내십시오.";
            //Debug.Log(controller.gameObject.name);

            // player.p_State = PlayerCtrl.PlayerState.WorkingProgress;

            isGrabLeft = true;  
        }
        //*/
        else if (controller.gameObject.name != "LeftHandCollider" || controller == null || !CIM.isGetGripL)
        {
            ctrlGuideLeft.transform.GetChild(0).gameObject.SetActive(true);
            //ctrlGuideLeft.transform.GetComponentInChildren<SkinnedMeshRenderer>().material.color = originColor;

            grabGuideIndicatorL.GetComponent<Image>().material = ungrab;

            //controller.transform.GetChild(0).gameObject.SetActive(true);

            // player.p_State = PlayerCtrl.PlayerState.EnterZone;

            isGrabLeft = false;
        }
        //*/
    }

    public void GrabFormworkR(Collider controller)
    {
        if (controller.CompareTag("ControllerRight") && CIM.isGetGripR)
        {
            ctrlGuideRight.transform.GetChild(0).gameObject.SetActive(false);
            //ctrlGuideRight.transform.GetComponentInChildren<SkinnedMeshRenderer>().material.color = Color.white;

            grabGuideIndicatorR.GetComponent<Image>().material = grab;

            //controller.transform.GetChild(0).gameObject.SetActive(false);
            ctrlGuideLeft.SetActive(true);
            grabGuideIndicatorL.SetActive(true);

            instructionText.text = "표시된 부분을 왼손으로 주먹을 쥐어 잡고 유지하십시오.";

            isGrabRight = true;
        }
        //*/
        else if (controller.gameObject.name != "RightHandCollider" || controller == null || !CIM.isGetGripR)
        {
            ctrlGuideRight.transform.GetChild(0).gameObject.SetActive(true);
            //ctrlGuideRight.transform.GetComponentInChildren<SkinnedMeshRenderer>().material.color = originColor;

            grabGuideIndicatorR.GetComponent<Image>().material = ungrab;

            //controller.transform.GetChild(0).gameObject.SetActive(true);
            ctrlGuideLeft.SetActive(false);
            grabGuideIndicatorL.SetActive(false);

            isGrabRight = false;
        }
        //*/
    }

    public void PullFormwork()
    {
        if(player.p_State == PlayerCtrl.PlayerState.Working)
        {
            instructionText.text = "두 손으로 잡은 상태로 왼쪽에 표시된 위치에 놓아주십시오.";

            // 컨트롤 가이드 UI 비활성
            grabGuideIndicatorL.SetActive(false);
            grabGuideIndicatorR.SetActive(false);

            ctrlGuideLeft.SetActive(false);
            ctrlGuideRight.SetActive(false);
        }
        else if(player.p_State == PlayerCtrl.PlayerState.Danger || player.p_State == PlayerCtrl.PlayerState.Safety)
        {
            instructionUI.transform.GetChild(2).gameObject.SetActive(true);
            instructionUI.transform.GetChild(0).gameObject.SetActive(true);

            instructionUI.SetActive(false);
        }
    }

    public void RemoveSecondFormwork()
    {
        instructionUI.transform.position = removeSecondFormworkUIPos.position;
        instructionText.text = "앞서 진행한 작업과 동일하게 양손으로 거푸집을 잡아 당겨 제거하십시오.";
    }
}
