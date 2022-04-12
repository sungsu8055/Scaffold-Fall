using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerPullDetect_Right : MonoBehaviour
{
    public float originZPos;
    public float prevZPos;
    public bool pullObjectRight = false;

    float checkDelay;

    void Start()
    {
        
    }

    void Update()
    {
        checkDelay += Time.deltaTime;
        originZPos = this.transform.position.z;

        if (checkDelay >= 0.6f)
        {            
            prevZPos = originZPos;
            checkDelay = 0f;
        }

        StartCoroutine(PullingRight());

    }

    public IEnumerator PullingRight()
    {
        if ((prevZPos - originZPos) >= 0.3f)
        {
            Debug.Log("¿À¸¥¼Õ ´ç±è");
            pullObjectRight = true;
        }

        yield return new WaitForSeconds(1.0f);

        pullObjectRight = false;
    }
}
