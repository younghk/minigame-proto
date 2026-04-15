using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int maxHp = 3;
    int hp;

    void Awake()
    {
        hp = maxHp;
    }

    public void TakeDamage(int amount)
    {
        hp -= amount;
        if (hp <= 0)
        {
            Destroy(gameObject);
        }
    }
}
