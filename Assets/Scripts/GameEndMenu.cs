using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GameEndMenu : MonoBehaviour
{
    [Header("UI_Elements")]
    public TextMeshProUGUI points;
    public TextMeshProUGUI enemies;
    [Header("Points")]
    public int finalPoints;
    public int finalEnemies;

    private void OnEnable()
    {
        points.text = finalPoints.ToString();
        enemies.text = finalEnemies.ToString();
    }
}
