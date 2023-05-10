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
    public AudioClip losingSound;
    public AudioClip slurpSound;
    public AudioClip splashSound;


    public enum Sound
    {
        coinSound,
        bobaThrowSound,
        swooshSound,
        serveSound,
        angrySound,
        winningSound,
        losingSound,
        slurpSound,
        splashSound,
    }

    public void Coin()
    {
        audioSource.PlayOneShot(coinSound);
        audioSource.volume = 0.5f;
    }

    public void BobaThrow()
    {
        audioSource.PlayOneShot(bobaThrowSound);
        audioSource.volume = 1f;
    }

    public void Swoosh()
    {
        audioSource.PlayOneShot(swooshSound);
        audioSource.volume = 1f;
    }

    public void Serve()
    {
        audioSource.PlayOneShot(serveSound);
        audioSource.volume = 1f;
    }

    public void Angry()
    {
        audioSource.PlayOneShot(angrySound);
        audioSource.volume = 1f;
    }

    public void Winning()
    {
        audioSource.PlayOneShot(winningSound);
        audioSource.volume = 1f;
    }

    public void Losing()
    {
        audioSource.PlayOneShot(losingSound);
        audioSource.volume = 1f;
    }
    public void Slurp()
    {
        audioSource.PlayOneShot(slurpSound);
        audioSource.volume = 3f;
    }

    public void Splash()
    {
        audioSource.PlayOneShot(splashSound);
        audioSource.volume = 1f;
    }
}
