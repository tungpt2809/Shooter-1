using System.Collections;
using ObjectPooling;
using UnityEngine;

namespace Bullet
{
    public class Bullet : MonoBehaviour
    {
        private PoolObjectType _type = PoolObjectType.None;
        [HideInInspector] public int _damage = 0;

        public void InitBullet(PoolObjectType type, int damage)
        {
            _type = type;
            _damage = damage;
            Cooling();
        }

        private void Cooling()
        {
            StartCoroutine(Cool());
        }

        private IEnumerator Cool()
        {
            var poolManager = PoolManager.Instance;

            yield return new WaitForSeconds(3f);

            poolManager.CoolObject(gameObject, _type);
        }
    }
}