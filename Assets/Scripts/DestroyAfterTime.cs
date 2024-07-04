using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAfterTime : MonoBehaviour
{
    [Header("TimeToDie")]
    [SerializeField]
    private float _time;

    void Start()
    {
        Destroy(gameObject, _time); 
    }

}
