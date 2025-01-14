using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSpawner : MonoBehaviour
{
    public GameObject MobPrefab;
    public float radius = 1;
    public float objectsToSpawn = 3;
    
    private void Start()
    {
        SpawnObjectAtRandom();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SpawnObjectAtRandom();
        }
    }

    void SpawnObjectAtRandom()
    {
        Vector3 randomPos = Random.insideUnitCircle * radius;
        for (int i = 0; i < objectsToSpawn; i++)
        {
            randomPos = Random.insideUnitCircle * radius;

            Instantiate(MobPrefab, this.transform.position + randomPos, Quaternion.identity);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;

        Gizmos.DrawWireSphere(this.transform.position, radius);
    }
}
