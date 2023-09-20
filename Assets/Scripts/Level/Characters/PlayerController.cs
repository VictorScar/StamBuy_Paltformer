using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerController : MonoBehaviour
{
    [SerializeField] Rigidbody2D rb;

    [SerializeField] private float moveSpeed = 100f;
    [SerializeField] private float jumpHeight = 2f;

    public void Move(Vector2 direction)
    {
        PlayerRotate(direction);
        //rb.velocity = direction * moveSpeed * Time.fixedDeltaTime;
        transform.position += new Vector3(direction.x, direction.y, 0) * moveSpeed * Time.deltaTime;
    }

    public void Jump()
    {
        //StartCoroutine(JumpMoving());
        rb.AddForce(Vector2.up * jumpHeight);
    }

    private void PlayerRotate(Vector2 direction)
    {
        if (direction == Vector2.right)
        {
            transform.rotation = Quaternion.identity;
        }
        else if (direction == Vector2.left)
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }
    }

    IEnumerator JumpMoving()
    {
        float currentHeight = 0f;

        while (currentHeight < jumpHeight)
        {
            Vector3.MoveTowards(transform.position, transform.position + new Vector3(0, jumpHeight, 0), 0.5f);
            currentHeight += 0.5f;
            yield return null;
        }
       
    }
}
