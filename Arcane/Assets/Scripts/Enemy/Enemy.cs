using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float Hitpoints;
    public float MaxHitpoints = 100;
    public HealthManager Healthbar;

    [SerializeField] private float damageDeal;

    public GameObject chasedObjcet;
    public float enemySpeed;
    public float disttanceBetween;
    private float distanseChase;
    
    private void Start()
    {
        chasedObjcet = GameObject.Find("Player");
        Hitpoints = MaxHitpoints;
        Healthbar.SetHealth(Hitpoints, MaxHitpoints);
    }

    private void Update()
    {
        Chase();
    }

    public void Chase()
    {
        if (!chasedObjcet) return;

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
        Hitpoints -= damage;
        Healthbar.SetHealth(Hitpoints, MaxHitpoints);

        if (Hitpoints <= 0f)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            collision.GetComponent<PlayerHealth>().TakeDamage(damageDeal);
        }
    }
    void OnDestroy()
    {
        if (GameObject.FindGameObjectWithTag("WaveSpawner") != null)
        {
            GameObject.FindGameObjectWithTag("WaveSpawner").GetComponent<WaveSpawner>().spawnedEnemies.Remove(gameObject);
        }

    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        Gizmos.DrawWireSphere(this.transform.position, disttanceBetween);
    }
}
