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
        // link ���� ���� �� HMD�� �Է��� �޴� �����̱� ������ ��Ʈ�ѷ� ���� ���� ����, ��Ʈ�ѷ� �Է� ������ �ð� ��� �� ����
        yield return new WaitForSeconds(delayTime);

        // ����Ǿ� �Է��� �޴� ��� ��Ʈ�ѷ��� ������ ����Ʈ
        List<InputDevice> devices = new List<InputDevice>();

        // ��ǲ����̽� �� ��Ʈ�ѷ� Ư���� ������ ����̽��� ����Ʈ�� ����
        InputDevices.GetDevicesWithCharacteristics(controllerCharacteristics, devices);

        // HandController�� ������ ���� �� �Է� ����̽��� �̸� �� ������ Ȯ���ϱ� ���� �Ʒ� �۾� ����
        foreach (var item in devices)
        {
            Debug.Log(item.name + item.characteristics);
        }

        if (devices.Count > 0)
        {
            // ������ ������ ���� �Էµ� ����� ��Ʈ�ѷ��� ����
            targetDevice = devices[0];

            // ��⺰ ��Ʈ�ѷ� ������ ����Ʈ �� ���� �Էµ� ��Ʈ�ѷ��� �̸��� ���� �������� ã�� ����
            // GameObject.Find�� �Ű������� ���ٽ��� �̿��� �־���
            // ������ ����Ʈ �� �Է� ��Ʈ�ѷ��� �̸��� ���� ���� controller ������ ���� �� ã��
            GameObject ctrlPrefab = controllerPrefabs.Find(controller => controller.name == targetDevice.name);

            // ctrlPrefab�� ����Ǹ� Instantiate�Ѵ�
            if (ctrlPrefab)
            {
                spawnCtrl = Instantiate(ctrlPrefab, this.transform);
            }
            // �Էµ� ��Ʈ�ѷ��� ����Ʈ�� ���� �� ���� ���� ��� �� ����Ʈ ������ ����
            else
            {
                Debug.LogError("����� VR ����� ��Ʈ�ѷ� ���� ã�� �� �߽��ϴ�.");
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
