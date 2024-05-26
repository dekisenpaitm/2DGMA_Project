using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ComboHolder : MonoBehaviour
{

    private string[] _niceThings = { "sweet!", "yeah buddy!", "O M G!", "sheesh!", "wow!", "craaazy!", "On Fire!", "woop!", "critical!", "blblbl", "S-s-sick!", "delicious!", "gg ez"  };
    private string[] _badThings = { "Rip.", "ehhh...", "aw cmon.", "meh.", "try harder.", "nope.", "no.", "close.", "skill issue.", "brother ew.", "cringe..", "next.", "uninstall", "kthxbye", "noob." };
    public TextMeshProUGUI text;
    private Animator _anim;

    public string[] GetNiceThings
    {
        get { return _niceThings; }
    }

    public string[] GetBadThings
    {
        get { return _badThings; }
    }
    // Start is called before the first frame update
    void Start()
    {
        text = GetComponentInChildren<TextMeshProUGUI>();
        _anim = GetComponent<Animator>();
    }

    public void RandomizeString(bool isBad)
    {
        if (isBad)
        {
            int word = Random.Range(0, _badThings.Length);
            text.text = _badThings[word];
            _anim.Play("pop");
            return;
        }

        if (!isBad)
        {
            int word = Random.Range(0, _niceThings.Length);
            text.text = _niceThings[word];
            _anim.Play("pop");
            return;
        }
    }
}
