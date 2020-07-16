using GamePlay;
using ObjectPooling;
using Observer;
using UnityEngine;

namespace Enemy
{
    [RequireComponent(typeof(Rigidbody2D), typeof(Collider2D))]
    public class EnemyHealth : MonoBehaviour
    {
        [SerializeField] private float health = 0;
        [SerializeField] private int scoreReward = 0;
        [SerializeField] private GameObject healthBar = null;

        private PoolObjectType _type = PoolObjectType.None;
        private float _currentHeath;

        private float CurrentHeath
        {
            get => _currentHeath;
            set
            {
                _currentHeath = value;
                UpdateHeathBar();
            }
        }

        public void InitEnemy(PoolObjectType type)
        {
            _type = type;
            CurrentHeath = health;
        }

        private void Update()
        {
            CoolEnemy();
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (!other.CompareTag("PlayerBullet")) return;
            var bullet = other.GetComponent<Bullet.Bullet>();
            CurrentHeath -= bullet.Damage;
            bullet.CoolBullet();
        }

        private void UpdateHeathBar()
        {
            healthBar.transform.localScale = new Vector2(CurrentHeath / health, healthBar.transform.localScale.y);
        }

        private void CoolEnemy()
        {
            if (CurrentHeath > 0) return;
            PoolManager.Instance.CoolObject(gameObject, _type);
            EventDispatcher.Instance.OnEnemyDeath.Invoke(scoreReward);
        }
    }
}