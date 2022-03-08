using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPlayer : MonoBehaviour
{
    [Header("Shooting")]
    [SerializeField] AudioClip playerShootingLeftSFX;
    [SerializeField] AudioClip playerShootingRightSFX;
    [SerializeField] [Range(0f,1f)] float shootingVolume = 0.5f;
    [Header("Damage")]
    [SerializeField] AudioClip damageTakenSFX;
    [SerializeField] AudioClip damageGivenSFX;
    [SerializeField] [Range(0,1f)] float damageTakenVolume;
    [SerializeField] [Range(0,1f)] float damageGivenVolume;

    void Awake()
    {
        ManageSingleton();    
    }

    void ManageSingleton()
    {
        int instanceCount = FindObjectsOfType(GetType()).Length;
        if (instanceCount > 1)
        {
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    public void PlayPlayerShootingSFX()
    {
        PlayClip(playerShootingLeftSFX,shootingVolume);
        PlayClip(playerShootingRightSFX,shootingVolume);
    }

    public void PlayDamageTakenSFX()
    {
        PlayClip(damageTakenSFX,damageTakenVolume);
    }

    public void PlayDamageGivenSFX()
    {
        PlayClip(damageGivenSFX,damageGivenVolume);
    }

    void PlayClip(AudioClip clip,float volume)
    {
        if (clip != null)
        {
            Vector3 camerapos = Camera.main.transform.position;
            AudioSource.PlayClipAtPoint(clip,camerapos,volume);
        }
    }

}