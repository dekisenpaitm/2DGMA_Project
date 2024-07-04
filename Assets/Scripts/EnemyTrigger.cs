using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTrigger : MonoBehaviour
{
    [Header("Enemies_in_sight")]
    public List<GameObject> _enemy;
    [Header("Closest_Enemy")]
    public GameObject _target;

    void Update()
    {
        CheckIfTarget();
    }

    public void DeleteFromList(GameObject enemy)
    {
        if (_enemy.Contains(enemy))
        {
            _enemy.Remove(enemy);
            if (enemy == _target)
            {
                _target = null;
            }
        }
    }

    private void CheckIfTarget()
    {
        if (_enemy != null)
        {
            for (int i = 0; i < _enemy.Count; i++)
            {
                for (int j = 0; j < _enemy.Count; j++)
                {
                    if (_enemy != null)
                    {
                        if (_enemy[i] != null && _enemy[j]!= null)
                        {
                            float distance_i = Vector2.Distance(new Vector2(transform.position.x, 0), new Vector2(_enemy[i].transform.position.x, 0));
                            float distance_j = Vector2.Distance(new Vector2(transform.position.x, 0), new Vector2(_enemy[j].transform.position.x, 0));
                            if (distance_i < distance_j)
                            {
                                _target = _enemy[i];
                            }
                            else
                            {
                                _target = _enemy[j];
                            }
                        } else
                        {
                            return;
                        }
                       
                    }
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy") && !collision.gameObject.GetComponent<Enemy>()._cantBeTarget)
        {
            if (!_enemy.Contains(collision.gameObject))
            {
                _enemy.Add(collision.gameObject);
            }
        }
    }
}
