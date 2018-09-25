using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Target : MonoBehaviour
{

    public Transform playerCam;
    public int sceneIndex;
    bool nearPlayer;

  

    // code for round1 objective player just picks up or clicks on the box
    void Update()
    {
        DistanceCheck();
        PickUpCheck();

    }

    void DistanceCheck()
    {
        float distance = Vector3.Distance(gameObject.transform.position, playerCam.position);
        if (distance <= 2.5f)
        {
            nearPlayer = true;
        }
        else
        {
            nearPlayer = false;
        }
    }

    void PickUpCheck()
    {
        if (nearPlayer == true && Input.GetMouseButtonDown(1))
        {
            RaycastHit hit;
            Ray ray = playerCam.GetComponent<Camera>().ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.rigidbody.gameObject.tag == "Objective")
                {
                    SceneManager.LoadScene(sceneIndex);
                }
                else
                {


                }

            }
        }
    }
}


