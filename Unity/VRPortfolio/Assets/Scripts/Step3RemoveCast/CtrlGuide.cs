using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CtrlGuide : MonoBehaviour
{
    public Step3_RemoveCast RC;

    private void OnTriggerStay(Collider other)
    {
        if (this.transform.name.Contains("Left"))
        {
            RC.GrabFormworkL(other);
        }
        else
        {
            RC.GrabFormworkR(other);
        }        
    }

    private void OnTriggerExit(Collider other)
    {
        if (this.transform.name.Contains("Left"))
        {
            RC.UngrabFormworkL(other);
        }
        else
        {
            RC.UngrabFormworkR(other);
        }
    }
}
