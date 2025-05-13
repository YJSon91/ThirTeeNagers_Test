using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance { get; private set; }           //�ν��Ͻ� ������Ƽ

    [SerializeField] private AudioSource audioSource;                  //����ϴ� ������ҽ� �ν����Ϳ��� ��������

    //��Ȳ�� �°� ���Ǵ� ����� Ŭ�� ����
    [Header("Audio Clips")]
    public AudioClip jumpClip;
    public AudioClip ItemPickupClip;
    public AudioClip scoreClip;
    public AudioClip DeathClip;
    public AudioClip HitClip;


    //���ھ�� pickup�Ǵ°� �����Ƿ� �ణ�� �����̸� �� �� �ְ� ������ ���� ����
    private float lastScoreSoundTime = 0f;
    private float minInterval = 0.05f;

    //���� Ŭ�� �޼���
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
    {       //ª�� �ð� �� �ߺ� ���� ����(minInterval��ŭ�� �ð� ���ȿ��� �ѹ� �� ������� ����)
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
