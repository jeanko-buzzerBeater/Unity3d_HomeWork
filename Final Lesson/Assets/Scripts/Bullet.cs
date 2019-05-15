using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float moveSpeed = 10f;
    public GameObject BOOM;
    public int Damage;

    private void Awake()
    {
        Destroy(gameObject, 4);
    }

    private void Update()
    {
        transform.Translate(Vector3.right * moveSpeed * Time.deltaTime);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            collision.GetComponent<NewEnemy>().Hurt();
            Instantiate(BOOM, transform.position, transform.rotation); 
            Destroy(gameObject); 
        }
    }

    //private void OnCollisionEnter2D(Collision2D collision)
    //{
    //    Destroy(gameObject);
    //}
}
