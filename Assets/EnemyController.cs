using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] Rigidbody2D player;
    [SerializeField] float moveSpeed;
	[SerializeField] bool loop = true;
	bool first = true;
	Vector3 currentDir = new Vector3(2.0f,0.0f,0.0f);

	//[SerializeField] Vector3 currentDirection, targetDir;
	//Vector2 hitForward, hitLeft, hitRight;
	//Vector2 fBA, fBB;
	List <Vector2> directions = new List <Vector2>
	{
		Vector2.up,
		Vector2.down,
		Vector2.left,
		Vector2.right,
	};
	void Awake()
	{
	}
	void Update()
	{
		if (first)
		{
			first = false;
			StartCoroutine(Loop());
		}
	}
	void FixedUpdate()
    {
		#region Deprecated
		//targetDir = currentDirection;
		//Vector3 right = new Vector3(currentDirection.y, currentDirection.x, 0);
		////hitForward = Physics2D.Raycast(transform.position + (currentDirection * 1.125f), currentDirection, 100.0f, -73).point;
		////hitRight = Physics2D.Raycast(transform.position + (right * 1.125f),right, 100.0f, -73).point;
		////hitLeft = Physics2D.Raycast(transform.position + (-right * 1.125f), -right, 100.0f, -73).point;
		//fBA = transform.position - right + currentDirection;
		//fBB = transform.position + right + (currentDirection * 1.125f);
		//Collider2D col = Physics2D.OverlapArea(fBA, fBB, -201);
		//if(col != null)
		//{
		//    Debug.Log(col.name);
		//}

		//transform.position += currentDirection * moveSpeed;
		#endregion
	}
	IEnumerator Loop()
	{
		while (loop)
		{
			yield return PickDirection();
			yield return Move();
		}
	}
	IEnumerator PickDirection()
	{
		Vector2 targetDir = (Vector2)transform.position - player.position;
		directions.Sort((a, b) => ((int)(Vector2.Dot(a, targetDir) * 10) - (int)(Vector2.Dot(b, targetDir) * 10)));
		currentDir = directions[0] * new Vector2(2.0f, 3.0f);
		yield break;
	}
	IEnumerator Move()
	{
		Vector2 p = transform.position + currentDir;
		Vector2 o = transform.position;
		Debug.Log(o + ", " + p);
		for(int i = 1; i < 101; i++)
		{
			transform.position = Vector2.Lerp(o, p, i * 0.01f);
			yield return new WaitForEndOfFrame();
		}
	}

	private void OnDrawGizmos()
    {
        //Gizmos.color = Color.black;
        //Gizmos.DrawLine(transform.position, player.position);
        //Gizmos.color = Color.white;
        //Gizmos.DrawLine(transform.position, transform.position + (targetDir * 2));
    }
}
