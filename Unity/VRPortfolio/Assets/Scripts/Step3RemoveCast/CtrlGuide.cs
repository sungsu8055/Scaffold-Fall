using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CtrlGuide : MonoBehaviour
{
    public Step3_RemoveCast RC;

    private void OnTriggerStay(Collider other)
    {
        // Debug.Log(other.gameObject.name);

        RC.GrabCast(other);
    }
}
