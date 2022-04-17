using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WearSafetyGear : MonoBehaviour
{
    public GameObject safetyHat;
    public GameObject safetyBelt;
    public Transform popupPos;
    public GameObject safetyGearInstruction;
    public DetectSafetyGear DSG;
    public CharacterController player;

    public GameObject approachLadderGuide;

    void Start()
    {
        StartCoroutine(StartSafetyProcedures());
    }

    void Update()
    {
        if(DSG.detectSafetyHat == true && DSG.detectSafetyBelt == true)
        {
            Debug.Log("안전 장구 착용 완료");
            approachLadderGuide.SetActive(true);
            player.enabled = true;
        }
    }

    IEnumerator StartSafetyProcedures()
    {
        safetyGearInstruction.SetActive(true);
        safetyGearInstruction.transform.SetPositionAndRotation(popupPos.position, popupPos.rotation);
        safetyHat.SetActive(true);
        safetyBelt.SetActive(true);

        yield return new WaitForSeconds(4.0f);

        safetyGearInstruction.SetActive(false);        
    }
}
