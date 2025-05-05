using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    public float detectionRange = 3f;
    public LayerMask enemyLayer;
    private Transform targetedEnemy;

    //Detect closest enemy
    public Transform DetectClosestEnemy()
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
        return targetedEnemy;
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRange);
    }
}