using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class ScaffoldFall : MonoBehaviour
{
    public Transform spinningPos;
    public Transform fallPos;
    public Transform hangingPos;
    public ControllerPullDetect_Right CPDR;
    public PlayerCtrl player;
    public Transform popupPos;
    private Transform accidentDescriptionCV;
    public Transform accidentDescriptionUI;
    public Transform accidentPreventionDescriptionUI;

    [Header("ResetExperienceConditions")]
    public Transform cast1;
    public Transform cast2;
    public Transform cast1Safe;
    public Transform cast2Safe;
    public RemoveNextCast RNC;
    public Transform startPosSF;
    public GameObject wearSafetyGear;

    [Header("FallAnimationParameter")]
    public Vector3 axisShakeStrength;
    public int vibrato;
    public float randomness;

    private bool isFellDown = false;

    void Start()
    {
        accidentDescriptionCV = this.transform.GetChild(0);
    }

    void Update()
    {
        if (player.p_State == PlayerCtrl.PlayerState.Danger)
        {
            if (CPDR.pullObjectRight)
            {
                if (!isFellDown)
                {
                    FallBehind();

                    StartCoroutine(AccidentDescription());

                    isFellDown = true;
                }
            }
        }
        else if (player.p_State == PlayerCtrl.PlayerState.Safety)
        {
            if (CPDR.pullObjectRight)
            {
                if (!isFellDown)
                {
                    FallSafely();

                    isFellDown = true;

                    StartCoroutine(AccidentPreventionDescription());
                }
            }
        }
    }

    void FallBehind()
    {
        player.transform.DOMove(spinningPos.position, 2.0f);
        player.transform.DOShakePosition(1.5f, axisShakeStrength, vibrato, randomness).SetDelay(0.5f);
        player.transform.DORotateQuaternion(spinningPos.rotation, 1.5f).SetDelay(1.8f);
        player.transform.DOMove(fallPos.position, 1.5f).SetEase(Ease.OutBounce).SetDelay(3.0f);
        player.transform.DORotateQuaternion(fallPos.rotation, 0.5f).SetDelay(3.5f);        
    }

    void FallSafely()
    {
        player.transform.DOMove(spinningPos.position, 2.0f);
        player.transform.DOShakePosition(1.5f, axisShakeStrength, vibrato, randomness).SetDelay(0.5f);
        player.transform.DORotateQuaternion(spinningPos.rotation, 1.5f).SetDelay(1.8f);
        player.transform.DOMove(hangingPos.position, 1.0f).SetDelay(3.0f);
        player.transform.DORotateQuaternion(hangingPos.rotation, 0.5f).SetDelay(3.5f);
        player.transform.DOShakePosition(7.0f, 0.4f, 2, 0.0f).SetDelay(3.9f);
    }

    IEnumerator AccidentDescription()
    {
        yield return new WaitForSeconds(5.0f);

        accidentDescriptionCV.gameObject.SetActive(true);
        accidentDescriptionCV.SetPositionAndRotation(popupPos.position, popupPos.rotation);
        accidentDescriptionUI.gameObject.SetActive(true);

        yield return new WaitForSeconds(5.0f);

        ResetExperienceConditions();
    }

    IEnumerator AccidentPreventionDescription()
    {
        yield return new WaitForSeconds(11.0f);

        accidentDescriptionCV.gameObject.SetActive(true);
        accidentDescriptionCV.SetPositionAndRotation(popupPos.position, popupPos.rotation);
        accidentPreventionDescriptionUI.gameObject.SetActive(true);

        yield return new WaitForSeconds(6.0f);

        SceneManager.LoadScene(0);
    }


    void ResetExperienceConditions()
    {
        // 플레이어 상태 변경
        player.p_State = PlayerCtrl.PlayerState.EnterZone;
        // 거푸집 위치 원복
        cast1.GetComponent<RemovePlateInteractable>().Destroy();
        cast2.GetComponent<RemovePlateInteractable>().Destroy();

        cast1Safe.transform.gameObject.SetActive(true);
        cast2Safe.transform.gameObject.SetActive(true);
        Debug.Log("거푸집 위치 원복 진행");

        isFellDown = false;

        // 2차 제거 위치 이동 체크 원복
        RNC.nextCastPosComplete = false;
        // UI 제거
        accidentDescriptionUI.gameObject.SetActive(false);
        accidentDescriptionCV.gameObject.SetActive(false);
        // 플레이어 위치 원복
        player.transform.SetPositionAndRotation(startPosSF.position, startPosSF.rotation);
        // 안전장구 착용 단계 활성화
        wearSafetyGear.SetActive(true);
    }
}