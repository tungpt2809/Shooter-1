using System.Collections.Generic;
using UnityEngine;

public class EnemyFollow : MonoBehaviour
{
    public float speed;

    private Transform _playerPos;
    private Rigidbody2D _rb;
    private List<Rigidbody2D> EnemyRbs;

    private float _repelRange = 0.5f;

    void Awake()
    {
        _playerPos = GameObject.FindGameObjectWithTag("Player").transform;
        _rb = GetComponent<Rigidbody2D>();

        if (EnemyRbs == null)
        {
            EnemyRbs = new List<Rigidbody2D>();
        }

        EnemyRbs.Add(_rb);
    }

    private void OnDestroy()
    {
        EnemyRbs.Remove(_rb);
    }

    void Update()
    {
        if (Vector2.Distance(transform.position, _playerPos.position) > 0.2f)
        {
            transform.position = Vector2.MoveTowards(transform.position, _playerPos.position, speed * Time.deltaTime);
        }
    }

    private void FixedUpdate()
    {
        Vector2 repelForce = Vector2.zero;
        foreach (Rigidbody2D enemy in EnemyRbs)
        {
            if (enemy == _rb) continue;

            if(Vector2.Distance(enemy.position, _rb.position) <= _repelRange)
            {
                Vector2 repelDir = (_rb.position - enemy.position).normalized;
                repelForce += repelDir;
            }


        }
    }
}
