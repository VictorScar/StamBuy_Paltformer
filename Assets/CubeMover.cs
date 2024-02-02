using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeMover : MonoBehaviour
{
    [SerializeField] Rigidbody2D rb;
    [SerializeField] float speed = 2f;
    [SerializeField] float gravity = 2f;
    private Vector2 direction;
    private bool isGrounded;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
        Vector2 xAxis = Vector2.zero;
        Vector2 yAxis = Vector2.zero;

        if (Input.GetKey(KeyCode.A))
        {
            xAxis = Vector2.left;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            xAxis = Vector2.right;
        }
        if (Input.GetKey(KeyCode.W))
        {
            yAxis = Vector2.up;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            yAxis = Vector2.down;
        }

        direction = xAxis + yAxis;
        //direction = Vector2.zero;



        Move(direction);
    }

    private void Move(Vector2 direction)
    {
        Vector2 gravityVector = Vector2.zero;

        if (!isGrounded)
        {
            gravityVector = Vector2.down * gravity;
        }

        rb.position += (direction * speed + gravityVector) * Time.deltaTime;
        //rb.position = transform.position;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision != null)
        {
            isGrounded = true;
        }
        else { isGrounded = false; }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        isGrounded = false;
    }
}
