using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class WearSafetyGear : MonoBehaviour
{
    public GameObject safetyHat;
    public GameObject safetyBelt;
    public Transform popupPos;
    public GameObject safetyGearInstruction;
    public DetectSafetyGear DSG;
    public Step2_ClimbLadderCtrl CC;

    private bool wearingSafetyGearComplete = false;

    [Header("Audio")]
    public AudioManager audioManager;
    public AudioClip wearSafetyGear;

    void Start()
    {
        StartCoroutine(StartSafetyProcedures());
    }

    void Update()
    {
        if(DSG.detectSafetyHat == true && DSG.detectSafetyBelt == true && !wearingSafetyGearComplete)
        {
            Debug.Log("안전 장구 착용 완료");
            CC.StartClimbLadder();
            wearingSafetyGearComplete = true;
        }
    }

    IEnumerator StartSafetyProcedures()
    {
        safetyGearInstruction.SetActive(true);
        safetyGearInstruction.transform.SetPositionAndRotation(popupPos.position, popupPos.rotation);
        audioManager.PlayAudioOnce(wearSafetyGear);

        safetyHat.SetActive(true);
        safetyBelt.SetActive(true);

        yield return new WaitForSeconds(6.0f);

        safetyHat.transform.GetComponent<XRGrabInteractable>().enabled = true;
        safetyBelt.transform.GetComponent<XRGrabInteractable>().enabled = true;

        safetyGearInstruction.SetActive(false);        
    }
}
