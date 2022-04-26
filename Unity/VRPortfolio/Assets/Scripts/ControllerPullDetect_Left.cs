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

        if ((prevZPos - originZPos) >= 0.3f)
        {
            pullObjectLeft = true;
            Debug.Log("¿Þ¼Õ ´ç±è");
        }
        else if ((prevZPos - originZPos) < 0.3f)
        {
            pullObjectLeft = false;
            Debug.Log("¿Þ¼Õ ´ë±â");
        }
    }
}
