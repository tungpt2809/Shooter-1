using System.Collections.Generic;
using ObjectPooling;
using Player;
using UnityEngine;

namespace Enemy
{
    public class EnemySpawner : MonoBehaviour
    {
        private const float SpawnRadius = 7, Time = 1.5f;
        private float _delayTime = 0;
        private Transform _playerPos;

        private readonly List<PoolObjectType> _enemies = new List<PoolObjectType>
        {
            PoolObjectType.EnemyZombie,
            PoolObjectType.EnemySkeleton,
            PoolObjectType.EnemyRobot,
        };

        private void Awake()
        {
            _playerPos = GameObject.FindGameObjectWithTag("Player").transform;
        }

        private void Update()
        {
            if (_delayTime > Time)
            {
                SpawnAnEnemy();
                _delayTime = 0f;
                return;
            }

            _delayTime += UnityEngine.Time.deltaTime;
        }

        private void SpawnAnEnemy()
        {
            Vector2 spawnPos = _playerPos.position;
            spawnPos += Random.insideUnitCircle.normalized * SpawnRadius;

            var type = _enemies[Random.Range(0, _enemies.Count)];
            var ob = PoolManager.Instance.GetPoolObject(type);

            ob.transform.position = spawnPos;
            ob.gameObject.SetActive(true);
            ob.GetComponent<EnemyHealth>().InitEnemy(type);
        }
    }
}