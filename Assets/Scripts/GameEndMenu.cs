using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GameEndMenu : MonoBehaviour
{
    public TextMeshProUGUI points;
    public TextMeshProUGUI enemies;
    public int finalPoints;
    public int finalEnemies;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnEnable()
    {
        points.text = finalPoints.ToString();
        enemies.text = finalEnemies.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
