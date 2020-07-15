using System.Collections;
using ObjectPooling;
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
        [HideInInspector] public bool spawn = true;
        public Level[] levels;

        private int _score, _levelNumber;

        private void Start()
        {
            _levelNumber = 0;
        }

        private void Update()
        {
            if (_score >= levels[_levelNumber].scoreMax)
            {
                StartCoroutine(UpgradeThePlayer());
            }
        }

        public void AddScore(int amount)
        {
            _score += amount;
        }

        private IEnumerator UpgradeThePlayer()
        {
            _score = 0;
            _levelNumber++;
            player.ChangeWeapon(levels[_levelNumber].weapon);
            PoolManager.Instance.CoolAllPool();
            spawn = false;
            yield return new WaitForSeconds(2f);
            spawn = true;
        }
    }
}