using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquippedSafetyBelt : MonoBehaviour
{
    public Step3_RemoveCast RC;

    [Header("UI")]
    public GameObject fixInstructionUI;

    [Header("SafetyBelt")]
    public GameObject safetyBeltGuide;
    public GameObject safetyBelt;

    [Header("Controller")]
    public GameObject safetyBeltController;
    public GameObject originController;

    [Header("Audio")]
    public AudioManager audioManager;
    public AudioClip fixSafetyGear;
    public AudioClip fixedSound;

    public void StartSafetyBeltFix()
    {
        // 안전대 결착 안내 UI와 음성 출력
        fixInstructionUI.SetActive(true);
        audioManager.PlayAudioOnce(fixSafetyGear);

        // 안전대 결착 위치 가이드 출력 및
        // 기존 컨트롤러 비활성 후 안전대 결착 컨트롤러 활성
        safetyBeltGuide.SetActive(true);
        originController.SetActive(false);
        safetyBeltController.SetActive(true);
    }

    private void OnTriggerEnter(Collider other)
    {
        StartCoroutine(SafetyBeltFixed(other));
    }

    IEnumerator SafetyBeltFixed(Collider collider)
    {
        // 오른손 컨트롤러 감지 시 안전대 결착 완료 프로세스 실행
        if (collider.CompareTag("ControllerRight"))
        {
            // 안전대 결착 안내 UI 비활성 후 결착 사운드 출력
            fixInstructionUI.SetActive(false);
            audioManager.PlayAudioOnce(fixedSound);

            // 컨트롤러 모델 원복 후 안전대 모델 출력
            safetyBeltController.SetActive(false);
            originController.SetActive(true);
            safetyBeltGuide.SetActive(false);
            safetyBelt.SetActive(true);

            yield return new WaitForSeconds(1.5f);

            // 거푸집 제거 프로세스 시작
            RC.StartInstruction();
        }
    }
}
