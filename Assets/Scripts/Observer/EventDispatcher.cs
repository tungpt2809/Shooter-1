using UnityEngine.Events;

namespace Observer
{
    [System.Serializable]
    public class OnPlayerShot : UnityEvent<Weapon.Weapon>
    {
    }

    [System.Serializable]
    public class OnUpdateHeath : UnityEvent<float>
    {
    }

    public class EventDispatcher : GenericSingleton<EventDispatcher>
    {
        public OnPlayerShot OnPlayerShot;
        public UnityEvent OnEnemyDeath;
        public UnityEvent OnEnemyHitPlayer;
        public UnityEvent OnPlayerDeath;
        public OnUpdateHeath OnUpdateHeath;

        public override void Awake()
        {
            OnPlayerShot = new OnPlayerShot();
            OnUpdateHeath = new OnUpdateHeath();
        }
    }
}