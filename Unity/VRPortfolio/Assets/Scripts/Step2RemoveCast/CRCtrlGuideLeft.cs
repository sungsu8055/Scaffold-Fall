using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CRCtrlGuideLeft : MonoBehaviour
{
    public Step3_RemoveCast RC;

    void Start()
    {
        
    }

    void Update()
    {
        if (ARAVRInput.GetDown(ARAVRInput.Button.HandTrigger, ARAVRInput.Controller.LTouch))
        {
            RC.GrabCastLeft();
        }
    }

    /*/
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("GameController") && ARAVRInput.GetDown(ARAVRInput.Button.HandTrigger, ARAVRInput.Controller.LTouch))
        {
            RC.GrabCastLeft();
        }
    }
    //*/
}
