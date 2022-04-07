using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectEntryOption : MonoBehaviour
{
    public EnterExperienceZone enterOptionCtrl;
    public Transform startPos;
    public GameObject step2;

    void Start()
    {
        // 각 체험존별 스텝2를 구별하기 위해 개별 UI 선에서 접근하여 변수 지정
        enterOptionCtrl.nextStep = step2;
    }

    private void OnTriggerEnter(Collider other)
    {
        // 옵션 버튼 단에서 충돌 체크 후 태그를 통해 옵션 종류 및 컨트롤러 유무를 체크
        if (gameObject.CompareTag("OptionY") && 
            (other.CompareTag("ControllerLeft") || 
            other.CompareTag("ControllerRight")))
        {
            enterOptionCtrl.StartExperience(startPos);
        }
        else if (gameObject.CompareTag("OptionN") && 
            (other.CompareTag("ControllerLeft") ||
            other.CompareTag("ControllerRight")))
        {
            enterOptionCtrl.ExitZone();
        }
    }
}
