using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class WeaponSpawner : MonoBehaviour
{
    [SerializeField] GameObject weaponPrefab; // 무기 프리팹
    [SerializeField] Transform spawnPoint; // 무기가 생성될 위치
    private XRController controller; // XR 컨트롤러

    void Start()
    {
        controller = GetComponent<XRController>();

        if (controller == null)
        {
            Debug.LogError("XR Controller가 설정되지 않았습니다. 컴포넌트를 추가하세요.");
        }

        if (weaponPrefab == null)
        {
            Debug.LogError("무기 프리팹이 할당되지 않았습니다. 인스펙터에서 할당하세요.");
        }

        if (spawnPoint == null)
        {
            Debug.LogError("스폰 포인트가 할당되지 않았습니다. 인스펙터에서 할당하세요.");
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
        // 무기 프리팹을 생성
        if (weaponPrefab != null && spawnPoint != null)
        {
            Instantiate(weaponPrefab, spawnPoint.position, spawnPoint.rotation);
        }
    }
}
