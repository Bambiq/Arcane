using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Player Movement")]
    public float moveSpeed = 5f;

    private Vector2 movement;
    private Animator animator;
    public Transform playerTransform;

    void Start()
    {
        animator = GetComponentInChildren<Animator>();
    }

    void Update()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");

        movement = new Vector2(moveX, moveY).normalized;

        transform.Translate(movement * moveSpeed * Time.deltaTime);

        if (Input.GetAxisRaw("Horizontal") == 0 && Input.GetAxisRaw("Vertical") == 0)
        {
            animator.SetBool("isWalking", false);
        }
        else
        {
            animator.SetBool("isWalking", true);
        }

        Flip(moveX);
    }

    public void Flip(float moveX)
    {
        if (moveX > 0)
        {
            playerTransform.localRotation = Quaternion.Euler(0, 0, 0);
        }
        else if (moveX < 0)
        {
            playerTransform.localRotation = Quaternion.Euler(0, 180, 0);
        }
    }
}
