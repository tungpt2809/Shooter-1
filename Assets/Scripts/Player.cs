using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
	public GameObject bullet;
	private Rigidbody2D myBody;
	private Animator legAnim;
    public float speed;
    private Vector2 moveVelocity;

    private void Awake()
    {
        myBody = GetComponent<Rigidbody2D>();
        legAnim = transform.GetChild(2).GetComponent<Animator>();
    }
    private void Update()
    {
    	Rotation();
    	//Ban
    	if(Input.GetMouseButtonDown(0))
    	{
    		Instantiate(bullet, transform.position, Quaternion.identity);
    	}
    	//Bo goc
    	transform.position = new Vector2(Mathf.Clamp(transform.position.x, -5.32f , 5.32f),Mathf.Clamp(transform.position.y, -2.7f , 2.7f));
    }
     void FixedUpdate()
    {
        Movement();
    }
    void Rotation()
    {
    	Vector2 dir = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
    	float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg + 90;
    	Quaternion rotation = Quaternion.AngleAxis(angle,Vector3.forward);
    	transform.rotation = Quaternion.Slerp(transform.rotation, rotation,10 * Time.deltaTime);
    }
    void Movement()
    {
        Vector2 moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        moveVelocity = moveInput.normalized * speed;
        myBody.MovePosition(myBody.position + moveVelocity * Time.fixedDeltaTime);
        if(moveVelocity == Vector2.zero)
        {
        	legAnim.SetBool("Moving", false);
        }
        else {
        	legAnim.SetBool("Moving",true);
        }
    }
}
