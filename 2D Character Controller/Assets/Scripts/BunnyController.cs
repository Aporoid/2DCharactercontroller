using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BunnyController : MonoBehaviour {

    private new Rigidbody2D rigidbody2D;

    public float maxSpeed = 10f;

    private bool facingRight = true;

    Animator anim;

    bool grounded = false;
    public Transform groundCheck;
    float groundRadius = 0.2f;
    public LayerMask whatIsGround;
    public float jumpForce = 700;
    
	void Start ()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
	}
	
	
	void FixedUpdate ()
    {
        grounded = Physics2D.OverlapCircle(groundCheck.position, groundRadius, whatIsGround);
        anim.SetBool("Ground", grounded);

        anim.SetFloat("vSpeed", rigidbody2D.velocity.y);

        float move = Input.GetAxis("Horizontal");

        anim.SetFloat("Speed", Mathf.Abs(move));

        rigidbody2D.velocity = new Vector2(move * maxSpeed, rigidbody2D.velocity.y);

        if(move > 0 && !facingRight)
        {
            Flip();
        }
        else if (move <0 && facingRight)
        {
            Flip();
        }
	}

    private void Update()
    {
        if(grounded && Input.GetKeyDown(KeyCode.Space)) //not a good idea, actually
        {
            anim.SetBool("Ground", false);
            rigidbody2D.AddForce(new Vector2(0, jumpForce));
        }
    }

    void Flip()
    {
        facingRight = !facingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
}
