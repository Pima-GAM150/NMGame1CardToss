using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour {

    Rigidbody cardBody;

	// Use this for initialization
	void Start () {
     
    }
	
	// Update is called once per frame
	void Update () {
		
	}
    void OnTriggerEnter(Collider other)
    {
        
        cardBody.constraints = RigidbodyConstraints.FreezePosition;
    }
}
