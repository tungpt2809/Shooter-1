using UnityEngine.Events;

namespace Observer
{
    [System.Serializable]
    public class OnPlayerShot : UnityEvent<Weapon.Weapon> { }
    public class EventDispatcher : GenericSingleton<EventDispatcher>
    {
        public OnPlayerShot OnPlayerShot;
        public UnityEvent OnEnemyDeath;
        public UnityEvent OnEnemyHitPlayer;
        public UnityEvent OnPlayerDeath;

        public override void Awake()
        {
            OnPlayerShot = new OnPlayerShot();
        }
    }
}