using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class InteractorTest : MonoBehaviour
{
    public InteractableTest interactable;
    public XRSocketInteractor interactor;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (interactor.hasSelection)
        {
            interactable.Destroy();
        }
    }
}
