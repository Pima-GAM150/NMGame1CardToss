using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerActions : MonoBehaviour
{
    

    // Movement Variables
    public float walkSpeed;
    public float sprintSpeed;
    float currentSpeed;
    
    public float throwForce;
    public CharacterController player;
    //variables for pick up and throw
    public Transform playerPos;
    public Transform playerCam;
    //whether the item is carried by player
    public bool nearPlayer = false;
    
    public int cardCount;
    public bool touched = false;
    
    public GameObject itemToThrow;
    public Transform cameraMovement;



    void Start()
    {
        currentSpeed = walkSpeed;
        nearPlayer = false;        
    }


    void Update()
    {

        CharacterMove();
        SprintCalc();
        CameraLook();
        FindItemDistance();
        
    }

        void FindItemDistance()
        {
        itemToThrow = GameObject.Find("");
        
        }
          
    void CameraLook()
    {

        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        Vector3 rotation = cameraMovement.eulerAngles;
        rotation.x += -mouseY;
        rotation.y += mouseX;
        cameraMovement.eulerAngles = rotation;

    }

    void CharacterMove()
    {
        float horiz = Input.GetAxis("Horizontal") * Time.deltaTime * currentSpeed;
        float vert = Input.GetAxis("Vertical") * Time.deltaTime * currentSpeed;
        Vector3 localMotion = playerCam.transform.TransformDirection(new Vector3(horiz, 0f, vert));
        

        player.SimpleMove(localMotion);

    }
    void SprintCalc()
    {
        if (Input.GetButton("Use"))
            {
            currentSpeed = sprintSpeed;
        }
        else
        {
            currentSpeed= walkSpeed;
        }
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Zombie")
        {
            gameObject.GetComponent<CapsuleCollider>().isTrigger = false;
        }
    }
}













