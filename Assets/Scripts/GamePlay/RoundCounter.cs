using System.Collections;
using Observer;
using TMPro;
using UnityEngine;

namespace GamePlay
{
    [RequireComponent(typeof(TextMeshProUGUI))]
    public class RoundCounter : MonoBehaviour
    {
        private TextMeshProUGUI txt;

        private void Awake()
        {
            txt = GetComponent<TextMeshProUGUI>();
        }

        private void Start()
        {
            EventDispatcher.Instance.OnUpgradeLevel.AddListener(RoundComplete);
            EventDispatcher.Instance.OnPlayerDeath.AddListener(GameOver);
        }

        private void RoundComplete(int levelNumber)
        {
            StartCoroutine(Complete(levelNumber));
        }

        private IEnumerator Complete(int levelNumber)
        {
            txt.text = $"Round {levelNumber.ToString()} complete!";
            yield return new WaitForSeconds(2f);
            txt.text = $"";
        }

        private void GameOver()
        {
            StartCoroutine(PlayerDeath());
        }

        private IEnumerator PlayerDeath()
        {
            txt.text = "Game Over!";
            yield return new WaitForSeconds(2f);
            txt.text = "";
        }
    }
}