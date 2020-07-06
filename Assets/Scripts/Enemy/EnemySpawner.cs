using System.Collections.Generic;
using ObjectPooling;
using UnityEngine;

namespace Enemy
{
    public class EnemySpawner : MonoBehaviour
    {
        private const float SpawnRadius = 7, Time = 1.5f;
        private float _delayTime = 0;
        private readonly List<PoolObjectType> _enemies = null;
        private PoolManager _poolManager;

        private void Awake()
        {
            _poolManager = PoolManager.Instance;

            _enemies.Add(PoolObjectType.EnemyZombie);
            _enemies.Add(PoolObjectType.EnemySkeleton);
        }

        private void Update()
        {
            if (_delayTime > Time)
            {
                SpawnAnEnemy();
                _delayTime = 0f;
            }

            _delayTime += UnityEngine.Time.deltaTime;
        }

        private void SpawnAnEnemy()
        {
            Vector2 spawnPos = GameObject.FindGameObjectWithTag("Player").transform.position;
            spawnPos += Random.insideUnitCircle.normalized * SpawnRadius;

            var type = _enemies[Random.Range(0, _enemies.Count)];
            var ob = _poolManager.GetPoolObject(type);

            ob.transform.position = spawnPos;
            ob.gameObject.SetActive(true);
        }
    }
}