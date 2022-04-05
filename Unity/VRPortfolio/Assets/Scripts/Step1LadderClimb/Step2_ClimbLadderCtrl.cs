using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Step2_ClimbLadderCtrl : MonoBehaviour
{
    public GameObject climbInstruction;
    public Transform popupPos;

    void Start()
    {
        StartCoroutine(StartClimbLadder());
    }

    IEnumerator StartClimbLadder()
    {
        yield return new WaitForSeconds(0.5f);

        climbInstruction.SetActive(true);

        climbInstruction.transform.SetPositionAndRotation(popupPos.position, popupPos.rotation);
    }
}
