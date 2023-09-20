using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class SpawnPoint : MonoBehaviour
{
    public Vector2 Position => transform.position;

    public bool IsBusy = false;
}
