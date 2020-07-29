using GamePlay;
using Observer;
using UnityEngine;
using UnityEngine.UI;

namespace Player
{
    [RequireComponent(typeof(HorizontalLayoutGroup))]
    public class PlayerHeathBar : MonoBehaviour
    {
        [SerializeField] private GameObject heathPoint = null;
        private float maxHeath;

        private void Start()
        {
            maxHeath = GamePlayManager.Instance.Player.CurrentHeath;
            for (var i = 0; i < maxHeath; i++)
            {
                Instantiate(heathPoint, transform);
            }

            EventDispatcher.Instance.OnUpdateHeath.AddListener(UpdateHeathBar);
        }

        private void UpdateHeathBar(float heath)
        {
            GetComponent<HorizontalLayoutGroup>().childControlWidth = false;
            for (var i = (int) heath; i < maxHeath; i++)
            {
                transform.GetChild(i).gameObject.SetActive(false);
            }
        }
    }
}