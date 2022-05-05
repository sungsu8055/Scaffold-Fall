using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerPullDetect_Left : MonoBehaviour
{
    public float originZPos;
    public float prevZPos;
    public bool pullObjectLeft = false;

    private float checkDelay;

    void FixedUpdate()
    {
        checkDelay += Time.deltaTime;
        originZPos = this.transform.position.z;

        if (checkDelay >= 0.6f)
        {            
            prevZPos = originZPos;
            checkDelay = 0f;
        }

        if ((prevZPos - originZPos) >= 0.25f)
        {
            pullObjectLeft = true;
            Debug.Log("�޼� ���");
        }
        else if ((prevZPos - originZPos) < 0.25f)
        {
            pullObjectLeft = false;
            Debug.Log("�޼� ���");
        }
    }
}
