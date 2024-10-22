using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class WeaponSpawner : MonoBehaviour
{
    [SerializeField] GameObject weaponPrefab; // ���� ������
    [SerializeField] Transform spawnPoint; // ���Ⱑ ������ ��ġ
    private XRController controller; // XR ��Ʈ�ѷ�

    void Start()
    {
        controller = GetComponent<XRController>();

        if (controller == null)
        {
            Debug.LogError("XR Controller�� �������� �ʾҽ��ϴ�. ������Ʈ�� �߰��ϼ���.");
        }

        if (weaponPrefab == null)
        {
            Debug.LogError("���� �������� �Ҵ���� �ʾҽ��ϴ�. �ν����Ϳ��� �Ҵ��ϼ���.");
        }

        if (spawnPoint == null)
        {
            Debug.LogError("���� ����Ʈ�� �Ҵ���� �ʾҽ��ϴ�. �ν����Ϳ��� �Ҵ��ϼ���.");
        }
    }

    void Update()
    {
        if (controller != null)
        {
            InputDevice device = controller.inputDevice;
            if (device.isValid)
            {
                if (device.TryGetFeatureValue(CommonUsages.gripButton, out bool gripValue) && gripValue)
                {
                    SpawnWeapon();
                }
            }
        }
    }

    void SpawnWeapon()
    {
        // ���� �������� ����
        if (weaponPrefab != null && spawnPoint != null)
        {
            Instantiate(weaponPrefab, spawnPoint.position, spawnPoint.rotation);
        }
    }
}
