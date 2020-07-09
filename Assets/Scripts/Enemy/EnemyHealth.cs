using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField]
    private float _health;

    public AudioClip deathClip;

    // Update is called once per frame
    void Update()
    {
        if (_health < 1)
        {
            Destroy(gameObject);
            // SoundManager.instance.playSounceFX(deathClip);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "PurpleBullet")
        {
            _health = GameObject.FindGameObjectWithTag("Player").GetComponent<Player.Player>().currentWeapon.damage;
            Destroy(collision);
        }
    }
}
