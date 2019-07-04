using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AreaExit : MonoBehaviour {

    public string sceneToLoad;

    public string areaTransitionName;
    public float waitToLoad = 1f;
    public AreaEntrance theEntrance;

    private bool shouldLoadAfterWait;
	// Use this for initialization
	
	// Update is called once per frame
	void Update () {
	    if (shouldLoadAfterWait)
	    {
	        waitToLoad -= Time.deltaTime;
	        if (waitToLoad <= 0)
	        {
	            shouldLoadAfterWait = false;
                SceneManager.LoadScene(sceneToLoad);
            }
	    }
	}

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            shouldLoadAfterWait = true;
            UIFade.instance.fadeToBlack();
            PlayerController.instance.areaTransitionName = areaTransitionName;
        }
    }
}
