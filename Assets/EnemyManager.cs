using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
	[SerializeField] GameObject player;
	[SerializeField] GameObject chaser, ambusher, whimsical;
	EnemyController cBrain, aBrain, wBrain;
	Rigidbody2D playerRb;
	private void Awake()
	{
		cBrain = chaser.GetComponent<EnemyController>();
		aBrain = ambusher.GetComponent<EnemyController>();
		wBrain = whimsical.GetComponent<EnemyController>();

		playerRb = player.GetComponent<Rigidbody2D>();
	}
	void Update()
	{
		cBrain.targetPos = player.transform.position;
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
