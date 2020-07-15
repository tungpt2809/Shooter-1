using System.Collections;
using GamePlay;
using UnityEngine;

namespace Enemy
{
    public class EnemyRobot : MonoBehaviour
    {
        [SerializeField] private Weapon.Weapon robotWeapon = null;
        [SerializeField] private Transform gun1 = null, gun2 = null;

        private Rigidbody2D _playerRb;
        private Rigidbody2D _rb;
        private bool _isInRange = false, _nextShot = true;
        private float _nextTimeOfFire1, _nextTimeOfFire2;

        private void Awake()
        {
            _rb = GetComponent<Rigidbody2D>();
        }

        private void Update()
        {
            _isInRange = Vector2.Distance(transform.position, GamePlayManager.Instance.player.transform.position) <=
                         2.5f;
            if (_nextShot) StartCoroutine(RobotShoot());
        }

        private void FixedUpdate()
        {
            Rotation();
        }

        private void Rotation()
        {
            var direction = (GamePlayManager.Instance.player.GetComponent<Rigidbody2D>().position - _rb.position)
                .normalized;
            var angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg + 90;
            _rb.rotation = angle;
        }

        private IEnumerator RobotShoot()
        {
            _nextShot = false;

            if (_isInRange) robotWeapon.Shoot(gun1, ref _nextTimeOfFire1);
            yield return new WaitForSeconds(0.3f);

            if (_isInRange) robotWeapon.Shoot(gun2, ref _nextTimeOfFire2);
            yield return new WaitForSeconds(0.3f);

            _nextShot = true;
        }
    }
}