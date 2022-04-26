using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerPullDetect_Right : MonoBehaviour
{
    public float originZPos;
    public float prevZPos;
    public bool pullObjectRight = false;

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
            pullObjectRight = true;
            Debug.Log("오른손 당김");
        }
        else if((prevZPos - originZPos) < 0.3f)
        {
            pullObjectRight = false;
            Debug.Log("오른손 대기");
        }
    }
}
