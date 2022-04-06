using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ClimbLadder : MonoBehaviour
{
    public Step3_RemoveCast RC;

    public Transform player;
    public Transform highPoint;
    public Transform endPoint;

    public GameObject approachGuide;


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            approachGuide.SetActive(false);

            // �ִϸ��̼� ���� �� ��ġ �������� ���� CC ��Ȱ��
            player.GetComponent<CharacterController>().enabled = false;

            // ��ġ ������
            player.SetPositionAndRotation(this.transform.position, this.transform.rotation);

            StartCoroutine(Climb());
        }
    }

    IEnumerator Climb()
    {
        yield return new WaitForSeconds(1.0f);

        player.DOMove(highPoint.position, 1.0f, false);

        yield return new WaitForSeconds(1.5f);

        player.DORotateQuaternion(highPoint.rotation, 1.0f);

        yield return new WaitForSeconds(1.0f);

        player.DOMove(endPoint.position, 1.0f, false);

        yield return new WaitForSeconds(1.0f);

        player.DORotateQuaternion(endPoint.rotation, 1.0f);

        yield return new WaitForSeconds(1.0f);

        RC.StartInstruction();
    }
}
