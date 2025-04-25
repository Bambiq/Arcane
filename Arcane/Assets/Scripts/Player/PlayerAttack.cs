using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public GameObject Weapon;
    bool isAttacking = false;

    [SerializeField] private Animator anim;
    [SerializeField] private float attackCooldown = 0.5f;
    [SerializeField] private float nextAttackTime = 0f;

    private void Update()
    {
        CheckAttackTimer();

        if (Input.GetMouseButtonDown(0))
        {
            OnAttack();
        }
    }

    void OnAttack()
    {
        if (!isAttacking)
        {
            Weapon.SetActive(true);
            isAttacking = true;
            //anim.SetTrigger("Attack");
            Debug.Log("ju¿ zaatakowa³em");
        }
    }

    void CheckAttackTimer()
    {
        if (isAttacking)
        {
            nextAttackTime += Time.deltaTime;
            if (nextAttackTime >= attackCooldown)
            {
                nextAttackTime = 0;
                isAttacking = false;
                Weapon.SetActive(false);
            }
        }
    }
}
