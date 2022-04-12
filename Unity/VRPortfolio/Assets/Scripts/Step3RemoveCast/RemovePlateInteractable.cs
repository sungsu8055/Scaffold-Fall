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
            Debug.Log("��Ǫ�� ����");
            this.trackPosition = true;
            this.trackRotation = true;

            removed = true;
        }
        else if(!CPDR.pullObjectRight && !removed)
        {
            Debug.Log("��Ǫ�� ����");
            this.trackPosition = false;
            this.trackRotation = false;
        }
    }

    // �Ķ���ͷ� �־��� interactor�� interactable�� ������ �� �ִ��� ���θ� �����ϴ� �޼ҵ�
    public override bool IsSelectableBy(XRBaseInteractor interactor)
    {
        // �ش� ��ü�� ������ selectingInteractor�� �νĵǰ�,
        // �Ķ���ͷ� ���� interactor�� ���� �νĵ� interactor�� ���� ���� ��� isAlreadyGrabbed ���� true�� ��ȯ�Ͽ�,
        // �̹� ��� ���� ���� ���¸� �˸�
        bool isAlreadyGrabbed = selectingInteractor && !interactor.Equals(selectingInteractor);

        // && ������ ��� �ش� �޼ҵ忡 bool ���� ��ȯ
        // base.IsSelectableBy(interactor) true �� ��ȯ�� && isAlreadyGrabbed�� false�� �� true�� ��ȯ�Ǿ� ������Ʈ ���� ����
        // true�� �� false�� ��ȯ�Ǿ� ������Ʈ ���úҰ�
        return base.IsSelectableBy(interactor) && !isAlreadyGrabbed;
    }
}
