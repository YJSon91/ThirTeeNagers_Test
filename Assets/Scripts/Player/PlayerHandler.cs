using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements.Experimental;

public class PlayerHandler : PlayerState
{
    Animator animator;
    Rigidbody2D _rigidbody2D;

    // 아이템 효과에 의한 속도 배율 적용용
    private PlayerItemInteraction itemInteraction;

    void Awake()
    {
        animator = GetComponentInChildren<Animator>();
        if (animator == null)
        {
            Debug.Log("애니메이터가 할당되지 않았습니다.");
        }

        _rigidbody2D = GetComponent<Rigidbody2D>();
        if (_rigidbody2D == null)
        {
            Debug.Log("리지드바디가 할당되지 않았습니다.");
        }

        itemInteraction = GetComponent<PlayerItemInteraction>();
        if (itemInteraction == null)
        {
            Debug.Log("PlayerItemInteraction 스크립트가 없습니다.");
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            TakeDamage(1); // 테스트용 데미지 입력
        }
    }

    private void FixedUpdate()
    {
        if (isDead) return; // 플레이어가 죽은 상태면 아무것도 안 함

        Vector3 velocity = _rigidbody2D.velocity;

        // 아이템 속도 효과를 반영한 현재 속도 계산
        if (itemInteraction != null)
        {
            velocity.x = itemInteraction.GetCurrentSpeed();
        }
        else
        {
            velocity.x = PlayerSpeed; // fallback
        }

        animator.SetBool("IsRun", true); // 기본 상태는 달리기

        if (isJump)
        {
            velocity.y += JumpForce;
            animator.SetBool("IsJump", true);
            isJump = false;
        }

        // 낙하 중일 때 애니메이션 처리
        if (_rigidbody2D.velocity.y < -0.1f)
        {
            animator.SetBool("IsJump", false);
            animator.SetBool("IsFall", true);
        }

        _rigidbody2D.velocity = velocity; // 실제 속도 적용
    }

    void OnJump(InputValue inputValue)
    {
        if (inputValue.isPressed && CurrentJumpCount < MaxJumpCount)
        {
            isJump = inputValue.isPressed;
            CurrentJumpCount++;
        }
    }

    public void TakeDamage(int damage)
    {
        CurrentHealth -= damage;
        StartCoroutine(HitEffect());
        animator.SetTrigger("IsHit");

        if (CurrentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        // TODO: 죽었을 때 로직 구현
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            animator.SetBool("IsJump", false);
            animator.SetBool("IsFall", false);
            CurrentJumpCount = 0;
        }
    }

    private IEnumerator HitEffect()
    {
        animator.SetTrigger("IsHit");
        yield return new WaitForSeconds(0.05f);
        animator.SetTrigger("IsHit");
    }
}
