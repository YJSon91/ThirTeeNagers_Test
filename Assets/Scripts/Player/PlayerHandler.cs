using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements.Experimental;

public class PlayerHandler : PlayerState
{

    Animator animator;
    Rigidbody2D _rigidbody2D;

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
            Debug.Log("리지드바디가 할당되지않았습니다.");
        }
    }

    private void FixedUpdate()
    {
        if (isDead) return; //플레이어가 죽은 상태면 아무것도 안하고 빠져나간다.

        Vector3 velocity = _rigidbody2D.velocity; // RigidBody2D에 있는 velocity를 velocity로 변수로 만든다.
        velocity.x = PlayerSpeed; // PlayerSpeed를 velocity.x에 계속 값 넣기

        if (isJump) // 플레이어가 점프 상태라면
        {

            velocity.y += JumpForce; // JumForce 만큼 더해줌
            isJump = false; // 그러고 점프상태를 false로 만들어서 점프를 끝냄
        }
        _rigidbody2D.velocity = velocity; //변경된 값들을 다시 rigidbody2D.velocity에 넣는다
    }
    void OnJump(InputValue inputValue) // 인풋 밸류 시스템에서 입력값을 받아옴
    {

        if (inputValue.isPressed && CurrentJumpCount < MaxJumpCount) // 입력한 값이 true고 현재점프보다 맥스점프카운트가 더 높다면
        {
            isJump = inputValue.isPressed;  // 입력한 값이 true면 isJump를 트루 상태로 변경
            CurrentJumpCount++;
        }
    }

    public void TakeDamage(int damage)
    {
        CurrentHealth -= damage;  // 현재체력 감소 시킴
        animator.SetBool("Damage", true); //애니메이터의 불값을 트루로 변경 해줌
        if (CurrentHealth <= 0)  // 현재체력이 0이되면 Die메서드 실행
        {
            Die();
        }

    }

    void Die()
    {

    }

    void OnTriggerEnter2D(Collider2D collision) // 충돌체에 닿으면 아래 실행
    {
        Debug.Log(CurrentJumpCount);     ///////////// 테스트용 코드(점프 횟수 확인) 
        collision.gameObject.CompareTag("Ground"); // 충돌체의 태그가 "Ground"면
        CurrentJumpCount = 0;    //충돌체에 닿을 경우 점프 횟수 초기화
        Debug.Log(CurrentJumpCount);    ///////////// 테스트용 코드(점프 횟수 확인)
    }
}
