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

    void Start()
    {
        source = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
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
            if (hit.collider.CompareTag("Zombie"))
            {
                Destroy(hit.collider.gameObject);
            }
        }
    }
}
