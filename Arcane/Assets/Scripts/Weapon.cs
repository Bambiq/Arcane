using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [Header("BulletSpawn")]
    public Transform bulletSpawnTransform;
    public GameObject bulletPrefab;

    [Header("DetectTarget")]
    private Transform targetedEnemy;
    public float detectionRange = 3f;
    public LayerMask enemyLayer;

    [Header("AutoShoot")]
    public float fireRate = 5f;
    public float shootCooldown;
    public bool autoFireEnabled = false;


    [Header("References")]
    public WeaponManager enemyDetector;
    
    private void Update()
    {
        DetectTarget();
        AimAtTarget();
        AutoShoot();
    }

    // Detect closest enemy
    void DetectTarget()
    {
        Collider2D[] enemies = Physics2D.OverlapCircleAll(transform.position, detectionRange, enemyLayer);

        float closestDistance = Mathf.Infinity;
        targetedEnemy = null;

        foreach (Collider2D enemy in enemies)
        {
            float distance = Vector2.Distance(transform.position, enemy.transform.position);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                targetedEnemy = enemy.transform;
            }
        }

        autoFireEnabled = targetedEnemy != null;
    }

    // Aim at that enemy
    void AimAtTarget()
    {
        if (targetedEnemy == null)
        {
            transform.rotation = Quaternion.identity; //Reset rotacji po straceniu celu??
        }
        else
        {
            Vector2 direction = (targetedEnemy.position - transform.position).normalized;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0f, 0f, angle);
        }
    }

    //Shoot with cooldown
    void AutoShoot()
    {
        shootCooldown -= Time.deltaTime;

        if (!autoFireEnabled)
            return;

        if (shootCooldown <= 0f)
        {
            Debug.Log("Strza³");
            Instantiate(bulletPrefab, bulletSpawnTransform.position, transform.rotation);
            shootCooldown = 1f / fireRate;
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRange);
    }
}