using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquippedSafetyBelt : MonoBehaviour
{
    public Step3_RemoveCast RC;

    [Header("UI")]
    public GameObject fixInstructionUI;

    [Header("SafetyBelt")]
    public GameObject safetyBeltGuide;
    public GameObject safetyBelt;

    [Header("Controller")]
    public GameObject safetyBeltController;
    public GameObject originController;

    [Header("Audio")]
    public AudioManager audioManager;

    public void StartSafetyBeltFix()
    {
        fixInstructionUI.SetActive(true);

        safetyBeltGuide.SetActive(true);
        originController.SetActive(false);
        safetyBeltController.SetActive(true);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("ControllerRight"))
        {
            fixInstructionUI.SetActive(false);

            safetyBeltController.SetActive(false);
            originController.SetActive(true);
            safetyBeltGuide.SetActive(false);
            safetyBelt.SetActive(true);

            RC.StartInstruction();
        }
    }
}
