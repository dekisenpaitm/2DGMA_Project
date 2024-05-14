using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlipEffectController : MonoBehaviour
{
    public GameObject[] effects;
    public void FlipEffects()
    {
        foreach (GameObject effect in effects) { effect.transform.localScale *= -1; }
    }

}
