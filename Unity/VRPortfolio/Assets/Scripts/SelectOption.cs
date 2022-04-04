using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectOption : MonoBehaviour
{
    public EnterExperienceZone enterOptionCtrl;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other);

        if (gameObject.CompareTag("OptionY") && other.CompareTag("GameController"))
        {
            Debug.Log(other);
            enterOptionCtrl.StartExperience(transform.GetChild(0));
        }
        else if (gameObject.CompareTag("OptionN") && other.CompareTag("GameController"))
        {
            Debug.Log("옵션 선택");

            enterOptionCtrl.ExitZone();
        }
    }
}
