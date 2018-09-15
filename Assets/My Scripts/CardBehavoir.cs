using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardBehavoir : MonoBehaviour
{

    public float speed;
    public float holdDownTime;
    public float throwForce;
    public float throwTorque;
    public float objectDistance;
    public float windAffect;

    public float beingCarriedRotationX;
    public float beingCarriedRotationY;
    public float beingCarriedRotationZ;
    public float beingCarriedRotationW;

    public float beingCarriedLocationX;
    public float beingCarriedLocationY;
    public float beingCarriedLocationZ;
    


    //public CharacterController player;
    // variables for pick up and throw
    public Transform playerPos;
    public Vector3 playerInteractRange;
    public Transform playerCam;
    //public Quaternion cardRot;
    //whether the item is carried by player
    bool nearPlayer = false;
    bool beingCarried = false;
    bool touched = false;



    void Start()
    {
        
        nearPlayer = false;

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        PickUpnThrow();
    }

    //void objectDistanceCheck()
    //{
    //    //distance between player and card to throw
    //   objectDistance = Vector3.Distance(cardBody.position, playerPos.position);
    //    if (objectDistance < 2.5f)

    //    {
    //        nearPlayer = true;
    //    }
    //    else nearPlayer = false;

    //}





    void PickUpnThrow()
    {
        //objectDistanceCheck();
        //distance between player and card to throw        
        float distance = Vector3.Distance(gameObject.transform.position, playerPos.position);
        if (distance <= 5.0f)
        {

            nearPlayer = true;
        }
        else
        {
            nearPlayer = false;
        }



        if (nearPlayer && Input.GetMouseButtonDown(1))
        {
            
            GetComponent<Rigidbody>().isKinematic = true;
            transform.parent = playerCam;        
                   
            beingCarried = true;
            

        }
        if (beingCarried)
        {
            transform.localPosition = new Vector3(beingCarriedLocationX, beingCarriedLocationY, beingCarriedLocationZ);
            transform.localRotation = new Quaternion(beingCarriedRotationX, beingCarriedRotationY, beingCarriedRotationZ, beingCarriedRotationW);

            //this drops the item if it comes in contact with another item.
            if (touched)
            {
                GetComponent<Rigidbody>().isKinematic = false;
                transform.parent = null;
                beingCarried = false;
                touched = false;
            }

            if (Input.GetMouseButtonDown(0))
            {
                GetComponent<Rigidbody>().isKinematic = false;
                    transform.parent = null;
                    beingCarried = false;
                //the throwing calculation
                GetComponent<Rigidbody>().AddForce(playerCam.forward * throwForce);
                


                //the throwing item's tag determines the throwing pattern. the knife needs work
                if (gameObject.tag == "Knife")
                {
                    GetComponent<Rigidbody>().AddTorque(transform.up * throwTorque * Time.deltaTime);
                    GetComponent<Rigidbody>().AddForce(playerCam.up * throwForce / 2.0f);
                }
                if (gameObject.tag == "Card")
                GetComponent<Rigidbody>().AddTorque(transform.forward * throwTorque * Time.deltaTime);
                
                if (gameObject.tag == "Star")
                {
                    GetComponent<Rigidbody>().AddTorque(transform.up * throwTorque * Time.deltaTime);                }
                
                //the wind affect calculation
                //GetComponent<Rigidbody>().AddForce(playerCam.forward * throwForce);
                //GetComponent<Rigidbody>().AddTorque(transform.up * 1); 
                GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
            }



        }


    }
    //void CalculateThrowForce()
    //{
     
    //    throwForce *= holdDownTime;

    //}
    private void OnTriggerEnter(Collider CardStick)
    {
        if (CardStick.gameObject.tag == "Target")
        {
            GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
            if (beingCarried)
            {
                touched = true;

                GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;


            }
        }
    }

    
   
}
