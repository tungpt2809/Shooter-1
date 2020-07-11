using UnityEngine;

namespace Enemy
{
    [RequireComponent(typeof(Rigidbody2D), typeof(Collider2D))]
    public class EnemyHealth : MonoBehaviour
    {
        [SerializeField] private float health;

        public AudioClip deathClip;
        public GameObject healthBar;

        private void Update()
        {
            if (health < 1)
            {
                Destroy(gameObject);
                // SoundManager.instance.playSounceFX(deathClip);
            }
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (!other.CompareTag("PlayerBullet")) return;
            var bullet = other.GetComponent<Bullet.Bullet>();
            health -= bullet.damage;
            bullet.CoolBullet();

            healthBar.transform.localScale = new Vector2(health / 100, healthBar.transform.localScale.y);
        }
    }
}