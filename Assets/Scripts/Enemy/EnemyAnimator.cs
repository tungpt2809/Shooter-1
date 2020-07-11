using UnityEngine;

namespace Enemy
{
    [RequireComponent(typeof(Animator))]
    public class EnemyAnimator : MonoBehaviour
    {
        private Animator _animator;
        private Vector2 _currentPos, _prevPos;
        private static readonly int Vertical = Animator.StringToHash("Vertical");
        private static readonly int Horizontal = Animator.StringToHash("Horizontal");

        private void Awake() => _animator = GetComponent<Animator>();

        private void Update()
        {
            _currentPos = transform.position;
            var deltaPos = (_currentPos - _prevPos);
            if (deltaPos.sqrMagnitude < 0.01) return;
            _animator.SetFloat(Horizontal, deltaPos.normalized.x);
            _animator.SetFloat(Vertical, deltaPos.normalized.y);
            _prevPos = _currentPos;
        }
    }
}