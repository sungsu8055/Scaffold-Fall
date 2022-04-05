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
        // �� ü������ ����2�� �����ϱ� ���� ���� UI ������ �����Ͽ� ���� ����
        enterOptionCtrl.nextStep = step2;
    }

    private void OnTriggerEnter(Collider other)
    {
        // �ɼ� ��ư �ܿ��� �浹 üũ �� �±׸� ���� �ɼ� ���� �� ��Ʈ�ѷ� ������ üũ
        if (gameObject.CompareTag("OptionY") && other.CompareTag("GameController"))
        {
            enterOptionCtrl.StartExperience(startPos);
        }
        else if (gameObject.CompareTag("OptionN") && other.CompareTag("GameController"))
        {
            enterOptionCtrl.ExitZone();
        }
    }
}
