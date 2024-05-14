using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapMovementController : MonoBehaviour
{
    public GameObject paralax1;
    public float p1_speed;

    public GameObject paralax2;
    public float p2_speed;

    public GameObject paralax3;
    public float p3_speed;

    public GameObject floorObjects;
    public float fO_speed;

    public GameObject foreGround;
    public float fG_speed;

    private MapController _inputActionReference;
    private float _mapMovementDirection;

    // Start is called before the first frame update
    void Start()
    {

        _inputActionReference = new MapController();
        //enabling the Input actions
        _inputActionReference.Enable();
        //reading the values of the player movement direction for the players movement.
        _inputActionReference.Movemap.Move.performed += moving =>
        {
            _mapMovementDirection = moving.ReadValue<float>();
        };
    }

    // Update is called once per frame
    void Update()
    {

        paralax1.GetComponent<Rigidbody2D>().velocity =
            new Vector2(_mapMovementDirection * p1_speed, paralax1.GetComponent<Rigidbody2D>().velocity.y);

        paralax2.GetComponent<Rigidbody2D>().velocity =
            new Vector2(_mapMovementDirection * p2_speed, paralax2.GetComponent<Rigidbody2D>().velocity.y);

        paralax3.GetComponent<Rigidbody2D>().velocity =
            new Vector2(_mapMovementDirection * p3_speed, paralax3.GetComponent<Rigidbody2D>().velocity.y);

        floorObjects.GetComponent<Rigidbody2D>().velocity =
            new Vector2(_mapMovementDirection * fO_speed, floorObjects.GetComponent<Rigidbody2D>().velocity.y);

        foreGround.GetComponent<Rigidbody2D>().velocity =
            new Vector2(_mapMovementDirection * fG_speed, foreGround.GetComponent<Rigidbody2D>().velocity.y);
    }
}
