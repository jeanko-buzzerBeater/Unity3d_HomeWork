using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PH_PlayerController : MonoBehaviour
{
    public float moveSpeed = 2f;
    public AudioClip jumpAudio;
    public AudioClip shootAudio;
    public float XSpeed = 5f;
    public float moveForce = 2f;
    public float jumpForce = 25f;
    public Transform firepoint;
    public Bullet bulletPrefab;
    public bool facingRight = true;

    private bool jumpFlag = false;
    private Rigidbody2D rigidbody;
    private AudioSource audioSource;
    private Transform groundCheck;
    private bool grounded = false;

    private Animator animator;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        groundCheck = transform.Find("GroundCheck");
    }

    private void Update()
    {
        grounded = Physics2D.Linecast(transform.position, groundCheck.position, 1 << LayerMask.NameToLayer("Ground"));
        float xInput = Input.GetAxis("Horizontal");

        transform.Translate(new Vector3(xInput, 0, 0) * Time.deltaTime * moveSpeed, Space.World);

        animator.SetFloat("VelocityX", rigidbody.velocity.x);
        if (Input.GetButtonDown("Fire1"))
        {
            Fire();
            audioSource.PlayOneShot(shootAudio);
        }
        if (Input.GetButtonUp("Fire1"))
        {
            animator.SetTrigger("BulletShoot");
        }
        if (Input.GetButtonDown("Jump") && grounded)
            jumpFlag = true;
        if (xInput > 0 && !facingRight) Flip();
        if (xInput < 0 && facingRight) Flip();
    }
    private void FixedUpdate()
    {
        //if (Mathf.Abs(xInput) > 0.1f)
        //{
        //    var moveDir = new Vector2(xInput * moveForce * Time.fixedDeltaTime, 0);
        //    rigidbody.AddForce(moveDir);
        //    if (Mathf.Abs(rigidbody.velocity.x) > XSpeed)
        //        rigidbody.velocity = new Vector2(XSpeed * Mathf.Sign(rigidbody.velocity.x), 0);
        //}

        if(jumpFlag == true)
        {
            rigidbody.AddForce(Vector2.up*jumpForce,ForceMode2D.Impulse);
            jumpFlag = false;
            audioSource.PlayOneShot(jumpAudio);
        }
            
    }

    private void Fire()
    {
        Instantiate(bulletPrefab, firepoint.position, firepoint.rotation);

    }
    void Flip()
    {
        facingRight = !facingRight;

        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
}
