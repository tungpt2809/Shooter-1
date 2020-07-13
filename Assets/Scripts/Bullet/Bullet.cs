using System.Collections;
using ObjectPooling;
using UnityEngine;

namespace Bullet
{
    [RequireComponent(typeof(Rigidbody2D), typeof(Collider2D))]
    public class Bullet : MonoBehaviour
    {
        private PoolObjectType _type = PoolObjectType.None;
        [HideInInspector] public int damage = 0;

        public void InitBullet(PoolObjectType type, int iDamage, Transform firePoint, float force)
        {
            _type = type;
            damage = iDamage;
            GetComponent<Rigidbody2D>().AddForce(firePoint.up * -force, ForceMode2D.Impulse);
            Cooling();
        }

        private void Cooling()
        {
            StartCoroutine(Cool());
        }

        private IEnumerator Cool()
        {
            yield return new WaitForSeconds(3f);

            CoolBullet();
        }

        public void CoolBullet()
        {
            PoolManager.Instance.CoolObject(gameObject, _type);
        }
    }
}