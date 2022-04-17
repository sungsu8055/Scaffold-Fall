using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerPullDetect_Right : MonoBehaviour
{
    public ControllerInputManager CIM;
    public float originZPos;
    public float prevZPos;
    public bool pullObjectRight = false;

    private float checkDelay;

    void Start()
    {
        
    }

    void FixedUpdate()
    {
        checkDelay += Time.deltaTime;
        originZPos = this.transform.position.z;

        if (checkDelay >= 0.6f)
        {            
            prevZPos = originZPos;
            checkDelay = 0f;
        }

        //StartCoroutine(PullingRight());

        if ((prevZPos - originZPos) >= 0.25f)
        {
            Debug.Log("¿À¸¥¼Õ ´ç±è");
            pullObjectRight = true;
        }

        StartCoroutine(PullingRight());
    }

    //*/
    public IEnumerator PullingRight()
    {
        if ((prevZPos - originZPos) >= 0.25f)
        {
            Debug.Log("¿À¸¥¼Õ ´ç±è");
            pullObjectRight = true;
        }

        yield return new WaitForSeconds(1.0f);

        pullObjectRight = false;
    }
    //*/
}
