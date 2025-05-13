using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance { get; private set; }

    [SerializeField] private AudioSource audioSource;

    [Header("Audio Clips")]
    public AudioClip jumpClip;
    public AudioClip ItemPickupClip;
    public AudioClip scoreClip;
    public AudioClip DeathClip;
    public AudioClip HitClip;

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
    {
        PlayClip(scoreClip);
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
