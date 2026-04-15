using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public float spawnInterval = 1.4f;
    public float spawnZ = 25f;
    public float spawnXRange = 3.5f;
    public float startDelay = 1.0f;
    public bool autoStart = true;

    float nextSpawnTime;
    bool running;

    void Start()
    {
        if (autoStart)
        {
            StartSpawning();
        }
    }

    public void StartSpawning()
    {
        running = true;
        nextSpawnTime = Time.time + startDelay;
    }

    public void StopSpawning()
    {
        running = false;
    }

    void Update()
    {
        if (!running) return;
        if (enemyPrefab == null) return;
        if (GameManager.Instance != null && GameManager.Instance.isGameOver) return;
        if (Time.time < nextSpawnTime) return;

        float x = Random.Range(-spawnXRange, spawnXRange);
        Vector3 pos = new Vector3(x, 0.5f, spawnZ);
        Instantiate(enemyPrefab, pos, Quaternion.identity);
        nextSpawnTime = Time.time + spawnInterval;
    }
}
