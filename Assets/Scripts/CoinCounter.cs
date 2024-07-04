using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CoinCounter : MonoBehaviour
{
    #region Attributes
    private int _coinCounter = 0;
    #endregion

    public int Coins
    {
        get { return _coinCounter; }  
        set { _coinCounter = value; }
    }

    private TextMeshProUGUI text;

    private void Start()
    {
        text = GetComponentInChildren<TextMeshProUGUI>();
    }

    private void Update()
    {
        text.text = _coinCounter.ToString();
    }
}
