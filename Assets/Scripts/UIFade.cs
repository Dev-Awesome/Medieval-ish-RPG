using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngineInternal;

public class UIFade : MonoBehaviour
{
    public static UIFade instance;
    public Image fadeScreen;
    public float fadeSpeed;
    [SerializeField]
    private bool shouldFadeToBlack = false;
    [SerializeField]
    private bool shouldFadeFromBlack = false;
	// Use this for initialization
	void Start ()
	{
	    if (instance == null)
	    {
	        instance = this;
        }
        DontDestroyOnLoad(gameObject);
	}
	
	// Update is called once per frame
	void Update ()
	{
	    if (shouldFadeToBlack)
	    {
	        fadeScreen.color = new Color(fadeScreen.color.r, fadeScreen.color.g, fadeScreen.color.b, Mathf.MoveTowards(fadeScreen.color.a, 1f, fadeSpeed * Time.deltaTime));
	        if (fadeScreen.color.a == 1f)
	            shouldFadeToBlack = false;
	    }

	    else if (shouldFadeFromBlack)
	    {
	        fadeScreen.color = new Color(fadeScreen.color.r, fadeScreen.color.g, fadeScreen.color.b, Mathf.MoveTowards(fadeScreen.color.a, 0f, fadeSpeed * Time.deltaTime));
	        if (fadeScreen.color.a == 0f)
	            shouldFadeFromBlack = false;
        }
    }

    public void fadeToBlack()
    {
        shouldFadeToBlack = true;
        shouldFadeFromBlack = false;
    }

    public void fadeFromBlack()
    {
        shouldFadeToBlack = false;
        shouldFadeFromBlack = true;
    }
}
