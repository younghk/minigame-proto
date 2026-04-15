using UnityEngine;

public class GateSpawner : MonoBehaviour
{
    public GameObject gatePrefab;
    public float spawnInterval = 8f;
    public float spawnZ = 25f;
    public float spawnXOffset = 2.2f;
    public float firstSpawnDelay = 4f;

    float nextSpawnTime;

    void Start()
    {
        nextSpawnTime = Time.time + firstSpawnDelay;
    }

    void Update()
    {
        if (gatePrefab == null) return;
        if (GameManager.Instance != null && GameManager.Instance.isGameOver) return;
        if (Time.time < nextSpawnTime) return;

        bool leftFireRate = Random.value > 0.5f;
        SpawnOne(new Vector3(-spawnXOffset, 1f, spawnZ), leftFireRate ? GateBuffType.FireRate : GateBuffType.Damage);
        SpawnOne(new Vector3(spawnXOffset, 1f, spawnZ), leftFireRate ? GateBuffType.Damage : GateBuffType.FireRate);

        nextSpawnTime = Time.time + spawnInterval;
    }

    void SpawnOne(Vector3 pos, GateBuffType type)
    {
        var go = Instantiate(gatePrefab, pos, Quaternion.identity);
        var gate = go.GetComponent<Gate>();
        if (gate != null) gate.buffType = type;
    }
}
