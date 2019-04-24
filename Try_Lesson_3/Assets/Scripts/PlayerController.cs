using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 2f;
    public Transform firepoint;
    public Bullet bulletPrefab;

    private void Update()
    {
        var xInput = Input.GetAxis("Horizontal");

        transform.Translate(new Vector3(xInput, 0, 0)*Time.deltaTime*moveSpeed,Space.World);

        if (Input.GetButtonDown("Fire1")) Fire();
    }

    private void Fire()
    {
         Instantiate(bulletPrefab, firepoint.position, firepoint.rotation);

    }
}
