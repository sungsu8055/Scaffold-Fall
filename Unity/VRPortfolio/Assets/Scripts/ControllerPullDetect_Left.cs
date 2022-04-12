using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerPullDetect_Left : MonoBehaviour
{
    public float originZPos;
    public float prevZPos;
    public bool pullObjectLeft = false;

    float checkDelay;

    void Start()
    {
        
    }

    void Update()
    {
        checkDelay += Time.deltaTime;
        originZPos = this.transform.position.z;

        if (checkDelay >= 0.8f)
        {            
            prevZPos = originZPos;
            checkDelay = 0f;
        }

       // StartCoroutine(PullingLeft());

    }

    public IEnumerator PullingLeft()
    {
        if ((prevZPos - originZPos) >= 0.3f)
        {
            Debug.Log("¿Þ¼Õ ´ç±è");
            pullObjectLeft = true;
        }


        yield return new WaitForSeconds(1.0f);

        pullObjectLeft = false;
    }
}
