using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;
    [SerializeField] AudioSource audioSource;

    public AudioClip coinSound;
    public AudioClip bobaThrowSound;


    public enum Sound
    {
        coinSound,
        bobaThrowSound,
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
}
