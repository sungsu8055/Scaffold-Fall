using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StepProgress : MonoBehaviour
{
    public GameObject player;

    private void OnTriggerEnter(Collider other)
    {
        if (gameObject.CompareTag("OptionY") && other.CompareTag("GameController"))
        {
            Progress();
        }
    }

    public void Progress()
    {
        player.GetComponent<CharacterController>().enabled = true;

        Transform instructionUI = this.gameObject.transform.parent;

        instructionUI.gameObject.SetActive(false);
    }
}
