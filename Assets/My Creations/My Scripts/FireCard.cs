using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireCard : MonoBehaviour {

    public float throwForce;
    public float throwTorque;
    public float objectDistance;
    

    public float carryRotationX;
    public float carryRotationY;
    public float carryRotationZ;
    public float carryRotationW;

    public float carryLocationX;
    public float carryLocationY;
    public float carryLocationZ;

    public GameObject fireEffect;

    //public CharacterController player;
    // variables for pick up and throw
    public Transform playerPos;
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
            RaycastHit hit;
            Ray ray = playerCam.GetComponent<Camera>().ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
                if (hit.rigidbody != null)
                {

                    GetComponent<Rigidbody>().isKinematic = true;
                    transform.parent = playerCam;
                    beingCarried = true;
                    GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePosition;
                }
                else
                {
                    GetComponent<Rigidbody>().isKinematic = false;
                    beingCarried = false;
                }
        }

            if (beingCarried)
            {

                transform.localPosition = new Vector3(carryLocationX, carryLocationY, carryLocationZ);
                transform.localRotation = new Quaternion(carryRotationX, carryRotationY, carryRotationZ, carryRotationW);


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
                    GetComponent<Rigidbody>().AddForce(playerCam.forward * throwForce / 2);
                    GetComponent<Rigidbody>().AddTorque(playerCam.transform.forward * throwTorque * Time.deltaTime);
                    GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
                }


            }
        


    }

    private void OnTriggerEnter(Collider CardStick)
    {
        if (CardStick.gameObject.tag == "Target")
        {
            Instantiate(fireEffect, CardStick.transform.position, Quaternion.identity);
        }
        if (CardStick.gameObject.tag == "Crate")
        {
            GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;

        }
        

    }
}
