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
    public AudioClip fixSafetyGear;
    public AudioClip fixedSound;

    public void StartSafetyBeltFix()
    {
        fixInstructionUI.SetActive(true);
        audioManager.PlayAudioOnce(fixSafetyGear);

        safetyBeltGuide.SetActive(true);
        originController.SetActive(false);
        safetyBeltController.SetActive(true);
    }

    private void OnTriggerEnter(Collider other)
    {
        /*/
        if (other.CompareTag("ControllerRight"))
        {
            fixInstructionUI.SetActive(false);
            audioManager.PlayAudioOnce(fixedSound);

            safetyBeltController.SetActive(false);
            originController.SetActive(true);
            safetyBeltGuide.SetActive(false);
            safetyBelt.SetActive(true);

            RC.StartInstruction();
        }
        //*/

        StartCoroutine(SafetyBeltFixed(other));

    }

    IEnumerator SafetyBeltFixed(Collider collider)
    {
        if (collider.CompareTag("ControllerRight"))
        {
            fixInstructionUI.SetActive(false);
            audioManager.PlayAudioOnce(fixedSound);

            safetyBeltController.SetActive(false);
            originController.SetActive(true);
            safetyBeltGuide.SetActive(false);
            safetyBelt.SetActive(true);

            yield return new WaitForSeconds(1.5f);

            RC.StartInstruction();
        }
    }
}
