using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StairManager : MonoBehaviour
{
    public GameObject stair;

    void Start()
    {
        CreateStair();
    }

    void Update()
    {
        
    }

    public void CreateStair()
    {
        for(int i = 0; i < 100; i++)
        {
            GameObject stairPrefab = Instantiate(stair);

            stairPrefab.transform.position = new Vector3(0, i * 1, i * 1);

            if (i == 99)
            {

            }
        }
    }
}
