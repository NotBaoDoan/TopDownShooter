using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    [SerializeField] GameObject enemyPrefab;
    [SerializeField] float minSpawnTime = 1.0f;
    [SerializeField] float maxSpawnTime = 3.0f;

    float spawnDistance = 10f;
    Vector2 screenBoundary;
    Vector2 spawnPosition;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        SpawnEnemy();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SpawnEnemy()
    {
        float spawnTime = Random.Range(minSpawnTime, maxSpawnTime);
        screenBoundary = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));

        int side = Random.Range(0, 4);
        switch (side)
        {
            case 0:
                spawnPosition = new Vector2(Random.Range(-screenBoundary.x, screenBoundary.x), screenBoundary.y + spawnDistance);
                break;
            case 1:
                spawnPosition = new Vector2(Random.Range(-screenBoundary.x, screenBoundary.x), -screenBoundary.y - spawnDistance);
                break;
            case 2:
                spawnPosition = new Vector2(screenBoundary.x + spawnDistance, Random.Range(-screenBoundary.y, screenBoundary.y));
                break;
            case 3:
                spawnPosition = new Vector2(-screenBoundary.x - spawnDistance, Random.Range(-screenBoundary.y, screenBoundary.y));
                break;
        }
        Instantiate(enemyPrefab, spawnPosition, transform.rotation);
        Invoke("SpawnEnemy", spawnTime);
    }
}
