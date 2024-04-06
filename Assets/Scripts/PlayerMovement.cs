using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public float playerMoveSpeed = 15.0f;
    public float jumpingPower = 8.0f;
    public float attackPower = 5.0f;
    public float attackRange = 0.5f;
    public float attackDelay = 0.5f;
    public float overlap = 0.1f;

    private float horizontal = 0.0f;
    private bool jump = false;
    private bool isFacingRight = true;
    private Vector2 forceDirection = Vector2.right;
    
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private SpriteRenderer sr;
    [SerializeField] private Transform attackPoint;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private LayerMask foregroundLayer;
    [SerializeField] private Animator animator;
    

    // Start is called before the first frame update
    void Start()
    {

    }

    void Attack()
    {
        animator.SetTrigger("specialMove"); 
        horizontal = 0.0f;

        new WaitForSeconds(attackDelay);
    
    // Loop for hitting enemies taken from Brackey's "MELEE COMBAT in Unity" video, shows the range of the attack 
       Collider2D[] hitObjects =  Physics2D.OverlapCircleAll(attackPoint.position, attackRange, foregroundLayer);
       foreach(Collider2D hitObject in hitObjects)
       {
           Debug.Log("Hit " + hitObject.name);

           Rigidbody2D attackedRigidBody = hitObject.attachedRigidbody;
           // add velocity to game object with a time offset


           if (!isFacingRight) 
           {
               forceDirection = Vector2.left;
               Debug.Log("Direction set to left");
           }          
           else 
           {
               forceDirection = Vector2.right;
               Debug.Log("Direction set to right");
           }
           attackedRigidBody.AddForce(forceDirection * attackPower, ForceMode2D.Force);
       }

    }
    // From Brackey's "MELEE COMBAT in Unity" video, shows the range of the attack point
    void OnDrawGizmosSelected()
    {
        if (attackPoint == null) 
        {
            return;
        }
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }

    private bool IsGrounded()
    {
        // considered grounded if ontop of something on the foreground (i.e., a block) or the ground
        return (Physics2D.OverlapCircle(groundCheck.position, overlap, groundLayer) || Physics2D.OverlapCircle(groundCheck.position, overlap, foregroundLayer));
    }



    private float RunningValue()
    {
        if (!IsGrounded()) 
        {
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
            Attack();
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
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
        }    

    }

    // recommended to use this for real time (i.e., delta time conversions)
    private void FixedUpdate()
    {
        rb.velocity = new Vector2(horizontal * playerMoveSpeed * Time.deltaTime, rb.velocity.y);
        animator.SetFloat("running", RunningValue());
        sr.flipX = !isFacingRight;

        rb.velocity = new Vector2(horizontal * playerMoveSpeed * Time.deltaTime, rb.velocity.y);
    }
}
