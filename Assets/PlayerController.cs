using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine.Events;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
	//variables for handling the movement speed and jump strength
	[SerializeField] float moveSpeed = 5f;
	[SerializeField] float jumpStr = 2f;

	[SerializeField] UnityEvent OnDeath = new UnityEvent();
	//variable for storing the player's rigidbody component to apply forces to
	Rigidbody2D rb;
	//variable for storing the player's collider
	BoxCollider2D col;
	//variables for keeping track of the player's jump state
	bool jump, grounded;
	//variable that stores the player's left and right input
	float xIn;

	//method that is called before the first frame of the game starts
	void Start()
	{
		//fetch and store the player's rigidbody and collider
		rb = GetComponent<Rigidbody2D>();
		col = GetComponent<BoxCollider2D>();
	}
	//method that is called once for every frame that gets drawn to the screen
	void Update()
	{
		//update the input variable by storing the value returned from fetching the horizontal input axis
		xIn = Input.GetAxis("Horizontal");
		//if the input variable is 0, set the collider's friction to 1000
		//this means the player stops moving very quickly when they let go of the input
		//otherwise, set the friction to 0
		if (xIn == 0)
		{
			col.sharedMaterial.friction = 1000;
		}
		else
		{
			col.sharedMaterial.friction = 0;
		}
		//set the collider's physics material to the collider's physics material
		//I have no idea why this is necessary, but without this line the friction is never updated at runtime
		col.sharedMaterial = col.sharedMaterial;    //what the fuck

		//check if the player has pressed the w key since the last frame
		//if they have, set jump to true to queue a jump
		if (Input.GetKeyDown(KeyCode.W))
		{
			jump = true;
		}
		//check if the player has released the w key since the last frame
		//if the have, set jump to false to cancel any unexecuted jumps
		if (Input.GetKeyUp(KeyCode.W))
		{
			jump = false;
		}
	}
	//method that is called once every physics frame
	//this method is called much more often than the Update() method, and at a more consistent rate
	//this consistency makes it ideal for physics logic
	void FixedUpdate()
	{
		//if the grounded and jump variables are both true, set them to false and add upwards force to the player's rigidybody equal to the jump strength variable
		if (jump && grounded)
		{
			rb.AddForce(Vector2.up * jumpStr);
			jump = false;
			grounded = false;
		}
		//add force on the x axis of the player's rigidbody equaly to the input variable times the move speed
		rb.AddForce(new Vector2(xIn * moveSpeed, 0));
	}
	//method that is called either when the collider enters a trigger, or when the trigger enters a collider
	//both game objects involved must have rigidbody components to trigger this method
	//triggers entering triggers don't cause this method to be called
	//provides a reference to the collider of the other object involved
	private void OnTriggerEnter2D(Collider2D collision)
	{
		//check if the other collider involved is a trigger and set the grounded variable to true if it isn't
		//this is to filter the collision events to only care about things entering the collider attached to this game object
		if (!collision.isTrigger)
		{
			grounded = true;
		}
	}
	//method that is called either when a collider leaves a trigger, or a trigger leaves a collider
	//both objects involved must have rigidbody components to trigger this method
	//provides a reference to the other collider involved
	private void OnTriggerExit2D(Collider2D collision)
	{
		//check if the other collider involved is a trigger and set the grounded variable to false if it isn't
		//this is to filter the collision events to only care about things entering the collider attached to this game object
		if (!collision.isTrigger)
		{
			grounded = false;
		}
	}
	public void TakeDamage()
	{
		gameObject.SetActive(false);
		OnDeath.Invoke();
	}
}
