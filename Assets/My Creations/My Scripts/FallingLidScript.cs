using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingLidScript : MonoBehaviour {

    public Transform lid;
    Rigidbody lidBody;



	// Use this for initialization
	void Start () {
        lid = GetComponent<Transform>();
        lidBody = GetComponent<Rigidbody>();
        lidBody.isKinematic = false;
	}
	
	// Update is called once per frame
	void Update () {
        if (lid.rotation.x == 0)
        {
            lidBody.isKinematic = true;
        }

	}
}
