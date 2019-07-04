using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed;
    public Rigidbody2D rb;

    public Animator myAnim;
    public bool canMove = true;
    public static PlayerController instance;
    private Vector3 bottomLeftLimit;
    private Vector3 topRightLimit;
    public string areaTransitionName;
	// Use this for initialization
	void Start ()
	{
	    if (instance == null)
	    {
	        instance = this;
	    }

	    else
	    {
	        if (instance != this)
	        {
	            Destroy(gameObject);
            }
	    }
	    DontDestroyOnLoad(gameObject);
	}
	
	// Update is called once per frame
	void Update ()
	{
	    if (canMove)
	    {
	        rb.velocity = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")) * moveSpeed;
	        
        }
	    else
	    {
	        rb.velocity = Vector2.zero;
	    }

	    if (Input.GetAxisRaw("Horizontal") == 1 || Input.GetAxisRaw("Horizontal") == -1 || Input.GetAxisRaw("Vertical") == 1 || Input.GetAxisRaw("Vertical") == -1)
	    {
	        if (canMove)
	        {
	            myAnim.SetFloat("moveX", rb.velocity.x);
	            myAnim.SetFloat("moveY", rb.velocity.y);
            }

            myAnim.SetFloat("faceX", Input.GetAxisRaw("Horizontal"));
            myAnim.SetFloat("faceY", Input.GetAxisRaw("Vertical"));
	    }

	    transform.position = new Vector3(Mathf.Clamp(transform.position.x, bottomLeftLimit.x, topRightLimit.x), Mathf.Clamp(transform.position.y, bottomLeftLimit.y, topRightLimit.y), transform.position.z);
    }

    public void SetBounds(Vector3 botLeft, Vector3 topRight)
    {
        bottomLeftLimit = botLeft + new Vector3(.5f, .5f, 0f);
        topRightLimit = topRight + new Vector3(-1f, -1f, 0f);
    }
}
