using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Step3_RemoveCast : MonoBehaviour
{
    public ControllerInputManager CIM;
    public PlayerCtrl player;

    public GameObject instructionUI;
    public Transform popupPos;

    public GameObject ctrlGuideLeft;
    public GameObject ctrlGuideRight;

    public Transform grabGuideIndicatorL;
    public Transform grabGuideIndicatorR;
    public Material grab;
    public Material ungrab;
    Color originColor;

    public bool isGrabLeft = false;
    public bool isGrabRight = false;

    private void Start()
    {
        originColor = ctrlGuideLeft.GetComponentInChildren<SkinnedMeshRenderer>().material.color;
    }

    public void StartInstruction()
    {
        instructionUI.SetActive(true);

        instructionUI.transform.position = popupPos.position;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("ControllerLeft") ||
            other.CompareTag("ControllerRight"))
        {
            StartRemove();
        }        
    }

    public void StartRemove()
    {
        instructionUI.SetActive(false);

        ctrlGuideLeft.SetActive(true);
        ctrlGuideRight.SetActive(true);

        grabGuideIndicatorL.gameObject.SetActive(true);
        grabGuideIndicatorR.gameObject.SetActive(true);
    }

    public void GrabCast(Collider controller)
    {
        if(controller.CompareTag("ControllerLeft") && CIM.isGetGripL)
        {
            //ctrlGuideLeft.transform.GetChild(0).gameObject.SetActive(false);
            ctrlGuideLeft.transform.GetComponentInChildren<SkinnedMeshRenderer>().material.color = Color.white;

            grabGuideIndicatorL.gameObject.GetComponent<Image>().material = grab;

            Debug.Log(controller.gameObject.name);

            player.p_State = PlayerCtrl.PlayerState.WorkingProgress;

            isGrabLeft = true;  
        }
        //*/
        else if (controller.gameObject.name != "LeftHandCollider" && !CIM.isGetGripL)
        {
            //ctrlGuideLeft.transform.GetChild(0).gameObject.SetActive(true);
            ctrlGuideLeft.transform.GetComponentInChildren<SkinnedMeshRenderer>().material.color = originColor;

            grabGuideIndicatorL.gameObject.GetComponent<Image>().material = ungrab;

            //controller.transform.GetChild(0).gameObject.SetActive(true);

            player.p_State = PlayerCtrl.PlayerState.EnterZone;

            isGrabLeft = false;
        }
        //*/

        if (controller.CompareTag("ControllerRight") && CIM.isGetGripR)
        {
            //ctrlGuideRight.transform.GetChild(0).gameObject.SetActive(false);
            ctrlGuideRight.transform.GetComponentInChildren<SkinnedMeshRenderer>().material.color = Color.white;

            grabGuideIndicatorR.gameObject.GetComponent<Image>().material = grab;

            //controller.transform.GetChild(0).gameObject.SetActive(false);

            isGrabRight = true;
        }
        //*/
        else if (controller.gameObject.name != "RightHandCollider" && !CIM.isGetGripR)
        {
            //ctrlGuideRight.transform.GetChild(0).gameObject.SetActive(true);
            ctrlGuideRight.transform.GetComponentInChildren<SkinnedMeshRenderer>().material.color = originColor;

            grabGuideIndicatorR.gameObject.GetComponent<Image>().material = ungrab;

            //controller.transform.GetChild(0).gameObject.SetActive(true);

            isGrabRight = false;
        }
        //*/
    }

    public void RemoveCast()
    {

    }
}
