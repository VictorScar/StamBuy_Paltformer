using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PointCounterUI : MonoBehaviour
{
    [SerializeField] private TMP_Text pointCountUI;

    private void UpdatePointCountText(int count)
    {
        pointCountUI.text = count.ToString();
    }
}
