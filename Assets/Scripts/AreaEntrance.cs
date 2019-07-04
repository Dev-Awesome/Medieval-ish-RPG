using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaEntrance : MonoBehaviour
{
    // Use this for initialization
    void Start()
    {
        var transitionName = transform.parent.GetComponent<AreaExit>().areaTransitionName;

        if (transitionName == PlayerController.instance.areaTransitionName)
        {
            PlayerController.instance.transform.position = transform.position;
        }

        UIFade.instance.fadeFromBlack();
    }

    // Update is called once per frame
    void Update () {
		
	}
}
