using UnityEngine;


public class SpawnManager : MonoBehaviour
{
    public GameObject[] enemyPrefabs;
    private float spawnRange = 9;
    private bool[] hasSpawnedPrefab;

    // Start is called before the first frame update
    void Start()
    {
        // Initialize the boolean array with the size of the enemyPrefabs array
        hasSpawnedPrefab = new bool[enemyPrefabs.Length];

        // Spawn each enemy prefab if it has not been spawned yet
        for (int i = 0; i < enemyPrefabs.Length; i++)
        {
            if (!hasSpawnedPrefab[i])
            {
                Instantiate(enemyPrefabs[i], GenerateSpawnPosition(), Quaternion.identity);
                hasSpawnedPrefab[i] = true;
            }
        }
    }

    private Vector3 GenerateSpawnPosition()
    {
        float spawnPosX = Random.Range(-spawnRange, spawnRange);
        float spawnPosZ = Random.Range(-spawnRange, spawnRange);
        Vector3 randomPos = new Vector3(spawnPosX, 0, spawnPosZ);
        return randomPos;
    }

    // Update is called once per frame
    void Update()
    {
        // You can add other functionality here if needed
    }
}
