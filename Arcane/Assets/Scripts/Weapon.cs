using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [Header("BulletSpawn")]
    public Transform bulletSpawnTransform;
    public GameObject bulletPrefab;

    [Header("AutoShoot")]
    public float fireRate = 5f;
    public float shootCooldown;
    public bool autoFireEnabled = false;
    private Transform targetedEnemy;

    [Header("Detection")]
    public float detectionRange = 3f;
    public LayerMask enemyLayer;

    private void Update()
    {
        Detection();
        AimAtTarget();
        AutoShoot();
    }

    void Detection()
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
    void AimAtTarget()
    {
        if (targetedEnemy == null) return;

        Vector2 direction = (targetedEnemy.position - transform.position).normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, angle);
    }

    void AutoShoot()
    {
        shootCooldown -= Time.deltaTime;

        if (!autoFireEnabled)
            return;

        if (shootCooldown <= 0f)
        {
            Shoot();
            shootCooldown = 1f / fireRate;
        }
    }

    public void Shoot()
    {
        Debug.Log("Strza³");
        Instantiate(bulletPrefab, bulletSpawnTransform.position, transform.rotation);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRange);
    }
}