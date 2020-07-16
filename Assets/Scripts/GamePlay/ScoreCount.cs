using Observer;
using UnityEngine;
using UnityEngine.UI;

namespace GamePlay
{
    public class ScoreCount : MonoBehaviour
    {
        [SerializeField] private Text scoreCount = null, scoreCountMax = null;

        private void UpdateScoreCount(int score, int scoreMax)
        {
            scoreCount.text = score.ToString();
            scoreCountMax.text = scoreMax.ToString();
        }

        private void Start()
        {
            EventDispatcher.Instance.OnUpdateScore.AddListener(UpdateScoreCount);
        }
    }
}