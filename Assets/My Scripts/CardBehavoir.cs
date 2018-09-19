using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardBehavoir : MonoBehaviour
{

    public float speed;
    public float holdDownTime;
    public float throwForce;
    //public CharacterController player;
    // variables for pick up and throw
    public Transform playerPos;
    public Transform playerCam;
    //whether the item is carried by player
    bool nearPlayer = false;
    bool beingCarried = false;
    public int cardCount;
    public bool touched = false;



    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        PickUpnThrow();
    }

    void PickUpnThrow()
    {
        //distance between player and card to throw
        float distance = Vector3.Distance(gameObject.transform.position, playerPos.position);
        if (distance <= 2.5)
        {
            nearPlayer = true;
        }
        else nearPlayer = false;


        if (nearPlayer = true && Input.GetButtonDown("Use"))
        {
            GetComponent<Rigidbody>().isKinematic = true;
            transform.parent = playerCam;
            beingCarried = true;
            //transform.rotation = 
        }
        if (beingCarried)
        {
            if (touched)
            {
                GetComponent<Rigidbody>().isKinematic = false;
                transform.parent = null;
                beingCarried = false;
                touched = false;

            }
            if (Input.GetMouseButtonDown(0))
            {
                CalculateThrowForce();

                GetComponent<Rigidbody>().isKinematic = false;
                transform.parent = null;
                beingCarried = false;
                GetComponent<Rigidbody>().AddForce(playerCam.forward * throwForce);
            }

        }


    }
    void CalculateThrowForce()
    {
        holdDownTime += Time.deltaTime;
        throwForce *= holdDownTime;
    }
    private void OnTriggerEnter()
    {
        if (beingCarried)
        {
            touched = true;
        }
    }
}
