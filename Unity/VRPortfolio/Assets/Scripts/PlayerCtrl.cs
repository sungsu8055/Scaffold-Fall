using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCtrl : MonoBehaviour
{
    public CharacterController cc;

    [Header("Control")]
    public float horizontalMove;
    public float verticalMove;
    public float gravity = -20;
    public float speed = 10;
    float yValocity = 0;

    Vector3 moveDir;

    void Start()
    {
        cc = GetComponent<CharacterController>();
    }

    void Update()
    {
        MoveKeyInput();

        Gravity();

        cc.Move(moveDir * speed * Time.deltaTime);
    }

    void MoveKeyInput()
    {
        // PC or VR 키입력을 받아 이동값 출력 변수로 저장
        horizontalMove = ARAVRInput.GetAxis("Horizontal");
        verticalMove = ARAVRInput.GetAxis("Vertical");

        moveDir = new Vector3(horizontalMove, 0, verticalMove);

        // 카메라 방향 전환에 따라 이동 방향 전환
        moveDir = Camera.main.transform.TransformDirection(moveDir);
    }

    void Gravity()
    {
        yValocity = gravity * Time.deltaTime;

        moveDir.y = yValocity;
    }


}
