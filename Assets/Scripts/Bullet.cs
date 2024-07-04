using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [Header("Attack_Power")]
    [SerializeField]
    private int attackPower;

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && collision.name == "Player")
        {
            collision.GetComponent<Player>().ApplyDamage(attackPower);
            Destroy(gameObject);
        }
    }

}
