using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthBar : MonoBehaviour
{
    [SerializeField] private PlayerHealth playerHealth;
    [SerializeField] private Image healthBarFull;
    [SerializeField] private Image healthBarCurrent;
    private void Start()
    {
        healthBarFull.fillAmount = playerHealth.currentHealth / 100;
    }

    private void Update()
    {
        healthBarCurrent.fillAmount = playerHealth.currentHealth / 100;
    }
}