using UnityEngine;

namespace Enemy
{
    public class EnemyFollow : MonoBehaviour
    {
        [SerializeField] private float speed = 0f;
        [SerializeField] private float range = 0f;

        private Transform _playerPos;

        private void Awake() =>
            _playerPos = GameObject.FindGameObjectWithTag("Player").transform;

        private void Update()
        {
            if (Vector2.Distance(transform.position, _playerPos.position) > range)
            {
                transform.position =
                    Vector2.MoveTowards(transform.position, _playerPos.position, speed * Time.deltaTime);
            }
        }
    }
}