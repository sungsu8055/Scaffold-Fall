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
        // OK ��ư �� ����̹��� ��Ȱ��ȭ
        instructionUI.transform.GetChild(2).gameObject.SetActive(false);
        instructionUI.transform.GetChild(0).gameObject.SetActive(false);

        // UI�ȳ� ���� ����
        instructionText.text = "ǥ�õ� �κ��� ���������� �ָ��� ��� ��� �����Ͻʽÿ�.";

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

            //instructionText.text = "����� �������� ���� ��� ��Ǫ���� ���ʽÿ�.";
            //Debug.Log(controller.gameObject.name);

            // player.p_State = PlayerCtrl.PlayerState.WorkingProgress;
            
            instructionText.text = "����� �������� ���� ��� ��Ǫ���� ���ʽÿ�.";

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

            instructionText.text = "ǥ�õ� �κ��� �޼����� �ָ��� ��� ��� �����Ͻʽÿ�.";

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

            if (!isGrabRight && !isGrabLeft)
            {
                instructionText.text = "ǥ�õ� �κ��� �޼����� �ָ��� ��� ��� �����Ͻʽÿ�.";
            }
            else if (isGrabLeft && isGrabRight)
            {
                instructionText.text = "����� �������� ���� ��� ��Ǫ���� ���ʽÿ�.";
            }

            isGrabRight = true;
        }
        //*/
        else if (controller.gameObject.name != "RightHandCollider" || controller == null || !CIM.isGetGripR)
        {
            ctrlGuideRight.transform.GetChild(0).gameObject.SetActive(true);
            //ctrlGuideRight.transform.GetComponentInChildren<SkinnedMeshRenderer>().material.color = originColor;

            grabGuideIndicatorR.GetComponent<Image>().material = ungrab;

            //controller.transform.GetChild(0).gameObject.SetActive(true);
            if (!isGrabLeft)
            {
                ctrlGuideLeft.SetActive(false);
                grabGuideIndicatorL.SetActive(false);
            }
            
            instructionText.text = "ǥ�õ� �κ��� ���������� �ָ��� ��� ��� �����Ͻʽÿ�.";

            isGrabRight = false;
        }
        //*/
    }

    public void PullFormwork()
    {
        if(player.p_State == PlayerCtrl.PlayerState.Working)
        {
            formworkLoadingPlace.enabled = true;

            instructionText.text = "�� ������ ���� ���·� ���ʿ� ǥ�õ� ��ġ�� �����ֽʽÿ�.";

            // ��Ʈ�� ���̵� UI ��Ȱ��
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
        instructionText.text = "�ռ� ������ �۾��� �����ϰ� ������� ��Ǫ���� ��� ��� �����Ͻʽÿ�.";
    }
}
