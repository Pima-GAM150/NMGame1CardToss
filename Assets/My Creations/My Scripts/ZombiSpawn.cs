using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombiSpawn : MonoBehaviour {

    public GameObject player;
    public GameObject spawnableZombie;
    public GameObject activeZombie;
    public float playerDistance;
    public float power;
    Vector3 spawnImpulsePower;

    Transform spawnSpot;
    Transform coffinLid;
    bool spawnedZombie = false;

	// Use this for initialization
	void Start () {
        player = GameObject.Find("Player");
        spawnSpot = gameObject.transform.Find("SpawnSpot");
        coffinLid = gameObject.transform.Find("Lid");
	}
	
	// Update is called once per frame
	void Update () {
        
        DistanceCheck();
        SpawnCheck();
        ZombieCheck();
	}



    void DistanceCheck()
    {
        playerDistance = Vector3.Distance(gameObject.transform.position, player.transform.position);
    }

    void SpawnCheck()
    {

        if (playerDistance <= 10f && spawnedZombie == false)
        {
            Instantiate(spawnableZombie, spawnSpot.transform.position, spawnSpot.transform.rotation);
            activeZombie = spawnableZombie;
            
            spawnedZombie = true;
            coffinLid.GetComponent<Rigidbody>().AddForce(coffinLid.up * power, ForceMode.Impulse);


        }
    }

    void ZombieCheck()
    {
        if (activeZombie == null)
        {
            spawnedZombie = false;

        }
    }

}
