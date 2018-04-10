﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeMovement : MonoBehaviour {

    private Rigidbody phys;
    private float AITrigger;
    public float AIcooldown = 2;
    public float enthusiasim  = 1;
    private float currentEnthus = 0;
    public float jumpHeight = 2;
    public float moveSpeed = 2;
    private Vector2 aim;
    public GameObject targetLocation;
    public float triggerDistance = 10;
    // Use this for initialization
    void Start()
    {
        phys = this.GetComponent<Rigidbody>();
        AITrigger = -AIcooldown;
        aim = Random.insideUnitCircle.normalized;
    }

    // Update is called once per frame
    void Update()
    {
        float jump = 0;
        if (AITrigger + Random.value > 0)
        {
            currentEnthus = enthusiasim;
            float aimDecision = Random.value;
            if (aimDecision > .8)
            {
                
                //aim at player
                Vector3 dif = this.transform.position - GameDirector.Director.Player.transform.position;
                if(triggerDistance<dif.magnitude)
                    aim = -new Vector2(dif.x, dif.z).normalized;
                else
                    aim = Random.insideUnitCircle.normalized;
            }
            else if (aimDecision > .7 && targetLocation != null)
            {
                Vector3 dif = this.transform.position - targetLocation.transform.position;
                aim = -new Vector2(dif.x, dif.z).normalized;
            }
            else if (aimDecision > .3)
            {
                //aim randomly
                aim = Random.insideUnitCircle.normalized;
            }
            else
            {
                //no change in aim form previous
            }
            if (Random.value > .8)
            {
                jump = jumpHeight;
            }
            AITrigger = -AIcooldown;
        }
        else
        {
            AITrigger += Time.deltaTime;
        }
        
        phys.velocity = new Vector3(aim.x * currentEnthus * moveSpeed, phys.velocity.y + jump,aim.y * currentEnthus * moveSpeed);
        currentEnthus -= Time.deltaTime*1.1f;
    }
}