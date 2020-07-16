using GamePlay;
using UnityEngine;

namespace Player
{
    [RequireComponent(typeof(Camera))]
    public class CameraFollow : MonoBehaviour
    {
        private void Update()
        {
            var playerPos = GamePlayManager.Instance.player.transform.position;
            transform.position = new Vector3(playerPos.x, playerPos.y, transform.position.z);
        }
    }
}