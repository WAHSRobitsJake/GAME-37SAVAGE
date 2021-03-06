using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bossScript : MonoBehaviour
{
    public enum boss
    {
        E,
        Ryan,
        ChrisChan,
        DummyThicc
    }
    public boss currentBoss;
    public boss currentPartnerBoss;

    public GameObject soundObj;
    public GameObject partnerBossObject;
    public GameObject player;
    public GameObject stairs;

    public Vector2 target;

    public string currentMode;
    public string selfState;
    
    public int health;

    public float coolDownTimer = 10.0f;
    public bool abilityUsed = false;
    public bool chrisAlive, jakeAlive, ryanAlive, elijahAlive = true;
    public bool partnerAlive;
    void Start()
    {
        {
            Invoke("newStart", 0.1f);
        }

        if (this.gameObject.CompareTag("E"))
        {
            currentBoss = boss.E;
            health = 50;
            partnerBossObject = GameObject.FindGameObjectWithTag("Ryan");
            currentPartnerBoss = boss.Ryan;
        }
        if (this.gameObject.CompareTag("Ryan"))
        {
            currentBoss = boss.Ryan;
            health = 30;
            partnerBossObject = GameObject.FindGameObjectWithTag("E");
            currentPartnerBoss = boss.E;
        }
        if (this.gameObject.CompareTag("ChrisChan"))
        {
            currentBoss = boss.ChrisChan;
            health = 40;
            partnerBossObject = GameObject.FindGameObjectWithTag("DummyThicc");
            currentPartnerBoss = boss.DummyThicc;
        }
        if (this.gameObject.CompareTag("DummyThicc"))
        {
            currentBoss = boss.DummyThicc;
            health = 20;
            partnerBossObject = GameObject.FindGameObjectWithTag("ChrisChan");
            currentPartnerBoss = boss.ChrisChan;
        }
        soundObj = GameObject.FindGameObjectWithTag("soundObj");
    }
    void Update()
    {
        if (health <= health / 2)
        {
            selfState = "panicMode";
            if (partnerAlive)
            {
                partnerBossObject.GetComponent<bossScript>().selfState = "panicMode";
            }
        }
        if (currentBoss == boss.E)
        {
            coolDownTimer -= 1 * Time.deltaTime;
            if (coolDownTimer <= 0 && selfState == "calmMode" && !abilityUsed)
            {
                calmMode();
                coolDownTimer = 12.5f;
            }
            if (coolDownTimer <= 0 && selfState == "panicMode" && !abilityUsed)
            {
                calmMode();
                coolDownTimer = 5.0f;
            }
            if (health <= 0)
            {
                bossDeath();
            }
        }
        if (currentBoss == boss.Ryan)
        {
            coolDownTimer -= 1 * Time.deltaTime;
            if (coolDownTimer <= 0 && selfState == "calmMode" && !abilityUsed)
            {
                calmMode();
                coolDownTimer = 15.0f;
            }
            if (coolDownTimer <= 0 && selfState == "panicMode" && !abilityUsed)
            {
                calmMode();
                coolDownTimer = 5.0f;
            }
            if (health <= 0)
            {
                bossDeath();
            }
        }
        if (currentBoss == boss.ChrisChan)
        {
            
            coolDownTimer -= 1 * Time.deltaTime;
            if (coolDownTimer <= 0 && selfState == "calmMode" && !abilityUsed)
            {
                calmMode();
                coolDownTimer = 10.0f;
            }
            if (coolDownTimer <= 0 && selfState == "panicMode" && !abilityUsed)
            {
                calmMode();
                coolDownTimer = 5.0f;
            }
            if (health <= 0)
            {
                bossDeath();
            }
        }
        if (currentBoss == boss.DummyThicc)
        {
            
            coolDownTimer -= 1 * Time.deltaTime;
            if (coolDownTimer <= 0 && selfState == "calmMode" && !abilityUsed)
            {
                calmMode();
                coolDownTimer = 10.0f;
            }
            if (coolDownTimer <= 0 && selfState == "panicMode" && !abilityUsed)
            {
                calmMode();
                coolDownTimer = 5.0f;
            }
            if (health <= 0)
            {
                bossDeath();
            }
        }

    }
    public void bossDeath()
    {
        if (!partnerAlive)
        {
            Instantiate(stairs, transform.position, Quaternion.identity);
            Destroy(this.gameObject);
        }
        else
        {
            partnerBossObject.GetComponent<bossScript>().partnerAlive = false;
            Destroy(this.gameObject);
        }
    }
    //FOR TESTING ONLY
    public void newStart()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        selfState = "calmMode";
    }
    public void calmMode()
    {
        switch (currentBoss)
        {
            case boss.E:
                int eliRand = Random.Range(0, 20);
                if (eliRand <= 10)
                {
                    this.gameObject.GetComponent<FlockerScript>().SpeedPerSecond = 0;
                    this.gameObject.GetComponentInChildren<ParticleSystem>().Play();
                    this.gameObject.GetComponentInChildren<AudioSource>().Play();
                    abilityUsed = true;
                    Invoke("callReTarget", 7.4f);
                }
                /*else if (eliRand > 5 && eliRand <= 10)
                {
                    //Attack 2 with burst particles at player, variation 1 (easy)
                    abilityUsed = true;
                }
                else if (eliRand > 10 && eliRand <= 15)
                {
                    //Attack 2 with burst particles at player, variation 2 (hard)
                    abilityUsed = true;
                }*/
                else if (eliRand > 10 && eliRand <= 20)
                {
                    //Instantiate Hurt Things
                    this.gameObject.GetComponent<FlockerScript>().SpeedPerSecond = 0;
                    this.gameObject.GetComponent<eScript>().SplurshAttack();
                    abilityUsed = true;
                    Invoke("callReTarget", 7.4f);
                }


                break;
            case boss.Ryan:
                int ryanRand = Random.Range(0, 10);
                if (ryanRand >= 9)
                {
                    this.gameObject.GetComponent<ryanScript>().prepAttack();
                    this.gameObject.GetComponent<ryanScript>().attack = "dashAttack";
                    this.gameObject.GetComponent<FlockerScript>().SpeedPerSecond *= 0.75f;
                } else
                {
                    this.gameObject.GetComponent<ryanScript>().prepAttack();
                    this.gameObject.GetComponent<ryanScript>().attack = "slashStorm";
                    this.gameObject.GetComponent<FlockerScript>().SpeedPerSecond *= 0.0f;
                }
                break;
            case boss.ChrisChan:
                int chrisRand = Random.Range(0, 10);
                if (chrisRand <= 3)
                {
                    this.gameObject.GetComponent<FlockerScript>().FlockingTarget = this.gameObject;
                    this.gameObject.GetComponent<chrisChanBOSS>().jumpSlam();
                    abilityUsed = true;
                    Invoke("callReTarget", 5.0f);
                } else {
                    this.gameObject.GetComponent<FlockerScript>().FlockingTarget = this.gameObject;
                    this.gameObject.GetComponent<chrisChanBOSS>().jumpKick();
                    abilityUsed = true;
                    Invoke("callReTarget", 5.0f);
                } 
                break;
            case boss.DummyThicc:
                this.gameObject.GetComponent<dummyThiccBOSS>().healChris();
                break;
        }
    }
    public void panicMode()
    {
        switch (currentBoss)
        {
            case boss.E:
                //Elijah's funcitonality
                break;
            case boss.Ryan:
                //Ryan's functionality
                break;
            case boss.ChrisChan:
                //Chris' functionality
                break;
            case boss.DummyThicc:
                //Jake's functionality
                break;
        }
    }
    public void callReTarget()
    {
        this.gameObject.GetComponent<FlockerScript>().reTargetPlayer();
        if (this.gameObject.CompareTag("E"))
        {
            this.gameObject.GetComponent<FlockerScript>().SpeedPerSecond = 4.0f;
        }
        abilityUsed = false;
    }
    public void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("pBullet"))
        {
            health--;
        }
    }
}
