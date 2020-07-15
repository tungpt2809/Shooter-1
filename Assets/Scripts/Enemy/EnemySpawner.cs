using System.Collections.Generic;
using GamePlay;
using ObjectPooling;
using UnityEngine;

namespace Enemy
{
    public class EnemySpawner : MonoBehaviour
    {
        private const float SpawnRadius = 7, DeltaDelay = 1.5f;
        private float _delayTime = 0;

        private readonly List<PoolObjectType> _enemies = new List<PoolObjectType>
        {
            PoolObjectType.EnemyZombie,
            PoolObjectType.EnemySkeleton,
            PoolObjectType.EnemyRobot,
        };

        private void Update()
        {
            if (_delayTime > DeltaDelay)
            {
                SpawnAnEnemy();
                _delayTime = 0f;
                return;
            }

            _delayTime += Time.deltaTime;
        }

        private void SpawnAnEnemy()
        {
            if (!GamePlayManager.Instance.spawn) return;
            Vector2 spawnPos = GamePlayManager.Instance.player.transform.position;
            spawnPos += Random.insideUnitCircle.normalized * SpawnRadius;

            var type = _enemies[Random.Range(0, _enemies.Count)];
            var ob = PoolManager.Instance.GetPoolObject(type);

            ob.transform.position = spawnPos;
            ob.gameObject.SetActive(true);
            ob.GetComponent<EnemyHealth>().InitEnemy(type);
        }
    }
}