using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState : MonoBehaviour
{
    [SerializeField] private int currenthealth = 3;  // 현재 체력
    public int CurrentHealth
    {
        get { return currenthealth; }
        set { currenthealth = Mathf.Clamp(value, 0, 3); }  // 현재체력이 0보다 낮을 수 없고 5보다 높을 수 없음.(최소값 및 최대값 설정)

    }

    [SerializeField] private int maxhealth = 3;// 최대 체력
    public int MaxHealth
    {
        get { return maxhealth; }
        set { maxhealth = Mathf.Clamp(value, 0, 3); } // 최대체력이 0보다 낮을 수 없고 5보다 높을 수 없음.(최소값 및 최대값 설정)

    }


    [SerializeField] private float playerSpeed = 3; //플레이어 속도

    public float PlayerSpeed
    {
        get { return playerSpeed; }
        set { playerSpeed = Mathf.Clamp(value, 0, 20); } // 플레이어 속도가 0보다 낮을 수 없고 10보다 높을 수 없음.(최소값 및 최대값 설정)
    }

    [SerializeField] private float jumpForce = 3; // 플레이어 점프 속도 (점프 속도는 고정으로 하기 위해 읽기만 가능)

    public float JumpForce
    {
        get { return jumpForce; }
    }

    [SerializeField] private int cureentJumpCount = 0; // 플레이어 점프 속도 (점프 속도는 고정으로 하기 위해 읽기만 가능)

    public int CurrentJumpCount
    {
        get { return cureentJumpCount; }
        set { cureentJumpCount = Mathf.Clamp(value, 0, 2); }

    }

    [SerializeField] private int maxJumpCount = 2; // 플레이어 점프 속도 (점프 속도는 고정으로 하기 위해 읽기만 가능)

    public int MaxJumpCount
    {
        get { return maxJumpCount; }

    }

    [SerializeField] private float hitCooldown = 1; // 플레이가 피격 당할 시 쿨타임 주기

    [SerializeField] private float knockbackForce = 5f;

    public float KnockbackForce
    {
        get {  return knockbackForce; }
        set { knockbackForce = value; }
    }
    [SerializeField] private float knockbackDuration = 0.2f;

    public float KnockbackDuration
    {
        get { return knockbackDuration; }
        set { knockbackDuration = value; }
    }


    public float HitCooldown
    {
        get { return hitCooldown; }
    }

    public bool isKnockback = false;
    public bool isDead = false; // 플레이어가 죽었는지 체크
    public bool isJump = false; // 플레이어가 점프 상태인지 체크
    public bool isSliding = false; // 플레이어가 점프 상태인지 체크


}
