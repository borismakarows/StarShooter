using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shooter : MonoBehaviour
{    
    [Header("Projectile")]
    [SerializeField] GameObject projectilePrefab;
    [SerializeField] float projectileSpeed = 5f;
    [SerializeField] float projectileLifetime = 10f;
    [SerializeField] float xPosDistanceLaser = 0.627f;
    [SerializeField] float yPosDistanceLaser = 0.422f;
    [Header("Fire")]
    [SerializeField] float firingRate = 0.2f;
    [SerializeField] float variantEnemyLasers = 0.1f;
    [SerializeField] float minFiringRate = 0.1f;
    public bool useAI;
    Coroutine firingCoroutine;
    [HideInInspector] public bool isFiring;
    AudioPlayer audioPlayer;
    
    void Awake()
    {
        audioPlayer = FindObjectOfType<AudioPlayer>();
    }
    
    void Start() 
    {
        if (useAI)
        {
            isFiring = true;
        }    
    }

    void Update()
    {
        Fire();
    }

    void Fire()
    {   
        if (isFiring && firingCoroutine == null)
        {
            firingCoroutine = StartCoroutine(FireContinuously());
        }
        else if (!isFiring && firingCoroutine != null)
        {
            StopCoroutine(firingCoroutine);
            firingCoroutine = null;
        }
    }

    IEnumerator FireContinuously()
    {
        while (true)
        {   
            if (!useAI)
            {
                audioPlayer.PlayPlayerShootingSFX();
                GameObject cloneLeft = Instantiate(projectilePrefab,
                                        transform.position + new Vector3(-xPosDistanceLaser,yPosDistanceLaser,0f),Quaternion.identity);
                GameObject cloneRight = Instantiate(projectilePrefab,
                                                    transform.position + new Vector3(xPosDistanceLaser,yPosDistanceLaser,0f),
                                                    Quaternion.identity);
                Rigidbody2D rb2dLeft = cloneLeft.GetComponent<Rigidbody2D>();
                Rigidbody2D rb2dRight = cloneRight.GetComponent<Rigidbody2D>();
                if (rb2dLeft != null && rb2dRight != null)
                {
                    rb2dLeft.velocity = Vector2.up * projectileSpeed;
                    rb2dRight.velocity = Vector2.up * projectileSpeed;
                }
                Destroy(cloneLeft,projectileLifetime);
                Destroy(cloneRight,projectileLifetime);
                yield return new WaitForSecondsRealtime(firingRate);
            }
            else
            {
                GameObject clone = Instantiate(projectilePrefab,transform.position,Quaternion.identity);
                Rigidbody2D cloneRb2D = clone.GetComponent<Rigidbody2D>();
                if (cloneRb2D != null)
                {
                    cloneRb2D.velocity = Vector2.down * projectileSpeed;
                }
                Destroy(clone,projectileLifetime);
                float randomRate =  UnityEngine.Random.Range(firingRate - variantEnemyLasers, firingRate + variantEnemyLasers);
                yield return new WaitForSecondsRealtime(Mathf.Clamp(randomRate,minFiringRate,float.MaxValue));
            }
        
        }
            
    }
}

