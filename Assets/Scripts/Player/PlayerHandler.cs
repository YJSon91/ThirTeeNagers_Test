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
        animator = GetComponent<Animator>();
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

    private void FixedUpdate()
    {
        if (isDead) return; //플레이어가 죽은 상태면 아무것도 안하고 빠져나간다.

        Vector3 velocity = _rigidbody2D.velocity; // RigidBody2D에 있는 velocity를 복사

        // 아이템 효과가 있으면 GameManager에서 속도 가져오고 배율 적용
        float adjustedSpeed = PlayerSpeed;
        if (itemInteraction != null)
        {
            adjustedSpeed = itemInteraction.GetCurrentSpeed();
        }

        velocity.x = adjustedSpeed; // 실제 적용 속도 = 기본 속도 * 배율

        // 점프 처리
        if (isJump)
        {
            velocity.y += JumpForce;
            animator.SetBool("IsJump", true);
            isJump = false;
        }

        // 점프 후 낙하 상태 감지 (속도 음수일 경우)
        if (_rigidbody2D.velocity.y < -0.1f)
        {
            animator.SetBool("IsJump", false);
            animator.SetBool("IsFall", true);
        }
        else if (_rigidbody2D.velocity.y >= 0)
        {
            animator.SetBool("IsFall", false);
        }

        animator.SetBool("IsRun", true); // 기본 달리기 애니메이션 유지

        _rigidbody2D.velocity = velocity; // 변경된 속도 적용
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
        animator.SetBool("Damage", true);

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
            CurrentJumpCount = 0;
        }
    }
}
