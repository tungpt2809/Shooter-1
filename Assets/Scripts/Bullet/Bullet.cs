using System.Collections;
using ObjectPooling;
using UnityEngine;
using UnityEngine.Serialization;

namespace Bullet
{
    [RequireComponent(typeof(Rigidbody2D), typeof(Collider2D))]
    public class Bullet : MonoBehaviour
    {
        private PoolObjectType _type = PoolObjectType.None;
        [HideInInspector] public int damage = 0;

        public void InitBullet(PoolObjectType type, int iDamage)
        {
            _type = type;
            damage = iDamage;
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