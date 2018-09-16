using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerActions : MonoBehaviour
{

    // Movement Variables
    public float speed;
    public float holdDownTime;
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
        nearPlayer = false;

        
    }


    void Update()
    {

        CharacterMove();
        SprintCalc();
        CameraLook();
        ItemDistance();
    }

        void ItemDistance()
        {

            //distance between player and card
            float distance = Vector3.Distance(gameObject.transform.position, playerPos.position);
        if (distance <= 2.5f)
        {

            nearPlayer = true;
        }
        else
        {
            nearPlayer = false;
        }
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
        float horiz = Input.GetAxis("Horizontal") * Time.deltaTime * speed;
        float vert = Input.GetAxis("Vertical") * Time.deltaTime * speed;

        player.SimpleMove(new Vector3(horiz, 0f, vert));

    }
    void SprintCalc()
    {
        if (Input.GetButton("Use"))
            {
            speed = 250.0f;
        }
        else
        {
            speed= 100.0f;
        }
    }
    
}













