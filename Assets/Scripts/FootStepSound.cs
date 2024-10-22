using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class FootStepSound : MonoBehaviour
{
    [SerializeField]
    private AudioClip footStepSound; // 발소리 클립
    [SerializeField]
    private float footStepDelay = 0.5f; // 발소리 간격

    private CharacterController characterController;
    private AudioSource audioSource;
    private float nextFootstepTime = 0;

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
        audioSource = gameObject.AddComponent<AudioSource>(); // AudioSource 추가
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

            // 캐릭터가 이동 중일 때 발소리 재생
            if (velocity.magnitude > 0.1f)
            {
                nextFootstepTime -= Time.deltaTime;

                if (nextFootstepTime <= 0)
                {
                    audioSource.PlayOneShot(footStepSound, 1.0f); // 발소리 재생
                    nextFootstepTime = footStepDelay; // 다음 발소리 재생 시간 설정
                }
            }
        }
    }
}
