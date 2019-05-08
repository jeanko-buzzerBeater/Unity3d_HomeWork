using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewEnemy : MonoBehaviour
{
    public float moveSpeed = 3f;
    public float damage = 1f;
    public LayerMask layerMask;
    public float raycastDistance = 5f;
    public float stopingDistance = 3f;
    public float attackDist = 2f;
    public GameObject startBullet,targetTransform;
    public Transform firepoint;

    public float angryTime = 3f;
    private bool isAngry;
    private float lastAngryTime;
    private Vector3? targetPos;

    private Rigidbody2D rigidbody;
    private Vector3 startPost;

    private void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        startPost = transform.position;

        StartCoroutine(InvFlip());
    }

    private void Update()
    {
        if(isAngry && Time.time - lastAngryTime >= angryTime)
        {
            isAngry = false;
            targetPos = null;
        }
    }

    private void FixedUpdate()
    {
        var hit = Physics2D.Raycast(transform.position, transform.right * transform.localScale.x, raycastDistance, layerMask);
        if (hit.collider != null)
        {
            isAngry = true;
            lastAngryTime = Time.time;
            targetPos = hit.collider.transform.position;

            Debug.DrawLine(transform.position, hit.point, Color.green);
        }
        else
        {
            Debug.DrawRay(transform.position, transform.right * transform.localScale.x * raycastDistance, Color.red);
        }

        if (isAngry)
        {
            if (targetPos.HasValue)
                MoveToPos(targetPos.Value);
            if (Vector3.Distance(transform.position, targetTransform.transform.position) < attackDist)
            {
               Attack();
            }
        }
        else
        {
            MoveToPos(startPost);
        }
    }

    private IEnumerator InvFlip()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);
            if(!isAngry && rigidbody.velocity.x < 0.1f)
            {
                Flip();
            }
        }
    }

    private void MoveToPos(Vector3 pos)
    {
        var deltaX = pos.x - transform.position.x;
        if (Mathf.Abs(deltaX) < stopingDistance) return;

        if (Mathf.Sign(deltaX) != Mathf.Sign(transform.localScale.x))
            Flip();

        rigidbody.velocity = transform.right * transform.localScale.x * moveSpeed;
    }

    private void Flip()
    {
        var newScale = transform.localScale;
        newScale.x *= -1;
        transform.localScale = newScale;
    }
    private void Attack()
    {
        Instantiate(startBullet, firepoint.position, firepoint.rotation);
    }
}
