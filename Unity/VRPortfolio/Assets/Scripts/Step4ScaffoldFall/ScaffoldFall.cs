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
        // �÷��̾� ���� ����
        player.p_State = PlayerCtrl.PlayerState.EnterZone;
        // ��Ǫ�� ��ġ ����
        cast1.GetComponent<RemovePlateInteractable>().Destroy();
        cast2.GetComponent<RemovePlateInteractable>().Destroy();

        cast1Safe.transform.gameObject.SetActive(true);
        cast2Safe.transform.gameObject.SetActive(true);
        Debug.Log("��Ǫ�� ��ġ ���� ����");

        // Ʈ�� ���� �ɼ� ����
        // RemovePlateInteractable���� �ɼ� ������

        // 2�� ���� ��ġ �̵� üũ ����
        RNC.nextCastPosComplete = true;
        // UI ����
        accidentDescriptionUI.gameObject.SetActive(false);
        // �÷��̾� ��ġ ����
        player.transform.SetPositionAndRotation(startPosSF.position, startPosSF.rotation);
        // �����屸 ���� �ܰ� Ȱ��ȭ
        wearSafetyGear.SetActive(true);
    }
}