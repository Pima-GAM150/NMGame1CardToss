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

    public Rigidbody cardBody;

    //public CharacterController player;
    // variables for pick up and throw
    public Transform playerPos;
    public Vector3 playerInteractRange;
    public Transform playerCam;
    //public Quaternion cardRot;
    //whether the item is carried by player
    bool nearPlayer = false;
    bool beingCarried = false;
    public int cardCount;
    public bool touched = false;



    void Start()
    {
        cardBody = GetComponent<Rigidbody>();


    }

    // Update is called once per frame
    void Update()
    {
        PickUpnThrow();
    }

    void objectDistanceCheck()
    {
        //distance between player and card to throw
       objectDistance = Vector3.Distance(cardBody.position, playerPos.position);
    }



    void PickUpnThrow()
    {
        objectDistanceCheck();
        //distance between player and card to throw
        //float distance = Vector3.Distance(cardBody.position, playerPos.position);

        if (objectDistance < 2.5f)

        {
            nearPlayer = true;
        }
        else nearPlayer = false;


        if (nearPlayer = true && Input.GetButtonDown("Use"))
        {
            GetComponent<Rigidbody>().isKinematic = true;
            transform.parent = playerCam;
            
            transform.localPosition = new Vector3(0f, -.25f, .5f);
            transform.localRotation = new Quaternion(beingCarriedRotationX, beingCarriedRotationY, beingCarriedRotationZ, beingCarriedRotationW);

            beingCarried = true;
            cardBody.constraints = RigidbodyConstraints.None;

        }
        if (beingCarried)
        {
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
                
                //    CalculateThrowForce();
                //}
                //if (Input.GetMouseButtonUp(0))
                {
                    GetComponent<Rigidbody>().isKinematic = false;
                    transform.parent = null;
                    beingCarried = false;
                    //the throwing calculation
                    GetComponent<Rigidbody>().AddForce(playerCam.forward * throwForce);
                    GetComponent<Rigidbody>().AddTorque(transform.forward * throwTorque);
                    //the wind affect calculation
                    GetComponent<Rigidbody>().AddForce(playerCam.forward * throwForce);
                    GetComponent<Rigidbody>().AddTorque(transform.up * 1);
                }
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
            cardBody.constraints = RigidbodyConstraints.FreezeAll;
            if (beingCarried)
            {
                touched = true;

                cardBody.constraints = RigidbodyConstraints.FreezeAll;


            }
        }
    }

    
   
}
