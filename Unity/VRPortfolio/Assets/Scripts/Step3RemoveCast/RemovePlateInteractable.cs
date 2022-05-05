using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using DG.Tweening;

public class RemovePlateInteractable : XRGrabInteractable
{
    // 두번째 그랩 상호작용 포인트 저장 리스트 
    public List<XRSimpleInteractable> secondGrabPoint = new List<XRSimpleInteractable>();
    // 두번째 그랩 Interactor
    private XRBaseInteractor secondHandInteractor;
    // Attach Point Rotation 기본값 초기화용 변수
    private Quaternion attachOriginRotation;

    public Step3_RemoveCast RC;

    public PlayerCtrl player;

    // 당기기 동작 유무 확인용 클래스
    public ControllerPullDetect_Left CPDL;
    public ControllerPullDetect_Right CPDR;

    public bool trackOptionChanged = false;

    private Rigidbody rigidbody;

    void Start()
    {
        rigidbody = this.GetComponent<Rigidbody>();

        // foreach문으로 두번째 그랩 state 마다 아래 작업 실행
        foreach (var item in secondGrabPoint)
        {
            // AddListener를 이용 해당 이벤트 발생 시 Grab, GrabRelease 함수 실행
            item.onSelectEnter.AddListener(OnSecondGrab);
            item.onSelectExit.AddListener(OnSecondGrabRelease);
        }
    }

    void FixedUpdate()
    {
        // selectingInteractor가 들어올 때만 트랙 옵션 활성,
        // 다른 거푸집 오브젝트 트랙 옵션에 영향 미치는 것을 방지
        if ((CPDR.pullObjectRight && CPDL.pullObjectLeft) && selectingInteractor)
        {
            if (!trackOptionChanged)
            {
                Debug.Log("거푸집 제거");
                this.GetComponent<AudioSource>().Play();

                this.trackPosition = true;
                this.trackRotation = true;
                trackOptionChanged = true;

                StartCoroutine(RC.PullFormwork());
            }
        }

        // 아래 트랙 옵션 원복 기능은 selectingInteractor 추가로 인해 사용하지 않게 됨
        // 컨트롤러 당김 체크만으로 걸러냈을 때에는 모두가 영향을 받았으나 selectingInteractor가 없으면 기능 작동하지 않게 됨
        /*/
        if((!CPDR.pullObjectRight && !CPDL.pullObjectLeft) && restoreTrackOption)
        {
            Debug.Log("거푸집 고정");
            this.trackPosition = false;
            this.trackRotation = false;
        }
        //*/
    }

    // Interactable의 업데이트 진행
    public override void ProcessInteractable(XRInteractionUpdateOrder.UpdatePhase updatePhase)
    {
        if(secondHandInteractor && selectingInteractor)
        {
            /*/ 참고 자료 원본 스크립트(z 축으로 길게 잡을 때 적용)
            selectingInteractor.attachTransform.rotation 
                = Quaternion.LookRotation(secondHandInteractor.attachTransform.position - selectingInteractor.attachTransform.position);
            //*/

            // 수정본(x 축으로 길게 잡을 때 사용)
            selectingInteractor.attachTransform.LookAt(secondHandInteractor.attachTransform.position, selectingInteractor.attachTransform.right);
            selectingInteractor.attachTransform.RotateAround(selectingInteractor.attachTransform.position, selectingInteractor.attachTransform.right, 90);
            selectingInteractor.attachTransform.RotateAround(selectingInteractor.attachTransform.position, selectingInteractor.attachTransform.up, 90);       
        }
        base.ProcessInteractable(updatePhase);
    }

    // 각 회차 그랩별 상태에 따라 그랩 유무 및 Interactor 설정
    public void OnSecondGrab(XRBaseInteractor interactor)
    {
        //Debug.Log("두번째 그랩 작동");
        secondHandInteractor = interactor;
    }
    public void OnSecondGrabRelease(XRBaseInteractor interactor)
    {
        //Debug.Log("두번째 그랩 풀림");
        secondHandInteractor = null;
    }

    protected override void OnSelectEntered(XRBaseInteractor interactor)
    {
        //Debug.Log("첫번째 그랩 작동");
        base.OnSelectEntered(interactor);

        // attach point의 rotation 기존 값 저장
        attachOriginRotation = interactor.attachTransform.localRotation;
    }
    protected override void OnSelectExited(XRBaseInteractor interactor)
    {
        //Debug.Log("첫번째 그랩 풀림");
        base.OnSelectExited(interactor);

        // 거푸집 제거(트랙 활성화) 진행 시 물리 작용 활성화
        if(this.trackPosition && this.trackRotation)
        {
            rigidbody.isKinematic = false;
            rigidbody.WakeUp();
        }        

        secondHandInteractor = null;
        // 양손 상호작용 후 조정된 attach point의 rotation을 원복
        interactor.attachTransform.localRotation = attachOriginRotation;  
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

    public void Destroy()
    {
        Destroy(gameObject);
    }
}
