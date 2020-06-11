﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    Rigidbody2D rb;
    BulletManager bm;

    //Will Get Input From Bullet Manager
    float speed;
    float timeTilNext;

    void Start()
    {
        //Defining Bullet Manager
        bm = FindObjectOfType<BulletManager>();

        //Getting Different Results From BM Depending On Ammo Used
        if(gameObject.tag == "CannonBall")
        {
            speed = bm.getCannonBallSpeed();
            timeTilNext = bm.getCannonBallTime();
        }

        else if(gameObject.tag == "Laser")
        {
            speed = bm.getLaserSpeed();
            timeTilNext = bm.getLaserTime();
        }
    }


    void Update()
    {
        //Moving Projectile To The Right Using Rigidbody
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = new Vector2(speed, 0);
    }

    public float getTime()
    {
        //To Be Used In BM Class
        return timeTilNext;
    }
}