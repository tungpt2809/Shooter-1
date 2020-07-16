using UnityEngine.Events;

namespace Observer
{
    [System.Serializable]
    public class PlayerShotEvent : UnityEvent<Weapon.Weapon>
    {
    }

    [System.Serializable]
    public class EnemyDeathEvent : UnityEvent<int>
    {
    }

    [System.Serializable]
    public class UpdateHeathEvent : UnityEvent<float>
    {
    }

    [System.Serializable]
    public class UpdateScoreEvent : UnityEvent<int, int>
    {
    }

    [System.Serializable]
    public class UpgradeLevelEvent : UnityEvent<int>
    {
    }

    public class EventDispatcher : GenericSingleton<EventDispatcher>
    {
        public PlayerShotEvent OnPlayerShot { get; private set; }
        public EnemyDeathEvent OnEnemyDeath { get; private set; }
        public UnityEvent OnEnemyHitPlayer { get; private set; }
        public UnityEvent OnPlayerDeath { get; private set; }
        public UpdateHeathEvent OnUpdateHeath { get; private set; }
        public UpdateScoreEvent OnUpdateScore { get; private set; }
        public UpgradeLevelEvent OnUpgradeLevel { get; private set; }

        public override void Awake()
        {
            OnPlayerShot = new PlayerShotEvent();
            OnEnemyDeath = new EnemyDeathEvent();
            OnEnemyHitPlayer = new UnityEvent();
            OnPlayerDeath = new UnityEvent();
            OnUpdateHeath = new UpdateHeathEvent();
            OnUpdateScore = new UpdateScoreEvent();
            OnUpgradeLevel = new UpgradeLevelEvent();
        }
    }
}