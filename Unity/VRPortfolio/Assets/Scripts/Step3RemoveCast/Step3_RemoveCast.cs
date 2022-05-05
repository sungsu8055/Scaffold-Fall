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

        // �ȳ� ���� ���
        audioManager.PlayAudioOnce(startRemove);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("ControllerLeft") ||
            other.CompareTag("ControllerRight"))
        {
            // ���� ȿ���� ���
            audioManager.PlayAudioOnce(select);
            // OK ��ư �� ����̹��� ��Ȱ��ȭ
            instructionUI.transform.GetChild(2).gameObject.SetActive(false);
            instructionUI.transform.GetChild(0).gameObject.SetActive(false);

            // �۾� ���� �ȳ� ���� �� �۾� ���·� ��ȯ
            Invoke("StartRemove", 1.0f); 
            player.p_State = PlayerCtrl.PlayerState.Working;
        }        
    }

    public void StartRemove()
    {
        // ������ �۾� �ȳ� ���� ���
        audioManager.PlayAudioOnce(grabFormworkR);

        // UI�ȳ� ���� ����
        instructionText.text = "ǥ�õ� �κ��� ���������� �ָ��� ��� ��� �����Ͻʽÿ�.";

        ctrlGuideRight.SetActive(true);

        grabGuideIndicatorR.SetActive(true);
    }

    public void GrabFormworkL(Collider controller)
    {
        if(controller.CompareTag("ControllerLeft") && CIM.isGetGripL && !isGrabLeft)
        {
            ctrlGuideLeft.transform.GetChild(0).gameObject.SetActive(false);

            grabGuideIndicatorL.GetComponent<Image>().material = grab;

            instructionText.text = "����� �� ������ ���� ��� ��Ǫ���� ���ʽÿ�.";
            // ��Ǫ�� ���� �ȳ� ���
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

            instructionText.text = "ǥ�õ� �κ��� �޼����� �ָ��� ��� ��� �����Ͻʽÿ�.";
            // �޼� �׷� �ȳ� ���
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
                instructionText.text = "ǥ�õ� �κ��� �޼����� �ָ��� ��� ��� �����Ͻʽÿ�.";
                // �޼� �׷� �ȳ� ���
                audioManager.PlayAudioOnce(grabFormworkL);
            }

            isGrabRight = true;

            if (isGrabLeft && isGrabRight)
            {
                instructionText.text = "����� �������� ���� ��� ��Ǫ���� ���ʽÿ�.";
                // ��Ǫ�� ���� �ȳ� ���
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

            instructionText.text = "ǥ�õ� �κ��� ���������� �ָ��� ��� ��� �����Ͻʽÿ�.";
            // ������ �׷� �ȳ� ���
            audioManager.PlayAudioOnce(grabFormworkR);

            isGrabRight = false;
        }
    }

    public IEnumerator PullFormwork()
    {
        if (player.p_State == PlayerCtrl.PlayerState.Working)
        {
            formworkLoadingPlace.enabled = true;

            // ���� �ȳ� ���� ��� ��� �ð� ����, ���� ���� ��� ����
            instructionText.text = null;
            audioManager.source.clip = null;

            // ��Ʈ�� ���̵� UI ��Ȱ��
            grabGuideIndicatorL.SetActive(false);
            grabGuideIndicatorR.SetActive(false);

            ctrlGuideLeft.SetActive(false);
            ctrlGuideRight.SetActive(false);

            yield return new WaitForSeconds(1.0f);

            instructionText.text = "�� ������ ���� ���¸� �����ϸ� ���ʿ� ǥ�õ� ��ġ�� �����ֽʽÿ�.";
            // ��Ǫ�� ���� �ȳ� ���� ���
            audioManager.PlayAudioOnce(loadingFormwork);
        }
        else if (player.p_State == PlayerCtrl.PlayerState.Danger || player.p_State == PlayerCtrl.PlayerState.Safety)
        {
            // ���� ��� ���� �� ���� �ܰ踦 ���� UI ���� �ʱ�ȭ
            instructionUI.transform.GetChild(2).gameObject.SetActive(true);
            instructionUI.transform.GetChild(0).gameObject.SetActive(true);

            instructionUI.SetActive(false);
        }
    }

    public IEnumerator RemoveSecondFormwork()
    {
        yield return new WaitForSeconds(2.0f);

        instructionUI.transform.position = removeSecondFormworkUIPos.position;
        instructionText.text = "�ռ� ������ �۾��� ������ ������� ������� ��Ǫ���� ��ƴ�� �����Ͻʽÿ�.";
        // �ι�° ��Ǫ�� ���� �ȳ� ���� ���
        audioManager.PlayAudioOnce(removeSecondFormwork);
    }
}
