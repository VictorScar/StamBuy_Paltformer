using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    [SerializeField] private PhotonView photonView; 
    [SerializeField] private PlayerController controller;
    void Update()
    {
        if (photonView.IsMine)
        {
            var direction = GetMoveDirection();
            controller.Move(direction);
            ActionsInput();
        }
    }

    private Vector2 GetMoveDirection()
    {
        var direction = Vector2.zero;
        var direction1 = Vector2.zero;
        var direction2 = Vector2.zero;

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            direction = Vector2.left;
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            direction = Vector2.right;
        }

       // direction = direction1 + direction2;

        return direction;
    }

    private void ActionsInput()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            controller.Jump();
        }
    }
}
