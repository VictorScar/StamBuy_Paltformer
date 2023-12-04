using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TheAbyss : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        var fallenPawn = collision.gameObject.GetComponent<PlayerController>();

        if (fallenPawn != null)
        {
            fallenPawn.GetDamage(200f);
        }
    }
}
