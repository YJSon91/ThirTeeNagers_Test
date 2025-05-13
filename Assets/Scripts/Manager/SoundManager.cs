using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance { get; private set; }           //인스턴스 프로퍼티

    [SerializeField] private AudioSource audioSource;                  //출력하는 오디오소스 인스펙터에서 가져오기

    //상황에 맞게 사용되는 오디오 클립 변수
    [Header("Audio Clips")]
    public AudioClip jumpClip;
    public AudioClip ItemPickupClip;
    public AudioClip scoreClip;
    public AudioClip DeathClip;
    public AudioClip HitClip;


    //스코어는 pickup되는게 많으므로 약간의 딜레이를 줄 수 있게 딜레이 변수 설정
    private float lastScoreSoundTime = 0f;
    private float minInterval = 0.05f;

    //각종 클립 메서드
    private void Awake()
    {
        if(instance == null) instance = this;
        else Destroy(gameObject);
    }


    public void PlayClip(AudioClip clip)
    {
        if(clip != null) audioSource.PlayOneShot(clip);
    }

    public void PlayJump()
    {
        PlayClip(jumpClip);
    }
    public void PlayItemPickUp()
    {
        PlayClip(ItemPickupClip);
    }
    public void PlayScore()
    {       //짧은 시간 내 중복 실행 방지(minInterval만큼의 시간 동안에는 한번 더 실행되지 않음)
        if(Time.time - lastScoreSoundTime > minInterval)
        {
            PlayClip(scoreClip);
            lastScoreSoundTime = Time.time;
        }

    }

    public void PlayDeath()
    {
        PlayClip(DeathClip);
    }

    public void PlayHit()
    {
        PlayClip(HitClip);
    }
}
