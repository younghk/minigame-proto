using UnityEngine;

public class Archer : MonoBehaviour
{
    public GameObject arrowPrefab;
    public Transform firePoint;
    public float fireInterval = 0.8f;
    public float range = 20f;

    float nextFireTime;

    void Update()
    {
        if (Time.time < nextFireTime) return;
        if (arrowPrefab == null) return;

        Enemy[] enemies = Object.FindObjectsByType<Enemy>(FindObjectsSortMode.None);
        Transform nearest = null;
        float minDistSq = range * range;
        for (int i = 0; i < enemies.Length; i++)
        {
            float d = (enemies[i].transform.position - transform.position).sqrMagnitude;
            if (d < minDistSq)
            {
                minDistSq = d;
                nearest = enemies[i].transform;
            }
        }
        if (nearest == null) return;

        Vector3 archerCenter = transform.position;
        Vector3 dir = nearest.position - archerCenter;
        if (dir.sqrMagnitude < 0.0001f) return;
        dir.Normalize();

        Vector3 origin = firePoint != null ? firePoint.position : archerCenter + dir * 1.2f;
        Instantiate(arrowPrefab, origin, Quaternion.LookRotation(dir));
        nextFireTime = Time.time + fireInterval;
    }
}
