using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class ControllerInputManager : MonoBehaviour
{
    // ��Ʈ�ѷ��� �׸� �� Ʈ���� ��ư �Է� ������ ���� ����
    // �޼�, ������ / ��ư �� �Է� ��� Ȯ�� Bool
    [Header("LeftHand")]
    public XRController LeftHand;
    public bool isGetGripL = false;
    public bool isGetTriggerL = false;

    [Header("RightHand")]
    public XRController RightHand;
    public bool isGetGripR = false;
    public bool isGetTriggerR = false;

    void Update()
    {
        // �޼�, ������ ����
        if (LeftHand)
        {
            // �� ��ư Ŭ�� �� ��ȯ �Ǵ� true ���� ������ ������ bool ������ ����
            LeftHand.inputDevice.TryGetFeatureValue(CommonUsages.gripButton, out isGetGripL);
            LeftHand.inputDevice.TryGetFeatureValue(CommonUsages.triggerButton, out isGetTriggerL);
        }
        if (RightHand)
        {
            RightHand.inputDevice.TryGetFeatureValue(CommonUsages.gripButton, out isGetGripR);
            RightHand.inputDevice.TryGetFeatureValue(CommonUsages.triggerButton, out isGetTriggerR);
        }
    }
}
