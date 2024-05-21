using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAfterTime : MonoBehaviour
{
    [Header("TimeToDie")]
    [SerializeField]
    private float _time;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, _time); 
    }

}
