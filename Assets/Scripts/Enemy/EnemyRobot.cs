using System.Collections;
using UnityEngine;

namespace Enemy
{
    public class EnemyRobot : MonoBehaviour
    {
        [SerializeField] private Weapon.Weapon robotWeapon = null;
        [SerializeField] private Transform gun1 = null, gun2 = null;

        private Transform _playerPos;
        private Rigidbody2D _playerRb;
        private Rigidbody2D _rb;
        private bool isInRange = false, nextShot = true;
        private float _nextTimeOfFire1, _nextTimeOfFire2;

        void Awake()
        {
            _rb = GetComponent<Rigidbody2D>();
            _playerPos = GameObject.FindGameObjectWithTag("Player").transform;
            _playerRb = _playerPos.GetComponent<Rigidbody2D>();
        }

        void Update()
        {
            isInRange = Vector2.Distance(transform.position, _playerPos.position) <= 2.5f;
            if (nextShot) StartCoroutine(RobotShoot());
        }

        private void FixedUpdate()
        {
            Rotation();
        }

        void Rotation()
        {
            var direction = (_playerRb.position - _rb.position).normalized;
            var angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg + 90;
            _rb.rotation = angle;
        }

        private IEnumerator RobotShoot()
        {
            nextShot = false;

            if (isInRange) robotWeapon.Shoot(gun1, ref _nextTimeOfFire1);
            yield return new WaitForSeconds(0.3f);

            if (isInRange) robotWeapon.Shoot(gun2, ref _nextTimeOfFire2);
            yield return new WaitForSeconds(0.3f);

            nextShot = true;
        }
    }
}