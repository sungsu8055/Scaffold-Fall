using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class HandControllerPresence : MonoBehaviour
{
    public InputDeviceCharacteristics controllerCharacteristics;
    public List<GameObject> controllerPrefabs;
    public bool showController = false;
    public GameObject handPrefab;
    
    InputDevice targetDevice;
    GameObject spawnCtrl;
    GameObject spawnHandModel;
    Animator handAnim;

    void Start()
    {
        StartCoroutine(CreateInputDeviceController(1.0f));
    }

    void Update()
    {
        SelectControllerType();

        UpdateHandAnim();
    }

    IEnumerator CreateInputDeviceController(float delayTime)
    {
        // link 에서 시작 시 HMD만 입력을 받는 상태이기 때문에 컨트롤러 값이 들어가지 않음, 컨트롤러 입력 들어오는 시간 대기 후 실행
        yield return new WaitForSeconds(delayTime);

        // 연결되어 입력을 받는 모든 컨트롤러를 저장할 리스트
        List<InputDevice> devices = new List<InputDevice>();

        // 인풋디바이스 중 컨트롤러 특성을 가지는 디바이스만 리스트에 저장
        InputDevices.GetDevicesWithCharacteristics(controllerCharacteristics, devices);

        // HandController에 프리팹 연결 시 입력 디바이스의 이름 및 정보를 확인하기 위해 아래 작업 실행
        foreach (var item in devices)
        {
            Debug.Log(item.name + item.characteristics);
        }

        if (devices.Count > 0)
        {
            // 프리팹 생성을 위해 입력된 기기의 컨트롤러를 저장
            targetDevice = devices[0];

            // 기기별 컨트롤러 프리팹 리스트 중 현재 입력된 컨트롤러와 이름이 같은 프리팹을 찾아 저장
            // GameObject.Find의 매개변수는 람다식을 이용해 넣어줌
            // 프리팹 리스트 중 입력 컨트롤러와 이름이 같은 것을 controller 변수로 저장 후 찾기
            GameObject ctrlPrefab = controllerPrefabs.Find(controller => controller.name == targetDevice.name);

            // ctrlPrefab가 저장되면 Instantiate한다
            if (ctrlPrefab)
            {
                spawnCtrl = Instantiate(ctrlPrefab, this.transform);
            }
            // 입력된 컨트롤러가 리스트에 없을 시 에러 문구 출력 후 디폴트 프리팹 생성
            else
            {
                Debug.LogError("연결된 VR 기기의 컨트롤러 모델을 찾지 못 했습니다.");
                spawnCtrl = Instantiate(controllerPrefabs[0], this.transform);
            }

            spawnHandModel = Instantiate(handPrefab, this.transform);
            handAnim = spawnHandModel.GetComponent<Animator>();
        }
    }

    void UpdateHandAnim()
    {
        if(targetDevice.TryGetFeatureValue(CommonUsages.trigger, out float triggerValue))
        {
            handAnim.SetFloat("Trigger", triggerValue);
        }
        else
        {
            handAnim.SetFloat("Trigger", 0);
        }

        if (targetDevice.TryGetFeatureValue(CommonUsages.grip, out float gripValue))
        {
            handAnim.SetFloat("Grip", gripValue);
        }
        else
        {
            handAnim.SetFloat("Grip", 0);
        }
    }

    void SelectControllerType()
    {
        if (showController)
        {
            spawnCtrl.SetActive(true);
            spawnHandModel.SetActive(false);
        }
        else
        {
            spawnCtrl.SetActive(false);
            spawnHandModel.SetActive(true);
        }
    }
}
