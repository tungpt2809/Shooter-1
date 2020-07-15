using UnityEngine;

namespace Weapon
{
    public class WeaponSpawner : MonoBehaviour
    {
        [SerializeField] private WeaponPickUp[] weapons = null;

        private const float XBound = 4f, YBound = 2.5f, DeltaDelay = 3f;
        private float _delayTime = 0;

        private void Update()
        {
            if (_delayTime > DeltaDelay)
            {
                SpawnAWeapon();
                _delayTime = 0f;
                return;
            }

            _delayTime += Time.deltaTime;
        }

        private void SpawnAWeapon()
        {
            if (GameObject.FindGameObjectsWithTag("Weapon").Length > 2) return;
            var spawnPoint = new Vector2(Random.Range(-XBound, XBound), Random.Range(-YBound, YBound));
            Instantiate(weapons[Random.Range(0, weapons.Length)], spawnPoint, Quaternion.identity);
        }
    }
}