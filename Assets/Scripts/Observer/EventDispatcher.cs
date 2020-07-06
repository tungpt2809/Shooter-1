using UnityEngine.Events;

namespace Observer
{
    public class EventDispatcher : GenericSingleton<EventDispatcher>
    {
        public UnityEvent OnPlayerShot;
        public UnityEvent OnBulletHitEnemy;
        public UnityEvent OnEnemyDead;
        public UnityEvent OnEnemyHitPlayer;
    }
}