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

    private bool wearingSafetyGearComplete = false;

    void Start()
    {
        StartCoroutine(StartSafetyProcedures());
    }

    void Update()
    {
        if(DSG.detectSafetyHat == true && DSG.detectSafetyBelt == true && !wearingSafetyGearComplete)
        {
            Debug.Log("���� �屸 ���� �Ϸ�");
            approachLadderGuide.SetActive(true);
            player.enabled = true;
            wearingSafetyGearComplete = true;
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
