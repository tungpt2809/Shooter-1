using ObjectPooling;
using UnityEngine;

namespace Weapon
{
    [CreateAssetMenu(fileName = "New Weapon", menuName = "Weapon")]
    public class Weapon : ScriptableObject
    {
        public Sprite currentWeaponSpr;
        public PoolObjectType type;
        public float fireRate = 1;
        public float force = 1;
        public int damage = 20;

        public void Shoot(Transform firePoint, ref float nextTimeOfFire)
        {
            if (Time.time >= nextTimeOfFire)
            {
                ShotABullet(firePoint);
                nextTimeOfFire = Time.time + 1 / fireRate;
            }
        }

        private void ShotABullet(Transform firePoint)
        {
            var poolManager = PoolManager.Instance;
            var ob = poolManager.GetPoolObject(type);

            ob.transform.position = firePoint.position;
            ob.gameObject.SetActive(true);
            ob.GetComponent<Bullet.Bullet>().InitBullet(type, damage, firePoint, force);
        }
    }
}