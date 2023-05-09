using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;
    [SerializeField] AudioSource audioSource;

    public AudioClip coinSound;
    public AudioClip bobaThrowSound;
    public AudioClip swooshSound;
    public AudioClip serveSound;
    public AudioClip angrySound;
    public AudioClip winningSound;


    public enum Sound
    {
        coinSound,
        bobaThrowSound,
        swooshSound,
        serveSound,
        angrySound,
        winningSound,
    }

    public void Coin()
    {
        audioSource.PlayOneShot(coinSound);
        audioSource.volume = 0.5f;
    }

    public void BobaThrow()
    {
        audioSource.PlayOneShot(bobaThrowSound);
    }

    public void Swoosh()
    {
        audioSource.PlayOneShot(swooshSound);
    }

    public void Serve()
    {
        audioSource.PlayOneShot(serveSound);
    }

    public void Angry()
    {
        audioSource.PlayOneShot(angrySound);
    }

    public void Winning()
    {
        audioSource.PlayOneShot(winningSound);
    }
}
