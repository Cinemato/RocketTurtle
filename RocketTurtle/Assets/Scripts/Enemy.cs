﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] float hp;
    [SerializeField] float spawnChance;
    [SerializeField] int scoreToSpawn;
    [SerializeField] int spawnLimit;
    [SerializeField] GameObject deathVFX = null;
    [SerializeField] AudioClip explosionSFX = null;
    [SerializeField] AudioClip hitSFX = null;
    [SerializeField] GameObject star = null;
    [SerializeField] AudioClip starCollectionSound = null;
    [SerializeField] int scoreAdditionAmount = 1;

    PlayerFire pf;
    ScoreManager sm;

    private void Start()
    {
        if (GameObject.FindGameObjectsWithTag(tag).Length - 1 >= spawnLimit && !CompareTag("FluffBall"))
        {
            Debug.Log(tag);
            EnemySpawner.count--;
            Destroy(gameObject);
        }

        sm = FindObjectOfType<ScoreManager>();
        pf = FindObjectOfType<PlayerFire>();
    } 

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.GetComponent<Projectile>())
        {
            Projectile projectile = collision.gameObject.GetComponent<Projectile>();           
            GameObject hitVFX = Instantiate(projectile.getHitVFX(), collision.gameObject.transform.position, Quaternion.identity);
            AudioSource.PlayClipAtPoint(hitSFX, Camera.main.transform.position, 4);
            Destroy(collision.gameObject);
            Destroy(hitVFX, 2f);
            
            recieveDamage(projectile.getDamage());
        }
    }

    void recieveDamage(float damage)
    {
        hp -= damage;

        if (hp <= 0)
        {
            if (GetComponent<EnemyProjectileMovement>() == null && FindObjectOfType<PlayerMovement>() != null)
            {
                ScoreManager.addToScore(scoreAdditionAmount);

                if(CompareTag("BigParrot"))
                {
                    Currency.addStars(1, star, transform.position, starCollectionSound);
                    sm.additionScore(scoreAdditionAmount);
                }

                if (ScoreManager.currentScore % 5 == 0)
                {
                    Currency.addStars(1, star, transform.position, starCollectionSound);
                }

            }

            explode();                                   
        }    
        
        else
        {
            pf.shakeCamera(0.05f, 0.05f);
        }
    }

    public void explode()
    {
        GameObject explodeVFX = Instantiate(deathVFX, transform.position, Quaternion.identity);
        Destroy(explodeVFX, 3f);
        if(!CompareTag("FluffBall"))
        {
            EnemySpawner.count--;
        }   
        AudioSource.PlayClipAtPoint(explosionSFX, Camera.main.transform.position, 0.6f);
        pf.shakeCamera(0.3f, 0.05f);
        Destroy(gameObject);
    }

    public float getSpawnChance()
    {
        return spawnChance;
    }

    public int getScoreToSpawn()
    {
        return scoreToSpawn;
    }

    public int getSpawnLimit()
    {
        return spawnLimit;
    }

    public float getHealth()
    {
        return hp;
    }

    public void setSpawnLimit(int spawnLimit)
    {
        this.spawnLimit = spawnLimit; 
    }
}
