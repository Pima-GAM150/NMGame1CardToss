using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChange : MonoBehaviour {

    public int sceneIndex;



	
	void Start () {
		

	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.P))
        {
            SceneManager.LoadScene(sceneIndex);
        }

        if (gameObject == null)
        {
            SceneManager.LoadScene(sceneIndex);
        }

	}
    private void OnTriggerEnter(Collider other)
    {
        
    }
}
