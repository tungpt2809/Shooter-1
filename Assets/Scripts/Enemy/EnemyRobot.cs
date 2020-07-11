using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRobot : MonoBehaviour
{
    public Transform gun1, gun2;
    public GameObject bullet;

    private Transform _playerPos;
    private Rigidbody2D _rb;

    public float speed = .3f;
    //private bool isInRange = false;

    void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _playerPos = GameObject.FindGameObjectWithTag("Player").transform;
    }
    //public void Start()
    //{
        //StartCoroutine(Shoot());
    //}

    void Update()
    {
        if (Vector2.Distance(transform.position, _playerPos.position) > 2.5f)
        {
            transform.position = Vector2.MoveTowards(transform.position, _playerPos.position, speed * Time.deltaTime);
            //isInRange = false;
        }
        //else { isInRange = true; }
    }

    private void FixedUpdate()
    {
        Rotation();
    }

    void Rotation()
    {
        Vector2 direction = (_playerPos.gameObject.GetComponent<Rigidbody2D>().position - _rb.position).normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg + 90;
        _rb.rotation = angle;
    }

    //IEnumerator Shoot()
    //{
        //if (isInRange)
        //    Instantiate(bullet, gun1.position, Quaternion.identity);
        //yield return new WaitForSeconds(.3f);

        //if (isInRange)
        //    Instantiate(bullet, gun2.position, Quaternion.identity);

        //yield return new WaitForSeconds(.3f);
        //StartCoroutine(Shoot());
    //}

}
