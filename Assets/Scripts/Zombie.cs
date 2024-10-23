using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Zombie : MonoBehaviour
{
    [SerializeField] NavMeshAgent agent;
    [SerializeField] Transform target;
    [SerializeField] float lookDistance = 8f;
    [SerializeField] int maxHP = 3;
    private int curHP;
    [SerializeField] Animator animator;
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip attackSound;

    private bool isAttacking = false;
    private bool isDead = false;
    private float attackCooltime = 2f;

    private void Start()
    {
        curHP = maxHP;
        StartCoroutine(MoveRoutine());
    }

    IEnumerator MoveRoutine()
    {
        WaitForSeconds delay = new WaitForSeconds(0.2f);

        while (!isDead)
        {
            float distance = Vector3.Distance(transform.position, target.position);

            if (distance <= lookDistance)
            {
                agent.SetDestination(target.position);
                animator.SetBool("isWalking", true);
            }
            else
            {
                agent.SetDestination(transform.position);
                animator.SetBool("isWalking", false);
            }

            yield return delay;
        }
        animator.SetBool("isWalking", false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            animator.SetBool("isWalking", false);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            animator.SetBool("isWalking", false);
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player") && !isAttacking && !isDead)
        {
            float distance = Vector3.Distance(transform.position, target.position);
            if (distance <= lookDistance)
            {
                StartCoroutine(Attack());
            }
        }
    }

    private IEnumerator Attack()
    {
        isAttacking = true;
        animator.SetTrigger("Attack");
        yield return new WaitForSeconds(0.5f);
        audioSource.PlayOneShot(attackSound);

        yield return new WaitForSeconds(1f);

        yield return new WaitForSeconds(attackCooltime);
        isAttacking = false;
    }

    public void TakeDamage(int damage)
    {
        curHP -= damage;
        if (curHP <= 0)
        {
            Die();
        }
        else
        {
            animator.SetTrigger("GetHit");
        }
    }
    private void Die()
    {
        isDead = true;
        animator.SetTrigger("Dead");
        agent.isStopped = true;
        Destroy(gameObject, 5f);
    }
    private void OnDestroy()
    {
        FindObjectOfType<EscapeDoor>()?.ZombieDestroyed();
    }
}
