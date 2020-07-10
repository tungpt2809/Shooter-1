// using System.Collections.Generic;
using UnityEngine;

namespace Enemy
{
    public class EnemyFollow : MonoBehaviour
    {
        [SerializeField] private float speed = 0f;

        private Transform _playerPos;
        // private Rigidbody2D _rb;
        // private readonly List<Rigidbody2D> _enemyRbs = new List<Rigidbody2D>();

        // private const float RepelRange = 0.5f;

        private void Awake()
        {
            _playerPos = GameObject.FindGameObjectWithTag("Player").transform;
            // _rb = GetComponent<Rigidbody2D>();

            // _enemyRbs.Add(_rb);
        }

        // private void OnDestroy()
        // {
        //     _enemyRbs.Remove(_rb);
        // }

        private void Update()
        {
            if (Vector2.Distance(transform.position, _playerPos.position) > 0.2f)
            {
                transform.position =
                    Vector2.MoveTowards(transform.position, _playerPos.position, speed * Time.deltaTime);
            }
        }

        // private void FixedUpdate()
        // {
        //     var repelForce = Vector2.zero;
        //     foreach (var enemy in _enemyRbs)
        //     {
        //         if (enemy == _rb) continue;
        //
        //         if (Vector2.Distance(enemy.position, _rb.position) <= RepelRange)
        //         {
        //             var repelDir = (_rb.position - enemy.position).normalized;
        //             repelForce += repelDir;
        //         }
        //     }
        // }
    }
}