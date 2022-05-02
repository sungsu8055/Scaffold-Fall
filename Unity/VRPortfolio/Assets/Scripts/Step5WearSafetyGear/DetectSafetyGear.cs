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

    void Update()
    {
        if (safetyHat.firstInteractableSelected.transform.CompareTag("SafetyHat"))
        {
            detectSafetyHat = true;
        }

        if (safetyBelt.firstInteractableSelected.transform.CompareTag("SafetyBelt"))
        {
            detectSafetyBelt = true;
        }
    }
}
