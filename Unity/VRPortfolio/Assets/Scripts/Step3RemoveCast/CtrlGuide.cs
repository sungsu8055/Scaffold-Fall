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
            RC.GrabCastL(other);
        }
        else
        {
            RC.GrabCastR(other);
        }
        
    }
}
