using System.Collections;
using ObjectPooling;
using UnityEngine;

namespace Bullet
{
    [RequireComponent(typeof(Rigidbody2D), typeof(Collider2D))]
    public class Bullet : MonoBehaviour
    {
        private PoolObjectType _type = PoolObjectType.None;
        private Animator _animator;
        private static readonly int Explosion = Animator.StringToHash("Explosion");
        public int Damage { get; private set; } = 0;

        public void InitBullet(PoolObjectType type, int iDamage, Transform firePoint, float force)
        {
            _type = type;
            Damage = iDamage;
            GetComponent<Rigidbody2D>().AddForce(firePoint.up * -force, ForceMode2D.Impulse);
            _animator = GetComponent<Animator>();
            Cooling();
        }

        private void Cooling()
        {
            StartCoroutine(Cool());
        }

        private IEnumerator Cool()
        {
            yield return new WaitForSeconds(3f);
            Hit();
        }

        private void CoolBullet()
        {
            PoolManager.Instance.CoolObject(gameObject, _type);
        }

        public void Hit()
        {
            if (_animator)
            {
                _animator.SetTrigger(Explosion);
            }
            else
            {
                CoolBullet();
            }
        }
    }
}