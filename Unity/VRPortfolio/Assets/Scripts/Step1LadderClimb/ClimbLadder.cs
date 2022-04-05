using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ClimbLadder : MonoBehaviour
{
    public Transform player;
    public Transform highPoint;
    public Transform endPoint;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            // other.transform.DOPath(waypoint, 2.0f, PathType.Linear, PathMode.Full3D, 10, null);            
            // other.transform.DORotateQuaternion(endPoint.rotation, 1.0f);
            
            // 애니메이션 시작 전 위치 재정렬을 위해 CC 비활성
            player.GetComponent<CharacterController>().enabled = false;

            // 위치 재정렬
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
    }

    void NextStep()
    {
        
    }
}
