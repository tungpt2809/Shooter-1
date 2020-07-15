using System.Collections;
using GamePlay;
using ObjectPooling;
using Observer;
using UnityEngine;
using UnityEngine.SceneManagement;

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
        [SerializeField] private float speed = 0f, heath = 0f;

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

            GamePlayManager.Instance.player = this;
            CurrentHeath = heath;
        }

        private void Start()
        {
            EventDispatcher.Instance.OnPlayerShot.AddListener(PlayerShot);
            EventDispatcher.Instance.OnEnemyHitPlayer.AddListener(UpdateHeath);
            EventDispatcher.Instance.OnPlayerDeath.AddListener(PlayerDeath);
        }

        private void Update()
        {
            if (CurrentHeath < 1) return;
            Rotation();

            if (Input.GetMouseButtonDown(0))
            {
                EventDispatcher.Instance.OnPlayerShot.Invoke(currentWeapon);
            }

            transform.position = new Vector2(Mathf.Clamp(transform.position.x, -5.32f, 5.32f),
                Mathf.Clamp(transform.position.y, -2.7f, 2.7f));
        }

        private void FixedUpdate()
        {
            if (CurrentHeath < 1) return;
            Movement();
        }

        private void PlayerShot(Weapon.Weapon weapon)
        {
            weapon.Shoot(firePoint, ref _nextTimeOfFire);
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
            _moveVelocity = moveInput.normalized * speed;
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
            if (other.CompareTag("Enemy") || other.CompareTag("EnemyBullet"))
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
                if (CurrentHeath < 1)
                {
                    EventDispatcher.Instance.OnPlayerDeath.Invoke();
                }
            }
        }

        private void UpdateHeathBar()
        {
        }

        private void PlayerDeath()
        {
            StartCoroutine(Death());
        }

        private static IEnumerator Death()
        {
            PoolManager.Instance.CoolAllPool();
            yield return new WaitForSecondsRealtime(2f);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        public void ChangeWeapon(Weapon.Weapon weapon)
        {
            currentWeapon = weapon;
            currentWeaponSpr.sprite = weapon.currentWeaponSpr;
        }
    }
}