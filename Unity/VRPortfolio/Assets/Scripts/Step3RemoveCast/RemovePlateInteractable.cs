using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class RemovePlateInteractable : XRGrabInteractable
{
    public ControllerPullDetect_Left CPDL;
    public ControllerPullDetect_Right CPDR;

    bool removed = false;

    void Start()
    {
        
    }

    void Update()
    {
        if(CPDR.pullObjectRight)
        {
            Debug.Log("°ÅÇªÁý Á¦°Å");
            this.trackPosition = true;
            this.trackRotation = true;

            removed = true;
        }
        else if(!CPDR.pullObjectRight && !removed)
        {
            Debug.Log("°ÅÇªÁý °íÁ¤");
            this.trackPosition = false;
            this.trackRotation = false;
        }
    }

    protected override void OnSelectEntered(XRBaseInteractor interactor)
    {
        base.OnSelectEntered(interactor);

        CPDL.PullingLeft();
        CPDR.PullingRight();
    }
}
