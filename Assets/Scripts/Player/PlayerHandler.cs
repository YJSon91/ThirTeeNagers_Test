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
            TakeDamage(1);
        }
    }

    private void FixedUpdate()
    {
        if (isDead) return; //플레이어가 죽은 상태면 아무것도 안하고 빠져나간다.

        Vector3 velocity = _rigidbody2D.velocity; // RigidBody2D에 있는 velocity를 복사
        velocity.x = PlayerSpeed;

        velocity.x = GetComponent<PlayerItemInteraction>().GetCurrentSpeed();//

        animator.SetBool("IsRun", true);

        if (isJump) // 플레이어가 점프 상태라면
        {
            velocity.y += JumpForce; // JumForce 만큼 더해줌
            animator.SetBool("IsJump", true);
            isJump = false; // 점프 상태를 false로 바꿔줌
        }

        if (_rigidbody2D.velocity.y < -0.1f)
        {
            animator.SetBool("IsJump", false);  // 꼭 같이 꺼주기!
            animator.SetBool("IsFall", true);
        }


        _rigidbody2D.velocity = velocity; //변경된 값들을 다시 rigidbody2D.velocity에 넣는다

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

        CurrentHealth -= damage;  // 현재체력 감소 시킴
        StartCoroutine(HitEffect());
        animator.SetTrigger("IsHit");
        if (CurrentHealth <= 0)  // 현재체력이 0이되면 Die메서드 실행

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

        Debug.Log(CurrentJumpCount);     ///////////// 테스트용 코드(점프 횟수 확인) 
        collision.gameObject.CompareTag("Ground"); // 충돌체의 태그가 "Ground"면
        animator.SetBool("IsJump", false);
        animator.SetBool("IsFall", false);
        CurrentJumpCount = 0;    //충돌체에 닿을 경우 점프 횟수 초기화
        Debug.Log(CurrentJumpCount);    ///////////// 테스트용 코드(점프 횟수 확인)

    }

    private IEnumerator HitEffect()
    {
        animator.SetTrigger("IsHit");
        yield return new WaitForSeconds(0.05f);
        animator.SetTrigger("IsHit");
    }
}
