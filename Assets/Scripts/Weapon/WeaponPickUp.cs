using UnityEngine;

namespace Weapon
{
    public class WeaponPickUp : MonoBehaviour
    {
        [SerializeField] private Weapon weapon = null;

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (!other.CompareTag("Player")) return;
            other.GetComponent<Player.Player>().ChangeWeapon(weapon);
            Destroy(gameObject);
        }
    }
}