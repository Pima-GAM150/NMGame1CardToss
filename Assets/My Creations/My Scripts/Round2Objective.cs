using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Round2Objective : MonoBehaviour {

    
    public int sceneIndex;
    public float time;
    bool hit = false;

    void Start()
    {
        time = 0.0f;
    }
    // round2 objective. player needs to set fire to this object and after 5 seconds then scene change to 3
    void Update () {
        if (hit == true)
        {
            time += Time.deltaTime;
        }
        if (time >= 5.0f)
        {
            SceneManager.LoadScene(sceneIndex);
        }
		
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "FireCard")
        {
            hit = true;
        }
    }

}
