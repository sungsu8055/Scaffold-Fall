using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using DG.Tweening;

public class RemovePlateInteractable : XRGrabInteractable
{
    // �ι�° �׷� ��ȣ�ۿ� ����Ʈ ���� ����Ʈ 
    public List<XRSimpleInteractable> secondGrabPoint = new List<XRSimpleInteractable>();
    // �ι�° �׷� Interactor
    private XRBaseInteractor secondHandInteractor;
    // Attach Point Rotation �⺻�� �ʱ�ȭ�� ����
    private Quaternion attachOriginRotation;

    public Step3_RemoveCast RC;

    public PlayerCtrl player;

    // ���� ���� ���� Ȯ�ο� Ŭ����
    public ControllerPullDetect_Left CPDL;
    public ControllerPullDetect_Right CPDR;

    public bool trackOptionChanged = false;

    private Rigidbody rigidbody;

    void Start()
    {
        rigidbody = this.GetComponent<Rigidbody>();

        // foreach������ �ι�° �׷� state ���� �Ʒ� �۾� ����
        foreach (var item in secondGrabPoint)
        {
            // AddListener�� �̿� �ش� �̺�Ʈ �߻� �� Grab, GrabRelease �Լ� ����
            item.onSelectEnter.AddListener(OnSecondGrab);
            item.onSelectExit.AddListener(OnSecondGrabRelease);
        }
    }

    void FixedUpdate()
    {
        // selectingInteractor�� ���� ���� Ʈ�� �ɼ� Ȱ��,
        // �ٸ� ��Ǫ�� ������Ʈ Ʈ�� �ɼǿ� ���� ��ġ�� ���� ����
        if ((CPDR.pullObjectRight && CPDL.pullObjectLeft) && selectingInteractor)
        {
            if (!trackOptionChanged)
            {
                Debug.Log("��Ǫ�� ����");
                this.GetComponent<AudioSource>().Play();

                this.trackPosition = true;
                this.trackRotation = true;
                trackOptionChanged = true;

                StartCoroutine(RC.PullFormwork());
            }
        }

        // �Ʒ� Ʈ�� �ɼ� ���� ����� selectingInteractor �߰��� ���� ������� �ʰ� ��
        // ��Ʈ�ѷ� ��� üũ������ �ɷ����� ������ ��ΰ� ������ �޾����� selectingInteractor�� ������ ��� �۵����� �ʰ� ��
        /*/
        if((!CPDR.pullObjectRight && !CPDL.pullObjectLeft) && restoreTrackOption)
        {
            Debug.Log("��Ǫ�� ����");
            this.trackPosition = false;
            this.trackRotation = false;
        }
        //*/
    }

    // Interactable�� ������Ʈ ����
    public override void ProcessInteractable(XRInteractionUpdateOrder.UpdatePhase updatePhase)
    {
        if(secondHandInteractor && selectingInteractor)
        {
            /*/ ���� �ڷ� ���� ��ũ��Ʈ(z ������ ��� ���� �� ����)
            selectingInteractor.attachTransform.rotation 
                = Quaternion.LookRotation(secondHandInteractor.attachTransform.position - selectingInteractor.attachTransform.position);
            //*/

            // ������(x ������ ��� ���� �� ���)
            selectingInteractor.attachTransform.LookAt(secondHandInteractor.attachTransform.position, selectingInteractor.attachTransform.right);
            selectingInteractor.attachTransform.RotateAround(selectingInteractor.attachTransform.position, selectingInteractor.attachTransform.right, 90);
            selectingInteractor.attachTransform.RotateAround(selectingInteractor.attachTransform.position, selectingInteractor.attachTransform.up, 90);       
        }
        base.ProcessInteractable(updatePhase);
    }

    // �� ȸ�� �׷��� ���¿� ���� �׷� ���� �� Interactor ����
    public void OnSecondGrab(XRBaseInteractor interactor)
    {
        //Debug.Log("�ι�° �׷� �۵�");
        secondHandInteractor = interactor;
    }
    public void OnSecondGrabRelease(XRBaseInteractor interactor)
    {
        //Debug.Log("�ι�° �׷� Ǯ��");
        secondHandInteractor = null;
    }

    protected override void OnSelectEntered(XRBaseInteractor interactor)
    {
        //Debug.Log("ù��° �׷� �۵�");
        base.OnSelectEntered(interactor);

        // attach point�� rotation ���� �� ����
        attachOriginRotation = interactor.attachTransform.localRotation;
    }
    protected override void OnSelectExited(XRBaseInteractor interactor)
    {
        //Debug.Log("ù��° �׷� Ǯ��");
        base.OnSelectExited(interactor);

        // ��Ǫ�� ����(Ʈ�� Ȱ��ȭ) ���� �� ���� �ۿ� Ȱ��ȭ
        if(this.trackPosition && this.trackRotation)
        {
            rigidbody.isKinematic = false;
            rigidbody.WakeUp();
        }        

        secondHandInteractor = null;
        // ��� ��ȣ�ۿ� �� ������ attach point�� rotation�� ����
        interactor.attachTransform.localRotation = attachOriginRotation;  
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

    public void Destroy()
    {
        Destroy(gameObject);
    }
}
