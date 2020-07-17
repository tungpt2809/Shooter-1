using GamePlay;
using UnityEngine;

namespace Enemy
{
    public class EnemyFollow : MonoBehaviour
    {
        [SerializeField] private float speed = 0f;
        [SerializeField] private float range = 0f;

        private void Update()
        {
            if (Vector2.Distance(transform.position, GamePlayManager.Instance.Player.transform.position) > range)
            {
                transform.position =
                    Vector2.MoveTowards(transform.position, GamePlayManager.Instance.Player.transform.position,
                        speed * Time.deltaTime);
            }
        }
    }
}