using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class DetectSafetyGear : MonoBehaviour
{
    public XRSocketInteractor safetyHat;
    public XRSocketInteractor safetyBelt;
    public bool detectSafetyHat = false;
    public bool detectSafetyBelt = false;

    void Start()
    {
        
    }

    void Update()
    {
        /*/
        if (safetyHat.interactablesSelected[0].transform.CompareTag("SafetyHat"))
        {
            Debug.Log(safetyHat.interactablesSelected[0].transform.name);
            detectSafetyHat = true;
        }
        else if (safetyBelt.interactablesSelected[0].transform.CompareTag("SafetyBelt"))
        {
            Debug.Log(safetyBelt.interactablesSelected[0].transform.name);
            detectSafetyBelt = true;
        }
        //*/

        if (safetyHat.firstInteractableSelected.transform.CompareTag("SafetyHat"))
        {
            Debug.Log(safetyHat.interactablesSelected[0].transform.name);
            detectSafetyHat = true;
        }
        if (safetyBelt.firstInteractableSelected.transform.CompareTag("SafetyBelt"))
        {
            Debug.Log(safetyBelt.interactablesSelected[0].transform.name);
            detectSafetyBelt = true;
        }
    }
}
