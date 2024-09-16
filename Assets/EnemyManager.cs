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
		wBrain.targetPos = chaser.transform.position + (((((Vector3)playerRb.velocity.normalized * 4) + player.transform.position) - chaser.transform.position) * 2);
	}
	private void OnDrawGizmos()
	{
		if (cBrain != null)
		{
			Gizmos.color = Color.red;
			Gizmos.DrawSphere(cBrain.targetPos, 0.2f);
			Gizmos.DrawLine(chaser.transform.position, cBrain.targetPos);
		}
		if (aBrain != null)
		{
			Gizmos.color = new Color(255, 160, 250);
			Gizmos.DrawSphere(aBrain.targetPos, 0.2f);
			Gizmos.DrawLine(ambusher.transform.position, aBrain.targetPos);
		}
		if (wBrain != null)
		{
			Gizmos.color = Color.blue;
			Gizmos.DrawSphere(wBrain.targetPos, 0.2f);
			Gizmos.DrawLine(whimsical.transform.position, wBrain.targetPos);
		}
	}
}
