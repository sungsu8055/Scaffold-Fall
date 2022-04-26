using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class PlayerCtrl : MonoBehaviour
{
    public CharacterController cc;
    public EnterExperienceZone zoneCtrl;
    public XRController oculursController;

    [Header("Control")]
    float horizontalMove;
    float verticalMove;
    public float gravity = -20;
    public float speed = 10;
    float yValocity = 0;

    Vector3 moveDir;

    public enum PlayerState
    {
        Idle,
        EnterZone,
        Working,
        Danger,
        Safety
    }

    public PlayerState p_State;

    void Start()
    {
        cc = GetComponent<CharacterController>();
        p_State = PlayerState.Idle;
    }

    void Update()
    {
        MoveKeyInput();

        Gravity();

        cc.Move(moveDir * speed * Time.deltaTime);
    }

    void MoveKeyInput()
    {
        // XR.Interaction.Toolkit의 컨트롤러 입력 값 함수 사용
        oculursController.inputDevice.TryGetFeatureValue(CommonUsages.primary2DAxis, out Vector2 MoveInput);

        moveDir = new Vector3(MoveInput.x, 0, MoveInput.y);

        // 카메라 움직임에 따라 Player Obj 방향 전환, 움직임 방향도 변경됨
        moveDir = Camera.main.transform.TransformDirection(moveDir);
    }

    // 중력 작용 함수
    void Gravity()
    {
        yValocity = gravity * Time.deltaTime;
        moveDir.y = yValocity;
    }

    private void OnTriggerEnter(Collider other)
    {
        // ExperienceZone 태그 감지 시 함수 호출
        if (other.gameObject.CompareTag("ExperienceZone") && p_State == PlayerState.Idle)
        {
            // 충돌한 체험 존의 UI를 zoneEntryUI로 설정
            zoneCtrl.zoneEntryUI = other.gameObject.transform.GetChild(0);

            // 충돌 감지 후 ZoneEntryController의 함수 호출
            zoneCtrl.ZoneEntry();

            // 중복 활성 방지를 위해 Zone 진입 상태로 변경
            p_State = PlayerState.EnterZone;
        }      
    }
}
