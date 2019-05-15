using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBullet : MonoBehaviour
{
    public GameObject BOOM; // Объект, который спаунится после взрыва
    public int Damage; // Урон, который мы нанесём
    public float Speed, LifeTime; // Скорость и время жизни снаряда
    Vector3 Dir = new Vector3(0, 0, 0); // Направление полёта
    void Start()
    {
        Dir.x = Speed; 
        Destroy(gameObject, LifeTime); 
    }
    void FixedUpdate()
    {
        transform.position += Dir; 
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy") 
       {
            collision.GetComponent<NewEnemy>().Hurt();
            Instantiate(BOOM, transform.position, transform.rotation); 
            Destroy(gameObject);
       }
    }
}