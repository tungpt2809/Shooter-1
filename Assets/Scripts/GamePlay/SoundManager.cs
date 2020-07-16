using ObjectPooling;
using Observer;
using UnityEngine;

namespace GamePlay
{
    public class SoundManager : MonoBehaviour
    {
        [SerializeField] private AudioSource audioTheme = null;
        [SerializeField] private AudioSource soundFx = null;
        [SerializeField] private AudioClip[] themeSongs = null, shootClips = null;
        [SerializeField] private AudioClip enemyDeathClip = null, hitClip = null, playerDeathClip = null;

        private void Start()
        {
            if (!audioTheme.playOnAwake)
            {
                PlayAudioTheme();
            }

            EventDispatcher.Instance.OnPlayerShot.AddListener(PlayShoot);
            EventDispatcher.Instance.OnEnemyDeath.AddListener(PlayEnemyDeath);
            EventDispatcher.Instance.OnEnemyHitPlayer.AddListener(PlayHit);
            EventDispatcher.Instance.OnPlayerDeath.AddListener(PlayPlayerDeath);
        }

        private void Update()
        {
            if (!audioTheme.isPlaying)
            {
                PlayAudioTheme();
            }
        }

        private void PlayAudioTheme()
        {
            audioTheme.clip = (themeSongs[Random.Range(0, themeSongs.Length)]);
            audioTheme.Play();
        }

        private void PlaySoundFx(AudioClip clip)
        {
            soundFx.clip = clip;
            soundFx.volume = Random.Range(0.3f, 0.5f);
            soundFx.Play();
        }

        private void PlayShoot(Weapon.Weapon currentWeapon)
        {
            PlaySoundFx(currentWeapon.type <= PoolObjectType.WaveBullet
                ? shootClips[Random.Range(0, shootClips.Length - 1)]
                : shootClips[shootClips.Length]);
        }

        private void PlayEnemyDeath(int score)
        {
            PlaySoundFx(enemyDeathClip);
        }

        private void PlayHit()
        {
            PlaySoundFx(hitClip);
        }

        private void PlayPlayerDeath()
        {
            PlaySoundFx(playerDeathClip);
        }
    }
}