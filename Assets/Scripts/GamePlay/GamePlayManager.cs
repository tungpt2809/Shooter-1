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
        [SerializeField] private Level[] levels = null;
        public bool Spawn { get; private set; } = true;
        private Player.Player _player;
        private int _score, _levelNumber;

        public Player.Player Player
        {
            get => _player;
            set
            {
                _player = value;
                ResetLevel();
            }
        }

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
            ResetLevel();
            EventDispatcher.Instance.OnEnemyDeath.AddListener(AddScore);
            EventDispatcher.Instance.OnUpgradeLevel.AddListener(Upgrade);
        }

        private void ResetLevel()
        {
            LevelNumber = 0;
            Score = 0;
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
            Player.ChangeWeapon(levels[LevelNumber].weapon);
            PoolManager.Instance.CoolAllPool();
            Spawn = false;
            yield return new WaitForSeconds(2f);
            Spawn = true;
        }
    }
}