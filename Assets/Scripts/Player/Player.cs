using System.Collections;
using Observer;
using UnityEngine;

namespace Player
{
    public class Player : MonoBehaviour, IPlayer
    {
        public Weapon.Weapon currentWeapon;
        [SerializeField] private SpriteRenderer currentWeaponSpr = null;
        [SerializeField] private Transform firePoint = null;
        private Rigidbody2D _myBody;
        private Animator _anim;
        [SerializeField] private Animator legAnim = null;

        private Camera _main;
        public float speed { get; private set; }
        public float heath { get; private set; }
        private float _currentHeath;

        private float Heath
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
        private float _nextTimeOfFire = 0f;
        private static readonly int Moving = Animator.StringToHash("Moving");
        private static readonly int Hit = Animator.StringToHash("Hit");

        private void Awake()
        {
            _myBody = GetComponent<Rigidbody2D>();
            _anim = GetComponent<Animator>();
            _main = Camera.main;

            speed = 2;
            heath = 10;
            Heath = heath;

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

        public void PlayerShot()
        {
            currentWeapon.Shoot(firePoint, ref _nextTimeOfFire);
        }

        public void Rotation()
        {
            var dir = _main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
            var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg + 90;
            var rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, 10 * Time.deltaTime);
        }

        public void Movement()
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
            if (other.CompareTag("Enemy"))
            {
                EventDispatcher.Instance.OnEnemyHitPlayer.Invoke();
            }
        }

        public void UpdateHeath()
        {
            if (_hit)
            {
                StartCoroutine(HitBoxOff());
                Heath--;
            }
        }

        private void UpdateHeathBar()
        {
        }
    }
}