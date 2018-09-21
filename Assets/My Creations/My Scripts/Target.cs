using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour {

    //Rigidbody stuckItem;

	// Use this for initialization
	void Start () {
     
    }
	
	// Update is called once per frame
	void Update () {
		
	}
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "FireCard")
        {
            

        }
        
        
       
    }
}
