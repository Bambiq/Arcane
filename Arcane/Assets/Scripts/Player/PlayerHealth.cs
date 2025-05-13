using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public GameObject DiedScreen;

    [Header ("Health")]
    public float maxHealth = 100;
    public float currentHealth;
    public GameObject Player;

    [Header("iFrames")]
    [SerializeField] private float iFramesDuration = 5f;
    public bool isInvincible = false;
    
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
            Die();
        }
    }

    private IEnumerator Invincibility()
    {
        isInvincible = true;

        yield return new WaitForSeconds(iFramesDuration);

        isInvincible = false;
    }

    void Die()
    {
        Object.Destroy(Player);
        GetComponent<PlayerMovement>().enabled = false;
        DiedScreen.SetActive(true);
    }
}