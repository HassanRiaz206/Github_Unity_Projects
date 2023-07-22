using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed;
    private Rigidbody enemyRb;
    private List<Transform> enemies; // List to store references to all enemy prefabs
    private bool shouldAttackPlayer;
    private Renderer enemyRenderer;
    public Material normalMaterial; // Assign the normal texture in the Inspector
    public Material damagedMaterial;
    void Start()
    {
        
        enemyRb = GetComponent<Rigidbody>();
        enemies = new List<Transform>();

        // Find all the enemy prefabs in the scene and store their references in the list
        GameObject[] enemyObjects = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject enemy in enemyObjects)
        {
            if (enemy != gameObject) // Exclude itself from the list
                enemies.Add(enemy.transform);
        }

        // Randomly decide whether this enemy should attack the player or follow another enemy
        shouldAttackPlayer = Random.Range(0, 2) == 0;

        enemyRenderer = GetComponent<Renderer>();
        if (enemyRenderer != null)
            enemyRenderer.material = normalMaterial; // Set the normal texture initially
    }

    // Update is called once per frame
    void Update()
    {
        if (shouldAttackPlayer)
        {
            // If shouldAttackPlayer is true, attack the player
            Transform player = GameObject.FindGameObjectWithTag("Player").transform;
            Vector3 lookDirection = (player.position - transform.position).normalized;
            enemyRb.AddForce(lookDirection * speed);
        }
        else
        {
            // If shouldAttackPlayer is false, follow the nearest enemy
            Transform nearestEnemy = FindNearestEnemy();
            if (nearestEnemy != null)
            {
                Vector3 lookDirection = (nearestEnemy.position - transform.position).normalized;
                enemyRb.AddForce(lookDirection * speed);
            }
        }

    }


// Function to find the nearest enemy from the list
Transform FindNearestEnemy()
    {
        Transform nearestEnemy = null;
        float nearestDistance = Mathf.Infinity;

        foreach (Transform enemy in enemies)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.position);
            if (distanceToEnemy < nearestDistance)
            {
                nearestDistance = distanceToEnemy;
                nearestEnemy = enemy;
            }
        }

        return nearestEnemy;
    }
 
}

