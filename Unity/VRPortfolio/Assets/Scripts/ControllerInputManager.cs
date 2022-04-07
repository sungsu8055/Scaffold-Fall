using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class ControllerInputManager : MonoBehaviour
{
    // 컨트롤러의 그립 및 트리거 버튼 입력 감지를 위한 변수
    // 왼손, 오른손 / 버튼 별 입력 요소 확인 Bool
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
        // 왼손, 오른손 구별
        if (LeftHand)
        {
            // 각 버튼 클릭 시 반환 되는 true 값을 위에서 선언한 bool 변수에 저장
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
