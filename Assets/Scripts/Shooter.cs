using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;

public class Shooter : MonoBehaviour
{
    [SerializeField] float weaponRange = 20f;

    [SerializeField] Transform firePoint;

    [SerializeField] ParticleSystem muzzleFlash;
    [SerializeField] ParticleSystem cartridgeEjection;

    [SerializeField] Animator animator;

    [SerializeField] AudioClip fireSfx;
    private AudioSource source;

    public GameObject metalHitEffect;
    public GameObject stoneHitEffect;
    public GameObject[] fleshHitEffects;
    public GameObject woodHitEffect;

    void Start()
    {
        source = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
    }
    private void Update()
    {
        Debug.DrawLine(firePoint.position, firePoint.position + firePoint.forward * weaponRange, Color.red);
    }
    public void Fire()
    {
        muzzleFlash.Play();
        cartridgeEjection.Play();
        animator.SetTrigger("Fire");
        source.PlayOneShot(fireSfx, 0.9f);

        Vector3 rayOrigin = firePoint.position;
        RaycastHit hit;

        if (Physics.Raycast(rayOrigin, firePoint.forward, out hit, weaponRange))
        {
            HandleHit(hit);

            if (hit.collider.CompareTag("Zombie"))
            {
                Destroy(hit.collider.gameObject, 2f);
            }
            if (hit.collider.CompareTag("Zombie2"))
            {
                StartCoroutine(LoadGameSceneAfterDelay(2f));
            }
        }
    }
    void HandleHit(RaycastHit hit)
    {
        if (hit.collider.sharedMaterial != null)
        {
            string materialName = hit.collider.sharedMaterial.name;

            switch (materialName)
            {
                case "Metal":
                    SpawnDecal(hit, metalHitEffect);
                    break;
                case "Stone":
                    SpawnDecal(hit, stoneHitEffect);
                    break;
                case "Wood":
                    SpawnDecal(hit, woodHitEffect);
                    break;
                case "Meat":
                    SpawnDecal(hit, fleshHitEffects[Random.Range(0, fleshHitEffects.Length)]);
                    break;
            }
        }
    }
    void SpawnDecal(RaycastHit hit, GameObject prefab)
    {
        GameObject spawnedDecal = GameObject.Instantiate(prefab, hit.point, Quaternion.LookRotation(hit.normal));
        spawnedDecal.transform.SetParent(hit.collider.transform);
    }
    private IEnumerator LoadGameSceneAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene("GameScene");
    }
}
