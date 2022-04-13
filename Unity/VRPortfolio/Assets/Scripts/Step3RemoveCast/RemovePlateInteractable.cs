using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class RemovePlateInteractable : XRGrabInteractable
{
    public List<XRSimpleInteractable> secondGrabPoint = new List<XRSimpleInteractable>();

    public ControllerPullDetect_Left CPDL;
    public ControllerPullDetect_Right CPDR;

    bool removed = false;

    void Start()
    {
        foreach(var item in secondGrabPoint)
        {
            item.onSelectEnter.AddListener(OnSecondGrab);
            item.onSelectExit.AddListener(OnSecondGrabRelease);
        }
    }

    public void OnSecondGrab(XRBaseInteractor interactor)
    {
        Debug.Log("두번째 그랩 작동");
    }
    public void OnSecondGrabRelease(XRBaseInteractor interactor)
    {
        Debug.Log("두번째 그랩 풀림");
    }

    protected override void OnSelectEntered(XRBaseInteractor interactor)
    {
        Debug.Log("첫번째 그랩 작동");
        base.OnSelectEntered(interactor);
    }
    protected override void OnSelectExited(XRBaseInteractor interactor)
    {
        Debug.Log("첫번째 그랩 풀림");
        base.OnSelectExited(interactor);
    }

    void Update()
    {
        if(CPDR.pullObjectRight)
        {
            Debug.Log("거푸집 제거");
            this.trackPosition = true;
            this.trackRotation = true;

            removed = true;
        }
        else if(!CPDR.pullObjectRight && !removed)
        {
            Debug.Log("거푸집 고정");
            this.trackPosition = false;
            this.trackRotation = false;
        }
    }

    // 파라미터로 주어진 interactor가 interactable을 선택할 수 있는지 여부를 결정하는 메소드
    public override bool IsSelectableBy(XRBaseInteractor interactor)
    {
        // 해당 물체를 선택한 selectingInteractor가 인식되고,
        // 파라미터로 들어온 interactor가 먼저 인식된 interactor와 같지 않을 경우 isAlreadyGrabbed 값을 true로 반환하여,
        // 이미 잡는 동작 중인 상태를 알림
        bool isAlreadyGrabbed = selectingInteractor && !interactor.Equals(selectingInteractor);

        // && 연산자 사용 해당 메소드에 bool 값을 반환
        // base.IsSelectableBy(interactor) true 값 반환됨 && isAlreadyGrabbed가 false일 시 true가 반환되어 오브젝트 선택 가능
        // true일 시 false가 반환되어 오브젝트 선택불가
        return base.IsSelectableBy(interactor) && !isAlreadyGrabbed;
    }
}
