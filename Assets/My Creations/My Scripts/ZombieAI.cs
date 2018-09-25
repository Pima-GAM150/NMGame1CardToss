using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieAI : MonoBehaviour {

    public GameObject playerObject;
    static Animator anim;

    Transform player;
    public float distanceToPlayer; 
    public float frozenTime;
    bool frozen = false;
	
	void Start () {
        anim = GetComponent<Animator>();
        
        frozenTime = 0.0f;

        playerObject = GameObject.Find("Player");

        player = playerObject.GetComponent<Transform>();        
        
	}

    
    void Update()
    {
        MainZombieFunction();
    }


    void MainZombieFunction()
    {
        //find player


        distanceToPlayer = Vector3.Distance(player.position, transform.position);

        if (distanceToPlayer < 15)
        {
            Vector3 directToPlayer = player.position - transform.position;
            directToPlayer.y = 0;

            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(directToPlayer), 0.1f);

            anim.SetBool("isIdol", false);
            anim.SetBool("isHit", false);

            if (directToPlayer.magnitude > 4)
            {
                anim.SetBool("isWalking", true);
                anim.SetBool("isAttacking", false);
                anim.SetBool("isHit", false);
               
            }
            else
            {
                anim.SetBool("isAttacking", true);
                anim.SetBool("isWalking", false);
                anim.SetBool("isHit", false);
            }
        }
        else
        {
            anim.SetBool("isIdol", true);
            anim.SetBool("isWalking", false);
            anim.SetBool("isAttacking", false);
            anim.SetBool("isHit", false);
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
            anim.SetBool("isHit", true);
            anim.SetBool("isIdol", false);
            anim.SetBool("isWalking", false);
            anim.SetBool("isAttacking", false);
            player = null;
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
            player = null;
            anim.SetBool("isIdol", false);
            anim.SetBool("isWalking", false);
            anim.SetBool("isAttacking", false);
            anim.SetBool("isHit", true);


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

        anim.SetBool("isHit", false);
        anim.SetBool("isIdol", true);
        anim.SetBool("isWalking", false);
        anim.SetBool("isAttacking", false);


    }



}



