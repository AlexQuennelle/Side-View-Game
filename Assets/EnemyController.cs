using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] Rigidbody2D player;
    [SerializeField] float moveSpeed;
    [SerializeField] Vector3 currentDirection, targetDir;
    Vector2 hitForward, hitLeft, hitRight;
    Vector2 fBA, fBB;

    void FixedUpdate()
    {
        targetDir = currentDirection;
        Vector3 right = new Vector3(currentDirection.y, currentDirection.x, 0);
        //hitForward = Physics2D.Raycast(transform.position + (currentDirection * 1.125f), currentDirection, 100.0f, -73).point;
        //hitRight = Physics2D.Raycast(transform.position + (right * 1.125f),right, 100.0f, -73).point;
        //hitLeft = Physics2D.Raycast(transform.position + (-right * 1.125f), -right, 100.0f, -73).point;
        fBA = transform.position - right + currentDirection;
        fBB = transform.position + right + (currentDirection * 1.125f);
        Collider2D col = Physics2D.OverlapArea(fBA, fBB, -201);
        if(col != null)
        {
            Debug.Log(col.name);
        }

        transform.position += currentDirection * moveSpeed;
    }
    private void OnDrawGizmos()
    {
        //Gizmos.color = Color.blue;
        //Gizmos.DrawSphere(hitForward, 0.1f);
        //Gizmos.DrawSphere(hitRight, 0.1f);
        //Gizmos.DrawSphere(hitLeft, 0.1f);

        Gizmos.color = Color.black;
        Gizmos.DrawLine(transform.position, player.position);
        Gizmos.color = Color.white;
        Gizmos.DrawLine(transform.position, transform.position + (targetDir * 2));
    }
}
public enum EnemyState { chasing, scattering, fleeing }
