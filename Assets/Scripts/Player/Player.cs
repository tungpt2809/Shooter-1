using System.Collections;
using Observer;
using UnityEngine;

namespace Player
{
    [RequireComponent(typeof(Rigidbody2D), typeof(Collider2D), typeof(Animator))]
    public class Player : MonoBehaviour
    {
        [SerializeField] private Weapon.Weapon currentWeapon = null;
        [SerializeField] private SpriteRenderer currentWeaponSpr = null;
        [SerializeField] private Transform firePoint = null;
        private Rigidbody2D _myBody;
        private Animator _anim;
        [SerializeField] private Animator legAnim = null;
        [SerializeField] private float Speed = 0f, Heath = 0f;

        private Camera _main;
        private float _currentHeath;

        private float CurrentHeath
        {
            get => _currentHeath;
            set
            {
                _currentHeath = value;
                UpdateHeathBar();
            }
        }

        private Vector2 _moveVelocity;
        private bool _hit = true;
        private float _nextTimeOfFire;
        private static readonly int Moving = Animator.StringToHash("Moving");
        private static readonly int Hit = Animator.StringToHash("Hit");

        private void Awake()
        {
            _myBody = GetComponent<Rigidbody2D>();
            _anim = GetComponent<Animator>();
            _main = Camera.main;

            CurrentHeath = Heath;

            EventDispatcher.Instance.OnPlayerShot.AddListener(PlayerShot);
            EventDispatcher.Instance.OnEnemyHitPlayer.AddListener(UpdateHeath);
        }

        private void Update()
        {
            Rotation();

            if (Input.GetMouseButtonDown(0))
            {
                EventDispatcher.Instance.OnPlayerShot.Invoke();
            }

            transform.position = new Vector2(Mathf.Clamp(transform.position.x, -5.32f, 5.32f),
                Mathf.Clamp(transform.position.y, -2.7f, 2.7f));
        }

        private void FixedUpdate()
        {
            Movement();
        }

        private void PlayerShot()
        {
            currentWeapon.Shoot(firePoint, ref _nextTimeOfFire);
        }

        private void Rotation()
        {
            var dir = _main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
            var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg + 90;
            var rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, 10 * Time.deltaTime);
        }

        private void Movement()
        {
            var moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
            _moveVelocity = moveInput.normalized * Speed;
            _myBody.MovePosition(_myBody.position + _moveVelocity * Time.fixedDeltaTime);
            legAnim.SetBool(Moving, _moveVelocity != Vector2.zero);
        }

        private IEnumerator HitBoxOff()
        {
            _hit = false;
            _anim.SetTrigger(Hit);
            yield return new WaitForSeconds(1.5f);
            _hit = true;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Enemy"))
            {
                EventDispatcher.Instance.OnEnemyHitPlayer.Invoke();
            }
        }

        private void UpdateHeath()
        {
            if (_hit)
            {
                StartCoroutine(HitBoxOff());
                CurrentHeath--;
            }
        }

        private void UpdateHeathBar()
        {
        }
    }
}