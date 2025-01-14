using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float health;
    public GameObject chasedObjcet;
    public float enemySpeed;
    public float disttanceBetween;

    private float distanseChase;
    
    private void Start()
    {
        chasedObjcet = GameObject.Find("Player");
    }

    private void FixedUpdate()
    {
        Chase();
    }

    public void Chase()
    {
        distanseChase = Vector2.Distance(transform.position, chasedObjcet.transform.position);
        Vector2 direction = chasedObjcet.transform.position - transform.position;
        direction.Normalize();
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        if (distanseChase < disttanceBetween)
        {
            transform.position = Vector2.MoveTowards(this.transform.position, chasedObjcet.transform.position, enemySpeed * Time.deltaTime);
            transform.rotation = Quaternion.Euler(Vector3.forward * angle);
        }
    }

    public void TakeDamage(float damage)
    {
        health -= damage;

        if (health <= 0f)
        {
            Destroy(gameObject);
        }
    }
}
