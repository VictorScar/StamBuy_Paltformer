using Photon.Pun;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Gun gun;

    [SerializeField] private float moveSpeed = 100f;
    [SerializeField] private float jumpForce = 2f;

    [SerializeField] private float maxHealth = 100f;
    [SerializeField] private float health = 100f;

    public void Move(Vector2 direction)
    {
        PlayerRotate(direction);
        //rb.velocity = direction * moveSpeed * Time.fixedDeltaTime;
        transform.position += new Vector3(direction.x, direction.y, 0) * moveSpeed * Time.deltaTime;
    }

    public void Jump()
    {
        //StartCoroutine(JumpMoving());
        rb.AddForce(Vector2.up * jumpForce);
    }

    public void Fire()
    {
        gun.Shoot();
    }

    public void GetDamage(float damage)
    {
        if (damage <= health)
        {
            health -= damage;
        }
        else
        {
            health = 0f;
            Death();
        }
    }

    private void Death()
    {
        Debug.Log(PhotonNetwork.LocalPlayer.NickName + " is Dead!");
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

        while (currentHeight < jumpForce)
        {
            Vector3.MoveTowards(transform.position, transform.position + new Vector3(0, jumpForce, 0), 0.5f);
            currentHeight += 0.5f;
            yield return null;
        }

    }


}
