using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
	//variables that stores references to the player and their rigidbody
	[SerializeField] GameObject player;
	Rigidbody2D playerRb;
	//variables that store references to the enemies and their controllers
	[SerializeField] GameObject chaser, ambusher, whimsical;
	EnemyController cBrain, aBrain, wBrain;

	//method that is called when the gameobject containing this script is loaded
	private void Awake()
	{
		//fetch and store the controller components for all enemies
		cBrain = chaser.GetComponent<EnemyController>();
		aBrain = ambusher.GetComponent<EnemyController>();
		wBrain = whimsical.GetComponent<EnemyController>();

		//fetch and store the player's rigidbody component
		playerRb = player.GetComponent<Rigidbody2D>();
	}
	//method that is called once every time a frame is drawn to the screen
	void Update()
	{
		//set the chaser's target position to the player's position
		cBrain.targetPos = player.transform.position;
		//set the ambusher's target position to a point 8 units away from the player in the direction they're travelling
		aBrain.targetPos = player.transform.position + (Vector3)(playerRb.velocity.normalized * 8);
		

		/*************************************/
		/*Logic for Whimsical target position*/
		/*************************************/

		//get the direction the player is moving by retrieving their normalized velocity
		//then multiply it by 4 and add it to the player's position to get a point 4 units in front of them
		Vector3 plrDir = ((Vector3)playerRb.velocity.normalized * 4) + player.transform.position;
		//get get a vector that points from the Chaser's position to the playerDir point
		//then multiply that by 2 to get a point twice as far
		//essentially this step reflects the chaser's position about the playerDir point by 180 degrees
		Vector3 targetDir = (plrDir - chaser.transform.position) * 2;
		//add the targetDir to the whimsical enemy's position to make it relative, then set that point as its target position
		wBrain.targetPos = chaser.transform.position + targetDir;
		//wBrain.targetPos = chaser.transform.position + (((((Vector3)playerRb.velocity.normalized * 4) + player.transform.position) - chaser.transform.position) * 2);
	}
	//method that is called when drawing gizmos in the editor
	private void OnDrawGizmos()
	{
		//check to make sure the script has fetched the EnemyController components from each enemy
		if (cBrain != null)
		{
			//set the gizmo colour to red, and draw point at the target position with a line connecting it to the enemy
			Gizmos.color = Color.red;
			Gizmos.DrawSphere(cBrain.targetPos, 0.2f);
			Gizmos.DrawLine(chaser.transform.position, cBrain.targetPos);
		}
		if (aBrain != null)
		{
			//set the gizmo colour to a pale pink, and draw point at the target position with a line connecting it to the enemy
			Gizmos.color = new Color(255, 160, 250);
			Gizmos.DrawSphere(aBrain.targetPos, 0.2f);
			Gizmos.DrawLine(ambusher.transform.position, aBrain.targetPos);
		}
		if (wBrain != null)
		{
			//set the gizmo colour to blue, and draw point at the target position with a line connecting it to the enemy
			Gizmos.color = Color.blue;
			Gizmos.DrawSphere(wBrain.targetPos, 0.2f);
			Gizmos.DrawLine(whimsical.transform.position, wBrain.targetPos);

			//set the gizmo colour to gray and draw a line from the chaser's position to the whimsical target position
			//this is to illustrate the part the chaser plays in the whimsical target position
			Gizmos.color = Color.gray;
			Gizmos.DrawLine(chaser.transform.position, wBrain.targetPos);
			//set the gizmo colour to black and draw a point 4 units in front of the player in the direction they're moving
			//this shows the pivot point used to calculate the whimsical target position
			Gizmos.color = Color.black;
			Gizmos.DrawSphere(player.transform.position + (Vector3)(playerRb.velocity.normalized * 4), 0.2f);
		}
	}
}
