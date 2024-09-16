using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
	//control for the coroutine while loop
	//determines if the coroutine should continue to repeat the AI steps
	public bool loop = true;

	//2D position the enemy is trying to reach
	public Vector2 targetPos;
	//vector3 storign the current direction in which the enemy is moveing
	//Z component is always 0
	Vector3 currentDir = new Vector3(0.0f,0.0f,0.0f);

	//list of possible directions the enemy can take
	//this list is never used directly, but is instead copied
	readonly List <Vector2> directions = new List <Vector2>
	{
		Vector2.up,
		Vector2.down,
		Vector2.left,
		Vector2.right,
	};

	//method that is called when the gameobject this script is attached to is loaded
	void Awake()
	{
		//starts the coroutine that controls enemy behaviour
		StartCoroutine(Loop());
	}
	IEnumerator Loop()
	{
		//main loop for the enemy AI
		//continues to loop until the loop variable evaluates as false
		while (loop)
		{
			PickDirection();
			//call the Move() coroutine and wait for it to finish executing
			yield return Move();
		}
	}
	//method that performs the logic for chosing a valid direction for the enemy to take
	void PickDirection()
	{
		//copy the directions list to a new list of Vector2
		List<Vector2> options = new List<Vector2>(directions);
		//remove any directions that match the current direction variable, negated and normalized
		//this is to ensure the enemy can't reverse directions under normal circumstances
		options.Remove(-currentDir.normalized);

		//loop through all 4 possible directions and check if each is valid
		for (int i = 0; i < 4; i++)
		{
			//send a raycast in the direction we are checking,
			//and if the point it hits is closer than 1.5 units away, remove it from the options list
			Vector2 p = Physics2D.Raycast(transform.position, directions[i], 100.0f, -201).point;
			if(Vector2.Distance(p, transform.position) < 1.5f)
			{
				options.Remove(directions[i]);
			}
		}

		//if the options list is empty, add back the current direction negated and normalized
		//the options list being empty means the enemy is cornered, and has no choice but to reverse directions
		if (options.Count < 1)
		{
			options.Add(-currentDir.normalized);
		}
		//get a vector pointing from the current position to the target position
		Vector2 targetDir = ((Vector2)transform.position - targetPos).normalized;
		//sort the remaining options by their dot product with the target direction
		//in theory this orders them by which direction will take the enemy closer to the target
		options.Sort((a, b) => ((int)(Vector2.Dot(a, targetDir) * 10) - (int)(Vector2.Dot(b, targetDir) * 10)));
		//set the current direction to match the first item in the options list
		//I'm also using a scaling vector to ensure the enemies remain alligned to the maze
		currentDir = options[0] * new Vector2(2.0f, 3.0f);
	}
	//coroutine that performs the actual movement of the enemy
	IEnumerator Move()
	{
		//store the current position, and the position after the movement logic is complete
		Vector2 p = transform.position + currentDir;
		Vector2 o = transform.position;
		//loop several times, waiting for the next FixedUpdate() call after each itterarion
		//this means whatever happens inside the loop will take place over a certain length of time
		for(int i = 1; i < 34; i++)
		{
			//set the enemy's position to be a value between o and p
			//the exact value is determined by the current loop itteration i
			//we multiply i by 1 divided by the max number of loops minus 1 to normalize i to 0-1
			transform.position = Vector2.Lerp(o, p, i * 0.03f);
			yield return new WaitForFixedUpdate();
		}
		//set the enemy's position to p to account for any small errors in the factor of i in the loop
		transform.position = p;
	}
}
