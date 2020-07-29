using UnityEngine;

namespace Bullet
{
    [RequireComponent(typeof(Collider2D))]
    public class SubBullet : MonoBehaviour
    {
        private Bullet _bullet;
        public int Damage { get; private set; } = 0;
        public bool IsHit { get; private set; } = false;

        public void InitSubBullet(Bullet bullet, int damage)
        {
            _bullet = bullet;
            Damage = damage;
        }

        public void Hit()
        {
            IsHit = true;
            if (!_bullet.GetComponent<Animator>())
                gameObject.SetActive(false);
            _bullet.Hit(false);
        }

        public void Cool()
        {
            IsHit = false;
            gameObject.SetActive(true);
        }
    }
}