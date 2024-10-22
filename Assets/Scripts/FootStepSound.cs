using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class FootStepSound : MonoBehaviour
{
    [SerializeField]
    private AudioClip footStepSound; // �߼Ҹ� Ŭ��
    [SerializeField]
    private float footStepDelay = 0.5f; // �߼Ҹ� ����

    private CharacterController characterController;
    private AudioSource audioSource;
    private float nextFootstepTime = 0;

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
        audioSource = gameObject.AddComponent<AudioSource>(); // AudioSource �߰�
        audioSource.clip = footStepSound;
    }

    private void Update()
    {
        PlayFootstepSound();
    }

    private void PlayFootstepSound()
    {
        if (characterController.isGrounded)
        {
            Vector3 velocity = characterController.velocity;

            // ĳ���Ͱ� �̵� ���� �� �߼Ҹ� ���
            if (velocity.magnitude > 0.1f)
            {
                nextFootstepTime -= Time.deltaTime;

                if (nextFootstepTime <= 0)
                {
                    audioSource.PlayOneShot(footStepSound, 1.0f); // �߼Ҹ� ���
                    nextFootstepTime = footStepDelay; // ���� �߼Ҹ� ��� �ð� ����
                }
            }
        }
    }
}
