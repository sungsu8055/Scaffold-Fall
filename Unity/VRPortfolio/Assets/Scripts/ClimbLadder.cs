using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ClimbLadder : MonoBehaviour
{
    public Transform player;
    public Transform highPoint;
    public Transform endPoint;

    public Vector3[] waypoint;

    void Start()
    {
        waypoint[0] = highPoint.position;
        waypoint[1] = endPoint.position;
    }

    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            // other.transform.DOMove(highPoint.position, 1f, false);

            other.transform.DOPath(waypoint, 2.0f, PathType.Linear, PathMode.Full3D, 10, null);
            other.transform.DORotateQuaternion(endPoint.rotation, 1.0f);
        }
    }
}
