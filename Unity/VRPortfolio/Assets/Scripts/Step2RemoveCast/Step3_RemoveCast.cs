using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Step3_RemoveCast : MonoBehaviour
{
    public GameObject instructionUI;
    public Transform popupPos;

    public GameObject ctrlGuideLeft;
    public GameObject ctrlGuideRight;

    public Transform controllerLeft;
    public Transform controllerRight;

    bool isGrabLeft = false;
    bool isGrabRight = false;

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
        if (other.CompareTag("GameController"))
        {
            StartRemove();
        }        
    }

    public void StartRemove()
    {
        instructionUI.SetActive(false);

        ctrlGuideLeft.SetActive(true);
        ctrlGuideRight.SetActive(true);

    }

    public void GrabCastLeft()
    {
        controllerLeft.SetPositionAndRotation(ctrlGuideLeft.transform.position, ctrlGuideLeft.transform.rotation);
        controllerLeft.SetParent(ctrlGuideLeft.transform);

        ctrlGuideLeft.transform.GetChild(0).gameObject.SetActive(false);

        isGrabLeft = true;
    }

    public void GrabCastRight()
    {
        controllerRight.SetPositionAndRotation(ctrlGuideRight.transform.position, ctrlGuideRight.transform.rotation);
        controllerRight.SetParent(ctrlGuideRight.transform);

        ctrlGuideRight.transform.GetChild(0).gameObject.SetActive(false);

        isGrabRight = true;
    }

    
}
