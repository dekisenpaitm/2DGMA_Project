using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdjustToParent : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector2(GetComponentInParent<Transform>().position.x, transform.position.y);
    }
}
