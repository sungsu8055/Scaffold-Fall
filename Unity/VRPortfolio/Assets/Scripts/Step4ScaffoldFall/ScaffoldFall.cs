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
    public ControllerPullDetect_Left CPDL;
    public PlayerCtrl player;
    public Transform popupPos;
    private Transform accidentDescriptionCV;
    public Transform accidentDescriptionUI;
    public Transform accidentPreventionDescriptionUI;
    public Transform blood;

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

    public bool isFellDown = true;

    [Header("Audio")]
    public AudioManager audioManager;
    public AudioClip fallDie;
    public AudioClip accident;
    public AudioClip accidentPrevent;

    void Start()
    {
        accidentDescriptionCV = this.transform.GetChild(0);
    }

    void Update()
    {
        if (player.p_State == PlayerCtrl.PlayerState.Danger)
        {
            if (CPDR.pullObjectRight && CPDL.pullObjectLeft)
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
            if (CPDR.pullObjectRight && CPDL.pullObjectLeft)
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
        StartCoroutine(audioManager.PlayAudioDelayed(fallDie, 2.8f));
        player.transform.DOMove(fallPos.position, 1.5f).SetEase(Ease.OutBounce).SetDelay(3.0f);
        player.transform.DORotateQuaternion(fallPos.rotation, 0.5f).SetDelay(3.5f);
        blood.DOScale(1.0f, 0.5f).SetDelay(3.5f);
    }

    void FallSafely()
    {
        player.transform.DOMove(spinningPos.position, 2.0f);
        player.transform.DOShakePosition(1.5f, axisShakeStrength, vibrato, randomness).SetDelay(0.5f);
        player.transform.DORotateQuaternion(spinningPos.rotation, 1.5f).SetDelay(1.8f);
        StartCoroutine(audioManager.PlayAudioDelayed(fallDie, 3.0f));
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

        audioManager.PlayAudioOnce(accident);

        yield return new WaitForSeconds(12.0f);

        ResetExperienceConditions();
    }

    IEnumerator AccidentPreventionDescription()
    {
        yield return new WaitForSeconds(11.0f);

        accidentDescriptionCV.gameObject.SetActive(true);
        accidentDescriptionCV.SetPositionAndRotation(popupPos.position, popupPos.rotation);
        accidentPreventionDescriptionUI.gameObject.SetActive(true);

        audioManager.PlayAudioOnce(accidentPrevent);

        yield return new WaitForSeconds(12.0f);

        SceneManager.LoadScene(0);
    }


    void ResetExperienceConditions()
    {
        // �÷��̾� ���� ����
        player.p_State = PlayerCtrl.PlayerState.Working;

        // ��Ǫ�� ����
        cast1.GetComponent<RemovePlateInteractable>().Destroy();
        cast2.GetComponent<RemovePlateInteractable>().Destroy();
        cast1Safe.transform.gameObject.SetActive(true);
        cast2Safe.transform.gameObject.SetActive(true);
        Debug.Log("��Ǫ�� ��ġ ���� ����");

        // Ʈ�� ���� �ɼ� ����
        // RNC.RPI.restoreTrackOption = true;

        // 2�� ���� ��ġ �̵� üũ ����
        RNC.nextCastPosComplete = false;
        // UI ����
        accidentDescriptionUI.gameObject.SetActive(false);
        accidentDescriptionCV.gameObject.SetActive(false);

        // ��Ǫ�� ���� �ȳ� UI ����
        RNC.RC.instructionUI.transform.position = RNC.RC.instructionUIOriginPos;
        // RNC.RC.instructionText.text = "��Ǫ�� Ż�� �۾��� �����ϰڽ��ϴ�. �̾����� �ȳ��� ���� �۾��� �������ֽʽÿ�.";

        // ���� ����
        blood.localScale = Vector3.zero;

        // �÷��̾� ��ġ ����
        player.transform.SetPositionAndRotation(startPosSF.position, startPosSF.rotation);

        // �����屸 ���� �ܰ� Ȱ��ȭ
        wearSafetyGear.SetActive(true);
    }
}