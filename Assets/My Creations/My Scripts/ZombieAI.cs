using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieAI : MonoBehaviour {

    public GameObject playerObject;
    static Animator anim;

    Transform player;
    public float distanceToPlayer; 
    public float frozenTime;
    public float fireTime;
    public float earthDownTime;

    public bool onFire = false;
    bool frozen = false;
    public bool earthHit = false;
	
	void Start () {

        anim = GetComponent<Animator>();
        
        frozenTime = 0.0f;
        fireTime = 0.0f;
        earthDownTime = 0.0f;

        ZombieFindPlayer();

	}

    
    void Update()
    {
        if (player == null)
        {
            if (onFire == true)
            {
                ZombieFallsOnBackAnimation();
                fireTime += Time.deltaTime;
                if (fireTime >= 5.0f)
                {
                    GetComponent<BoxCollider>().isTrigger = true;
                    if (fireTime >= 6.0f)

                    {
                        Destroy(gameObject);
                    }


                }
            }


            if (earthHit == true)
            {
                ZombieFallsOnBackAnimation();
                earthDownTime += Time.deltaTime;

                if (earthDownTime >= 5f)
                {
                    anim.SetBool("isHit", false);
                    anim.SetBool("isIdol", true);
                    anim.SetBool("isWalking", false);
                    anim.SetBool("isAttacking", false);
                    earthHit = false;
                    earthDownTime = 0f;
                    ZombieFindPlayer();
                    MainZombieFunction();

                }
            }


            //if the zombie hasnt been hit by anything but has lost the play for some reason. the zombie will stand still.
            else
            {
                anim.SetBool("isHit", false);
                anim.SetBool("isIdol", true);
                anim.SetBool("isWalking", false);
                anim.SetBool("isAttacking", false);

            }
        }
        // if the player object isnt null then it will do the main function
        else
        {
            MainZombieFunction();
        }

       
        
            


    }

    void ZombieFindPlayer()
    {
        playerObject = GameObject.Find("Player");
        player = playerObject.GetComponent<Transform>();
    }

    void MainZombieFunction()
    {
        //find player 
        
        distanceToPlayer = Vector3.Distance(player.position, transform.position);
        

        if (distanceToPlayer < 20)
        {
            Vector3 directToPlayer = player.position - transform.position;
            directToPlayer.y = 0;
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(directToPlayer), 0.1f);

            anim.SetBool("isIdol", false);
            anim.SetBool("isHit", false);

            if (directToPlayer.magnitude > 3)
            {
                ZombieWalkingAnimation();
            }
            else
            {
                ZombieAttackAnimation();
            }
        }
        else
        {
            ZombieIdolAnimation();
        }
    }


    void FreezeTimerStart()
    {
        if (frozen == true)
        {
            frozenTime += Time.deltaTime;
        }
        if (frozenTime >= 6.0f)
        {
            gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
            player = playerObject.GetComponent<Transform>();
            frozenTime = 0.0f;
            frozen = false;
            MainZombieFunction();

        }


    }
    

    

    //when zomboy gets hit by a card it will check its tag and react accordingly
    private void OnTriggerEnter(Collider other)
    {
        //earthcard knocks down zombie
        if (other.gameObject.tag == "EarthCard")
        {
            EarthCardHit();            
        }

        //ice card will freeze zombie
        if (other.gameObject.tag == "IceCard")
        {
            IceCardHit();
        }            
        
        //aircard will push the zombie in its own code
        if (other.gameObject.tag == "AirCard")
        {

        }

        //firecard will burn the zombie by creating a fire prefab and destory zombie.
        if (other.gameObject.tag == "FireCard")
        {
            FireCardHit();
        }

    }

    //these are the effects of cards, icecard first
    void IceCardHit()
    {
        frozen = true;

        if (frozen == true)
        {
            FrozenZombie();
            player = null;
            FreezeTimerStart();
            
        }

    }

    void FrozenZombie()
    {
        gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePosition;
        ZombieIdolAnimation();       


    }

   void FireCardHit()
    {  
    
        onFire = true;
        player = null;
        
    
    }

    void EarthCardHit()
    {
        earthHit = true;
    }


    void ZombieFallsOnBackAnimation()
    {
        anim.SetBool("isHit", true);
        anim.SetBool("isIdol", false);
        anim.SetBool("isWalking", false);
        anim.SetBool("isAttacking", false);
    }

    void ZombieWalkingAnimation()
    {
        anim.SetBool("isWalking", true);
        anim.SetBool("isAttacking", false);
        anim.SetBool("isHit", false);
        anim.SetBool("isIdol", false);
    }
    void ZombieAttackAnimation()
    {
        anim.SetBool("isAttacking", true);
        anim.SetBool("isWalking", false);
        anim.SetBool("isHit", false);
        anim.SetBool("isIdol", false);
    }
    void ZombieIdolAnimation()
    {
        anim.SetBool("isIdol", true);
        anim.SetBool("isWalking", false);
        anim.SetBool("isAttacking", false);
        anim.SetBool("isHit", false);
    }

}



