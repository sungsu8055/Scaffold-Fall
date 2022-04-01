using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCtrl : MonoBehaviour
{
    public CharacterController cc;
    public GameObject zoneCtrl;

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
        zoneCtrl = GetComponent<EnterExperienceZone>();
    }

    void Update()
    {
        MoveKeyInput();

        Gravity();

        cc.Move(moveDir * speed * Time.deltaTime);
    }

    void MoveKeyInput()
    {
        // PC or VR ???????? ???? ?????? ???? ?????? ????
        horizontalMove = ARAVRInput.GetAxis("Horizontal");
        verticalMove = ARAVRInput.GetAxis("Vertical");

        moveDir = new Vector3(horizontalMove, 0, verticalMove);

        // ?????? ???? ?????? ???? ???? ???? ????
        moveDir = Camera.main.transform.TransformDirection(moveDir);
    }

    void Gravity()
    {
        yValocity = gravity * Time.deltaTime;

        moveDir.y = yValocity;
    }


}
