using UnityEngine;

namespace Enemy
{
    [RequireComponent(typeof(Animator))]
    public class EnemyAnimator : MonoBehaviour
    {
        private Animator _animator;
        private Vector2 _currentPos, _prevPos;
        private float _horizontal, _vertical;
        private static readonly int Vertical = Animator.StringToHash("Vertical");
        private static readonly int Horizontal = Animator.StringToHash("Horizontal");

        private void Awake() => _animator = GetComponent<Animator>();

        private void Update()
        {
            _currentPos = transform.position;
            _horizontal = _currentPos.x - _prevPos.x;
            _vertical = _currentPos.y - _prevPos.y;
            _animator.SetFloat(Horizontal, _horizontal);
            _animator.SetFloat(Vertical, _vertical);
            _prevPos = _currentPos;
        }
    }
}