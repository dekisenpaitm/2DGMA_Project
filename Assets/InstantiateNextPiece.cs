using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantiateNextPiece : MonoBehaviour
{
    public GameObject[] objToInstantiate;
    public GameObject pointToInstantiate;
    public string layer;
    public Transform layerToSpawn;
    public bool hasInstantiated;

    // Update is called once per frame
    void Update()
    {
        layerToSpawn = GameObject.Find(layer).transform;
    }

    private int RandomizeMap()
    {
        return Random.Range(0, objToInstantiate.Length);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("I have collided");
        if (collision.CompareTag("MapTrigger") && !hasInstantiated)
        {
            if (objToInstantiate.Length > 1)
            {
                GameObject mapPiece = Instantiate(objToInstantiate[RandomizeMap()], layerToSpawn.transform);
                mapPiece.transform.position = new Vector2(pointToInstantiate.transform.position.x, transform.position.y);
            }
            else
            {
                GameObject mapPiece = Instantiate(objToInstantiate[0], layerToSpawn.transform);
                mapPiece.transform.position = new Vector2(pointToInstantiate.transform.position.x, transform.position.y);
            }
            hasInstantiated = true;
        }
        
        if(collision.CompareTag("MapDelete") && hasInstantiated)
        {
            Destroy(gameObject);
        }
    }
}
