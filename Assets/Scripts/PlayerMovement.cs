using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public float playerMoveSpeed = 15.0f;
    public float jumpingPower = 8.0f;

    private float horizontal = 0.0f;
    private bool jump = false;
    private bool isFacingRight = true;
    
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private SpriteRenderer sr;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private Animator animator;
    


    // Start is called before the first frame update
    void Start()
    {
        
    }

    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.1f, groundLayer);
        // return true;
    }

    private float RunningValue()
    {
        if (!IsGrounded()) {
            return 0.0f;
        }
        else {
            return Mathf.Abs(rb.velocity.x);
        }
    }

    // Update is called once per frame
    void Update()
    {
        horizontal = Input.GetAxis("Horizontal") * playerMoveSpeed;
        animator.SetFloat("PlayerSpeed", Mathf.Abs(horizontal));

        // special move
        if (Input.GetButtonDown("Fire1"))
        {
            animator.SetTrigger("specialMove"); 
            horizontal = 0.0f;
        }

        // changes direction
        if ((isFacingRight && horizontal < 0.0f) || (!isFacingRight && horizontal > 0.0f)) 
        {
            isFacingRight = !isFacingRight;
        }

        animator.SetBool("jump",(!IsGrounded()));

        // Makes jump happen
        if (Input.GetButtonDown("Jump") && IsGrounded())
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpingPower);
        }  

        // changes the sensitivity of the jump button
        if (Input.GetButtonUp("Jump") && rb.velocity.y > 0.0f)
        {
            rb. velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
        }    

    }

    // recommended to use this for real time (i.e., delta time conversions)
    private void FixedUpdate()
    {
        rb.velocity = new Vector2(horizontal * playerMoveSpeed * Time.deltaTime, rb.velocity.y);
        animator.SetFloat("running", RunningValue());
        sr.flipX = !isFacingRight;
    }
}
