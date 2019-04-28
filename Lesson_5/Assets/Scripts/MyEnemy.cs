using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyEnemy : MonoBehaviour
{
    public int Health, AttackDamage;               // Колличество жизней и сила атаки
    public float MinDistance, Speed, ReloadTime;   // дисстанция на которой атакует, скорость и время перезарядки
    public GameObject Target, StartBullet;         //Цель(помещаем туда игрока, когда тот подходит)

    public bool Angry;                             // Проверка сагрили ли мы противника 

    // Проверка, смотрит ли вперед(направо) и проверка, идёт ли сейчас перезарядка 
    bool IsForward = true, Couldawn = false;
    Vector3 Dir = new Vector3(1, 0);


    private void FixedUpdate()
    {
        // Если противник нас заметил входим в режим злости
        if (Angry)
        {
            // Смотрим где находится игрок x<0 игрок слева, х>0 справа
            float x = Target.transform.position.x - transform.position.x;

            //разворачиваемся в зависимости от того, где игрок и куда смотрим

            if (x < 0 && IsForward)
            {
                Flip();
            }
            else if (x > 0 && !IsForward)
            {
                Flip();
            }

            // Если дистанция позволяет, то атакуем 
            if (Vector3.Distance(transform.position, Target.transform.position) <= MinDistance)
            {
                Engage();   //Метод атаки
            }
            //если мы далековато, то двигаемся к цели
            else
            {
                transform.position += Dir * Speed * Time.deltaTime;
            }
        }
        // Если мы не озлоблены завершаем работу кадра
        else
            return;
    }
    //метод, нанесения урона противнику
    public void Hurt(int Damage)
    {
        print("Ouch: " + Damage); // Выводим в консоль сообщение о кол-ве урона

        Health -= Damage;         // отнимаем жизни

        if (Health <= 0)          // если вдруг жизней 0, умираем
            Die();
    }
    //Просто удаляем со сцены
    void Die()
    {
        Destroy(gameObject);
    }

    //Метод Атаки
    void Engage()
    {
        // print(x);

        //Если мы не на перезарядке, то не атакуем
        if (!Couldawn)
        {
            Couldawn = true;     //Включение перезарядки
            Invoke("Relaod: ", ReloadTime); //вызываем таймер перезарядки
        }
    }

    //перезарядка 
    void Reload()
    {
        Couldawn = false;
    }
    // Касание нашего триггера 
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 9) //Если этот триггер игрок
        {
            Target = collision.gameObject; // Делаем ссылку на игрока 
            Angry = true;                  //Становимся агрессивными
        }
    }

    // разорот 
    void Flip()
    {
        IsForward = !IsForward;   // Запоминаем, что мы больше не смотрим вправо 

        Dir.x *= -1;              // Меняем направление движения 

        Vector3 V = transform.localScale; //Берём новую структуру Vector3 и копируем её с нашего scale
        V.x *= -1;                        //И меняем её ось Х в другую сторону

        //присваиваем свою структуру, так как напрямую к localScaale.x нам не обратиться 
        transform.localScale = V;
    }
}
