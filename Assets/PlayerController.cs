using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float moveSpeed = 5f;
    [SerializeField] float jumpStr = 2f;
    Rigidbody2D rb;
    bool right, left, jump, grounded;
    float xIn;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        xIn = Input.GetAxis("Horizontal");
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
