using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int maxHp = 3;
    public float moveSpeed = 2.5f;
    public float contactRange = 1.2f;
    public int contactDamage = 1;
    public float despawnZ = -8f;

    int hp;
    Transform archerTransform;

    void Awake()
    {
        hp = maxHp;
    }

    void Update()
    {
        transform.position += Vector3.back * moveSpeed * Time.deltaTime;

        if (transform.position.z < despawnZ)
        {
            Destroy(gameObject);
            return;
        }

        if (archerTransform == null)
        {
            var go = GameObject.Find("Archer");
            if (go != null) archerTransform = go.transform;
        }

        if (archerTransform != null)
        {
            Vector3 diff = archerTransform.position - transform.position;
            if (diff.sqrMagnitude < contactRange * contactRange)
            {
                if (GameManager.Instance != null)
                {
                    GameManager.Instance.TakePlayerDamage(contactDamage);
                }
                Destroy(gameObject);
            }
        }
    }

    public void TakeDamage(int amount)
    {
        hp -= amount;
        if (hp <= 0)
        {
            if (GameManager.Instance != null)
            {
                GameManager.Instance.AddScore(1);
            }
            Destroy(gameObject);
        }
    }
}
