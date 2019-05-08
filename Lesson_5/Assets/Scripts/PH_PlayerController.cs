using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PH_PlayerController : MonoBehaviour
{
    public float XSpeed = 5f;
    public float moveForce = 2f;
    public float jumpForce = 25f;
    public Transform firepoint;
    public Bullet bulletPrefab;

    private bool jumpFlag;
    private float xInput;
    private Rigidbody2D rigidbody;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (Input.GetButtonDown("Fire1")) Fire();
        xInput = Input.GetAxis("Horizontal");
        if (Input.GetButtonDown("Jump")) jumpFlag = true;
    }
    private void FixedUpdate()
    {
        if (Mathf.Abs(xInput) > 0.1f)
        {
            var moveDir = new Vector2(xInput * moveForce * Time.fixedDeltaTime, 0);
            rigidbody.AddForce(moveDir);
            if (Mathf.Abs(rigidbody.velocity.x) > XSpeed)
                rigidbody.velocity = new Vector2(XSpeed * Mathf.Sign(rigidbody.velocity.x), 0);
        }

        if(jumpFlag == true)
        {
            rigidbody.AddForce(Vector2.up*jumpForce,ForceMode2D.Impulse);
            jumpFlag = false;
        }
            
    }

    private void Fire()
    {
        Instantiate(bulletPrefab, firepoint.position, firepoint.rotation);

    }
}
