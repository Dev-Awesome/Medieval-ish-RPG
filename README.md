# Medieval-ish RPG
A 2D pixel art RPG game with features like quest system, battle system, dialog system, boss fights and more...

## A summary about the project
This project is going to be a 2D RPG. Main inspiration is the almighty Undertale but due to my experience from playing some other RPGM games, I will add more features like battle system and quest systems. Project is in the development so of course there will be some bugs or other problems. Please point them out and I will appreciate your concern gladly. 

## Requirements for the project

1) I will expect you to have a basic understanding of C#. I will not go through and explain every single detail about the code and I will only explain what the code does in a manner of function in the game. It helps if you also know about the intermediate concepts in C# but not necessarily. 

2) I will also expect you to have a basic understanding of Unity. You need to know what prefab, canvas and etc.. mean in the interface. You need to know how the X-Y-Z coordinations work and I expect you to understand how to rotate and manipulate objects in the scene. 

## Unity Version That I Used

Unity 2018.2.0f, please do not attempt to open this project with Unity versions below Unity 2017.3 because tilemap feature is not included in those versions so you will have trouble openning it. 

## Project Presentation

So, if you are familiar with the almighty 2D pixel art RPG's, you can pretty much expect what this project is about. However, in the future, I will try to add more features that either other games didn't cover properly or did not cover at all. 

For now, I created the basic gameplay so you should expect more to see in the future. The assets do not belong to me, however you can use it in your commercial projects and they are free. In the sprites folder, you will see the license and there are a couple of terms before you use them. You should stick with the license terms. 

First, I started to create the gameworld with Tilemaps. I created 3 tilemaps which are ground, objects and road. You will see in the inspector and I also set their order so objects and ground and roads are not going to mix together. This is the final game world: 

<a href="https://ibb.co/Cb3PTZ4"><img src="https://i.ibb.co/x1tFR0w/Game-World.jpg" alt="Game-World" border="0"></a>

After that, I created walking and idle animations in the Animation panel then I created a BlendTree to manage those animations which is pretty useful so I highly recommend you stick with that feature in your future projects: 

<a href="https://imgbb.com/"><img src="https://i.ibb.co/QmJyrXq/Blend-Tree.jpg" alt="Blend-Tree" border="0"></a>
<a href="https://imgbb.com/"><img src="https://i.ibb.co/SdtdNn4/Blend-Tree2.jpg" alt="Blend-Tree2" border="0"></a>

Blend Tree is basically an animation controller. Instead of activating the animations in code, you can use BlendTree to make the transitions for you. So, it will provide more code efficiency and require less keep-up. It works in the same principle as animation controller. I added two parameters, moveX and moveY. Those parameter values will change according to player movement. I set the boundaries to either 0.1 or -0.1 with the respect of game world. When you move up, moveY parameter will move towards 0.1 value but when you go down from the origin it will move to -0.1 which will make the player move downside. Same goes for moveX parameter. You are free to change which value is going to be, you can make it to 1 but I figured 0.1 is the best solution in my opinion. 

You can look into the official Unity documentation to find more about BlendTree feature: 

https://docs.unity3d.com/Manual/class-BlendTree.html

Here is the inspector and I will explain what those objects stood for: 

<a href="https://imgbb.com/"><img src="https://i.ibb.co/LgjyVMw/Inspector.jpg" alt="Inspector" border="0"></a>

## Player

So this is the handsome that will serve as the protagonist of our game: 

<a href="https://imgbb.com/"><img src="https://i.ibb.co/WzFnFfq/player.jpg" alt="player" border="0"></a>

Those are what I attached to him:

<a href="https://imgbb.com/"><img src="https://i.ibb.co/r7THQ6T/Player-Inspector.jpg" alt="Player-Inspector" border="0"></a>

He also has an animator. 

I attached a PlayerController script to handle the movement. 

```C#
    public float moveSpeed;
    public Rigidbody2D rb;
    public Animator myAnim;
    public bool canMove = true;
    public static PlayerController instance;
    private Vector3 bottomLeftLimit;  
    private Vector3 topRightLimit;
    public string areaTransitionName; //will cover that later 
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
```

Script will first to check if the player exist. If he exists, then he won't duplicate it and set that player as the original. We have a canMove boolean value that we will use A LOT. In the update method, we receive vertical and horizontal inputs from the player and multiply it by a value that we previously set on the inspector. You can also set it from the code but I won't recommend it. If canMove parameter is false, then we set the vector to zero so movement won't be possible when the value is false. 

In the if statement, we are looking for specific inputs from the player. In four possibilities(moving up,down,left,right), we handle the movement and we are looking for which parameters should work so BlendTree can handle the movement. According to canMove value, BlendTree will receive moveX and moveY parameters and movement will be possible. Regardless of the canMove statement, animator will make the player face in the direction according to our input. 

I also added a transform.position statement but I set some limits on it. The reason for that is, player is not supposed to leave the gameplay field. If we don't do that, then player will reach beyond infinity once he leaves the game world. So I set two game limits and set them as controllers so player will stay in the game world. 

In the SetBounds method, I received two parameters botLeft and topRight. Those values will also be used in CameraController so camera wont leave the game field as well as the player. I set some limits on my liking but you are free to set the limits yourself. 

EXPLANATION WILL CONTINUE SOON...






