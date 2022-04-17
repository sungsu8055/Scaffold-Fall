using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ScaffoldFall : MonoBehaviour
{
    public Transform spinningPos;
    public Transform fallPos;
    public ControllerPullDetect_Right CPDR;
    public PlayerCtrl player;
    public Transform popupPos;
    private Transform accidentDescriptionUI;

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
        accidentDescriptionUI = this.transform.GetChild(0);
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
    }

    void FallBehind()
    {
        player.transform.DOMove(spinningPos.position, 2.0f);
        player.transform.DOShakePosition(1.5f, axisShakeStrength, vibrato, randomness).SetDelay(0.5f);
        player.transform.DORotateQuaternion(spinningPos.rotation, 1.5f).SetDelay(1.8f);
        player.transform.DOMove(fallPos.position, 1.5f).SetEase(Ease.OutBounce).SetDelay(3.0f);
        player.transform.DORotateQuaternion(fallPos.rotation, 0.5f).SetDelay(3.5f);        
    }

    IEnumerator AccidentDescription()
    {
        yield return new WaitForSeconds(5.0f);

        accidentDescriptionUI.gameObject.SetActive(true);
        accidentDescriptionUI.SetPositionAndRotation(popupPos.position, popupPos.rotation);

        yield return new WaitForSeconds(5.0f);

        ResetExperienceConditions();
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

        // 트랙 복구 옵션 원복
        // RemovePlateInteractable에서 옵션 조정함

        // 2차 제거 위치 이동 체크 원복
        RNC.nextCastPosComplete = true;
        // UI 제거
        accidentDescriptionUI.gameObject.SetActive(false);
        // 플레이어 위치 원복
        player.transform.SetPositionAndRotation(startPosSF.position, startPosSF.rotation);
        // 안전장구 착용 단계 활성화
        wearSafetyGear.SetActive(true);
    }
}