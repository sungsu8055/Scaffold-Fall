using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Step2_ClimbLadderCtrl : MonoBehaviour
{
    public GameObject climbInstruction;
    public Transform popupPos;

    public GameObject player;

    public GameObject approachGuide;

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

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("ControllerLeft") ||
            other.CompareTag("ControllerRight"))
        {
            ApproachLadder();
        }
    }

    public void ApproachLadder()
    {
        player.GetComponent<CharacterController>().enabled = true;

        climbInstruction.gameObject.SetActive(false);

        approachGuide.SetActive(true);
    }
}
