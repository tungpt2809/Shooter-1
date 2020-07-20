using System.Collections;
using System.Linq;
using GamePlay;
using ObjectPooling;
using UnityEngine;

namespace Bullet
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class Bullet : MonoBehaviour
    {
        [SerializeField] private SubBullet[] subBullets = null;
        private PoolObjectType _type = PoolObjectType.None;
        private Animator _animator;
        private static readonly int Explosion = Animator.StringToHash("Explosion");
        private int _damage = 0;

        public void InitBullet(PoolObjectType type, int damage, Transform firePoint, float force)
        {
            _type = type;
            _damage = damage;
            if (subBullets.Length > 0)
            {
                foreach (var t in subBullets)
                {
                    t.InitSubBullet(this, _damage);
                }
            }

            transform.eulerAngles =
                new Vector3(0f, 0f, GamePlayManager.Instance.Player.transform.rotation.eulerAngles.z - 90f);
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
            Hit(true);
        }

        private void CoolBullet()
        {
            PoolManager.Instance.CoolObject(gameObject, _type);
        }

        public void Hit(bool timeout)
        {
            if (_animator)
            {
                _animator.SetTrigger(Explosion);
            }
            else
            {
                if (!timeout)
                {
                    if (subBullets.Any(t => t.IsHit == false))
                    {
                        return;
                    }
                }

                CoolBullet();
                foreach (var t in subBullets)
                {
                    t.Cool();
                }
            }
        }
    }
}