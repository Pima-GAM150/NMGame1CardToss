using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieAI : MonoBehaviour {

    public Transform player;
    static Animator anim;
   
	
	void Start () {
        anim = GetComponent<Animator>();
	}

    // Update is called once per frame
    void Update()
    {

        //find player
        float distanceToPlayer = Vector3.Distance(player.position, this.transform.position);

        if (distanceToPlayer < 15)
        {
            Vector3 directToPlayer = player.position - this.transform.position;
            directToPlayer.y = 0;

            this.transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(directToPlayer), 0.1f);

            anim.SetBool("isIdol", false);
            anim.SetBool("isHit", false);

            if (directToPlayer.magnitude > 5)
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
    private void OnTriggerEnter(Collider other)
    {
        //if (gameObject.tag == "Star")
        //{
            anim.SetBool("isHit", true);
            player = null;
        //}
    }

}



