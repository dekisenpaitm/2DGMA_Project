using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    [Header("Start_Position")]
    private float startpos;
    [Header("Cam_To_Follow")]
    public GameObject cam;
    [Header("Parallax_Speed")]
    public float parallaxEffect;


    void Start()
    {
       startpos = transform.position.x;
    }

    private void FixedUpdate()
    {
        float temp = (cam.transform.position.x * (1 - parallaxEffect));
    }

    void Update()
    {
        float dist = (cam.transform.position.x) * parallaxEffect;
        transform.position = new Vector3(startpos + dist, transform.position.y, transform.position.z);
    }
}
