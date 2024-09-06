using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float moveSpeed = 5f;
    [SerializeField] float jumpStr = 2f;
    Rigidbody2D rb;
    BoxCollider2D col;
    bool right, left, jump, grounded;
    float xIn;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<BoxCollider2D>();
    }
    void Update()
    {
        xIn = Input.GetAxis("Horizontal");
        if (xIn == 0)
        {
            col.sharedMaterial.friction = 1000;
        }
        else
        {
            col.sharedMaterial.friction = 0;
        }
        col.sharedMaterial = col.sharedMaterial;    //What the fuck

        if (Input.GetKeyDown(KeyCode.W))
        {
            jump = true;
        }
        if (Input.GetKeyUp(KeyCode.W))
        {
            jump = false;
        }
    }
    void FixedUpdate()
    {
        if (jump && grounded)
        {
            rb.AddForce(Vector2.up * jumpStr);
            jump = false;
            grounded = false;
        }
        rb.AddForce(new Vector2(xIn, 0) * moveSpeed);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.isTrigger)
        {
            grounded = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!collision.isTrigger)
        {
            grounded = false;
        }
    }
}
