using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] Rigidbody2D player;
    [SerializeField] int moveSpeed;
	[SerializeField] bool loop = true;
	Vector3 currentDir = new Vector3(0.0f,0.0f,0.0f);

	readonly List <Vector2> directions = new List <Vector2>
	{
		Vector2.up,
		Vector2.down,
		Vector2.left,
		Vector2.right,
	};
	void Awake()
	{
		StartCoroutine(Loop());
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
		List<Vector2> options = new List<Vector2>(directions);
		options.Remove(-currentDir.normalized);

		for (int i = 0; i < 4; i++)
		{
			Vector2 p = Physics2D.Raycast(transform.position, directions[i], 100.0f, -193).point;
			if(Vector2.Distance(p, transform.position) < 1.5f)
			{
				options.Remove(directions[i]);
			}
		}
		Debug.Log(options.Count);

		if (options.Count < 1)
		{
			options.Add(-currentDir.normalized);
		}
		Vector2 targetDir = ((Vector2)transform.position - player.position).normalized;
		options.Sort((a, b) => ((int)(Vector2.Dot(a, targetDir) * 10) - (int)(Vector2.Dot(b, targetDir) * 10)));
		currentDir = options[0] * new Vector2(2.0f, 3.0f);
		yield break;
	}
	IEnumerator Move()
	{
		Vector2 p = transform.position + currentDir;
		Vector2 o = transform.position;
		for(int i = 1; i < 34; i++)
		{
			transform.position = Vector2.Lerp(o, p, i * 0.03f);
			yield return new WaitForEndOfFrame();
		}
		transform.position = p;
	}

	private void OnDrawGizmos()
    {
        //Gizmos.color = Color.black;
        //Gizmos.DrawLine(transform.position, player.position);
        //Gizmos.color = Color.white;
        //Gizmos.DrawLine(transform.position, transform.position + (targetDir * 2));
    }
}
