using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public int health = 50;
    [SerializeField] ParticleSystem hitEffect;
    [SerializeField] bool applyCameraShake;
    CameraShake cameraShake;
    AudioPlayer audioPlayer;
    shooter shooter;
    uiscript uiscript;
    LevelManager levelManager;

    void Awake() 
    {
        cameraShake = Camera.main.GetComponent<CameraShake>();
        audioPlayer = FindObjectOfType<AudioPlayer>();    
        shooter = GetComponent<shooter>();
        uiscript = FindObjectOfType<uiscript>();
        levelManager = FindObjectOfType<LevelManager>();
    }

    
    void OnTriggerEnter2D(Collider2D other)
    {
        DamageDealer damageDealer = other.GetComponent<DamageDealer>();
        if (damageDealer != null)
        {            
            TakeDamage(damageDealer.GetDamage());
            ShakeCamera();
            playHitEffect();
            damageDealer.Hit();
            if (other.gameObject.tag == "1hit")
            {
                audioPlayer.PlayDamageTakenSFX();
                uiscript.DeleteHeart(1);
            }
            else if (other.gameObject.tag == "2hit")
            {
                audioPlayer.PlayDamageTakenSFX();
                uiscript.DeleteHeart(2);
            }
            else
            {
                audioPlayer.PlayDamageGivenSFX();
            }
        }    
    }

    void ShakeCamera()
    {
        if (cameraShake != null && applyCameraShake)
        {
            cameraShake.Play();
        }
    }

    void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Destroy(gameObject);
            if (gameObject.tag == "Player")
            {
                levelManager.LoadEndMenu();
            }
            else
            {
                uiscript.AddScore(10);
            }
        }
    }

    void playHitEffect()
    {
        if (hitEffect != null)
        {
            ParticleSystem clone = Instantiate(hitEffect,transform.position,Quaternion.identity);
            Destroy(clone.gameObject,clone.main.duration + clone.main.startLifetime.constantMax);
            
        }
    }

    public int GetHealth()
    {
        return health;
    }

}
