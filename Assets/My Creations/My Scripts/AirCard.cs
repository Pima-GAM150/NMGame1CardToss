using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirCard : MonoBehaviour {

    public PlayerActions playerScript;

    
    public float throwForce;
    public float throwTorque;
    public float objectDistance;
    public float windAffect;

    public float carryRotationX;
    public float carryRotationY;
    public float carryRotationZ;
    public float carryRotationW;

    public float carryLocationX;
    public float carryLocationY;
    public float carryLocationZ;

    //AirCard's explosion stats
    public float explosionForce;
    public float explosionRadius;
    public float explosionUplift;


    //public CharacterController player;
    // variables for pick up and throw
    public GameObject playerObject;
    public Transform playerPos;
    public Vector3 playerInteractRange;
    public Transform playerCam;
    //public Quaternion cardRot;
    //whether the item is carried by player
    bool nearPlayer = false;
    bool beingCarried = false;
    bool touched = false;
    bool primed = false;



    void Start()
    {
        FindPlayer();
        nearPlayer = false;

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        PickUpnThrow();
    }

    void FindPlayer()
    {
        playerObject = GameObject.Find("Player");
        playerPos = playerObject.transform;
        playerCam = playerObject.transform.Find("Camera");
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



        if (nearPlayer && Input.GetMouseButton(1))
        {

            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.rigidbody != null)
                {
                    
                    GetComponent<Rigidbody>().isKinematic = true;
                    GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePosition;
                    transform.parent = playerCam;
                    beingCarried = true;
                    primed = true;
                }
                else
                {
                    GetComponent<Rigidbody>().isKinematic = false;
                    beingCarried = false;
                }
            }
            else
            {
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
                GetComponent<Rigidbody>().AddForce(playerCam.up * throwForce / 6);
                GetComponent<Rigidbody>().AddTorque(transform.forward * throwTorque * Time.deltaTime);               
                GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
            }



        }


    }
        
    
    private void OnTriggerEnter(Collider CardStick)
    {
        if (CardStick.gameObject.tag == "Target" && primed == true)
        {
            Vector3 spot = CardStick.gameObject.transform.position;
            
            CardStick.GetComponent<Rigidbody>().AddExplosionForce(explosionForce, spot, explosionRadius, explosionUplift);
            
        }
    }

    private void OnTriggerExit(Collider CardStick)
    {
        if (CardStick.gameObject.tag == "Target"&& primed == true)
        {
            primed = false;
        }
    }



}

