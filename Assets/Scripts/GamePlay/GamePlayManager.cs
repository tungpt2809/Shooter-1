using System.Collections;
using ObjectPooling;
using Observer;
using UnityEngine;

namespace GamePlay
{
    [System.Serializable]
    public struct Level
    {
        public int scoreMax;
        public Weapon.Weapon weapon;
    }

    public class GamePlayManager : GenericSingleton<GamePlayManager>
    {
        [HideInInspector] public Player.Player player;
        [SerializeField] private Level[] levels = null;
        public bool Spawn { get; private set; } = true;

        private int _score, _levelNumber;

        private int Score
        {
            get => _score;
            set
            {
                _score = value;
                EventDispatcher.Instance.OnUpdateScore.Invoke(Score, levels[LevelNumber].scoreMax);
                if (Score >= levels[LevelNumber].scoreMax && LevelNumber + 1 < levels.Length)
                {
                    EventDispatcher.Instance.OnUpgradeLevel.Invoke(LevelNumber + 1);
                }
            }
        }

        public int LevelNumber
        {
            get => _levelNumber;
            private set
            {
                _levelNumber = value;
                if (LevelNumber < levels.Length)
                {
                    EventDispatcher.Instance.OnUpdateScore.Invoke(Score, levels[LevelNumber].scoreMax);
                }
            }
        }

        private void Start()
        {
            LevelNumber = 0;
            EventDispatcher.Instance.OnEnemyDeath.AddListener(AddScore);
            EventDispatcher.Instance.OnUpgradeLevel.AddListener(Upgrade);
        }

        private void AddScore(int amount)
        {
            Score += amount;
        }

        private void Upgrade(int levelNumber)
        {
            StartCoroutine(UpgradeThePlayer());
        }

        private IEnumerator UpgradeThePlayer()
        {
            Score = 0;
            LevelNumber++;
            player.ChangeWeapon(levels[LevelNumber].weapon);
            PoolManager.Instance.CoolAllPool();
            Spawn = false;
            yield return new WaitForSeconds(2f);
            Spawn = true;
        }
    }
}