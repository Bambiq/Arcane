using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform target; // Reference to the player
    [SerializeField] private Vector3 offset = new Vector3(0f, 0f, -10f); // Default camera offset

    private void LateUpdate()
    {
        if (target == null) return;

        // Set camera position directly to target position plus offset
        transform.position = target.position + offset;
    }
}