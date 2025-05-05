using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [Header ("Health")]
    public float maxHealth = 100;
    public float currentHealth;

    [Header("iFrames")]
    [SerializeField] private float iFramesDuration = 5f;
    private bool isInvincible = false;

    private void Awake()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(float damageDeal)
    {
        if (isInvincible) return;

        currentHealth = Mathf.Clamp(currentHealth - damageDeal, 0, maxHealth);

        if (currentHealth > 0)
        {
            StartCoroutine(Invincibility());
        }
        else
        {
            //player die()
            GetComponent<PlayerMovement>().enabled = false;
        }
    }

    private IEnumerator Invincibility()
    {
        isInvincible = true;

        yield return new WaitForSeconds(iFramesDuration);

        isInvincible = false;
    }
}