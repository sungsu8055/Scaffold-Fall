using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CRCtrlGuideRight : MonoBehaviour
{
    public Step3_RemoveCast RC;

    void Start()
    {

    }

    void Update()
    {
        if (ARAVRInput.GetDown(ARAVRInput.Button.IndexTrigger))
        {
            RC.GrabCastRight();
        }
    }

    /*/
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("GameController") && ARAVRInput.GetDown(ARAVRInput.Button.HandTrigger))
        {
            RC.GrabCastRight();
        }
    }
    //*/
}
