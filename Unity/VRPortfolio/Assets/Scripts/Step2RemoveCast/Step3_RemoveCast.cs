using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Step3_RemoveCast : MonoBehaviour
{
    public ControllerInputManager CIM;

    public GameObject instructionUI;
    public Transform popupPos;

    public GameObject ctrlGuideLeft;
    public GameObject ctrlGuideRight;

    public Transform grabGuideIndicatorL;
    public Transform grabGuideIndicatorR;
    public Material grab;
    public Material ungrab;

    public bool isGrabLeft = false;
    public bool isGrabRight = false;

    void Start()
    {
        
    }

    void Update()
    {
        
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
            ctrlGuideLeft.transform.GetChild(0).gameObject.SetActive(false);

            grabGuideIndicatorL.gameObject.GetComponent<Image>().material = ungrab;

            isGrabLeft = true;
        }
        //*/
        else if (controller.gameObject.name != "LeftHandCollider" && !CIM.isGetGripL)
        {
            ctrlGuideLeft.transform.GetChild(0).gameObject.SetActive(true);

            grabGuideIndicatorL.gameObject.GetComponent<Image>().material = grab;

            isGrabLeft = false;
        }
        //*/

        if (controller.CompareTag("ControllerRight") && CIM.isGetGripR)
        {
            ctrlGuideRight.transform.GetChild(0).gameObject.SetActive(false);

            grabGuideIndicatorR.gameObject.GetComponent<Image>().material = ungrab;


            isGrabRight = true;
        }
        //*/
        else if (controller.gameObject.name != "RightHandCollider" && !CIM.isGetGripR)
        {
            ctrlGuideRight.transform.GetChild(0).gameObject.SetActive(true);

            grabGuideIndicatorR.gameObject.GetComponent<Image>().material = grab;

            isGrabRight = false;
        }
        //*/
    }

    public void RemoveCast()
    {

    }
}
