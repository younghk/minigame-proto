using UnityEngine;

public enum GateBuffType { FireRate, Damage }

public class Gate : MonoBehaviour
{
    public GateBuffType buffType = GateBuffType.FireRate;
    public float moveSpeed = 2.5f;
    public float despawnZ = -8f;

    public float fireRateMultiplier = 0.75f;
    public int damageBonus = 1;

    public float triggerXHalf = 1.0f;
    public float triggerZHalf = 0.6f;

    bool applied;

    void Start()
    {
        var mr = GetComponent<MeshRenderer>();
        if (mr != null)
        {
            Color c = buffType == GateBuffType.FireRate
                ? new Color(0.3f, 0.9f, 0.4f)
                : new Color(1f, 0.55f, 0.2f);
            mr.material.color = c;
        }
    }

    void Update()
    {
        transform.position += Vector3.back * moveSpeed * Time.deltaTime;

        if (transform.position.z < despawnZ)
        {
            Destroy(gameObject);
            return;
        }

        if (applied) return;

        var archer = Object.FindAnyObjectByType<Archer>();
        if (archer == null) return;

        Vector3 apos = archer.transform.position;
        Vector3 gpos = transform.position;
        if (Mathf.Abs(apos.x - gpos.x) > triggerXHalf) return;
        if (Mathf.Abs(apos.z - gpos.z) > triggerZHalf) return;
        if (Mathf.Abs(apos.y - gpos.y) > 3f) return;

        applied = true;
        ApplyBuff(archer);
        Destroy(gameObject);
    }

    void ApplyBuff(Archer archer)
    {
        switch (buffType)
        {
            case GateBuffType.FireRate:
                archer.fireInterval = Mathf.Max(0.1f, archer.fireInterval * fireRateMultiplier);
                break;
            case GateBuffType.Damage:
                archer.arrowDamage += damageBonus;
                break;
        }
    }
}
