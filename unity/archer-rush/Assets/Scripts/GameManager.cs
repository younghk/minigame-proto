using UnityEngine;

public class GameManager : MonoBehaviour
{
    static GameManager _instance;

    public static GameManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = Object.FindAnyObjectByType<GameManager>();
            }
            return _instance;
        }
    }

    public int maxPlayerHp = 5;
    public int playerHp;
    public int score;
    public bool isGameOver;

    public System.Action<int> OnHpChanged;
    public System.Action<int> OnScoreChanged;
    public System.Action OnGameOver;

    void Awake()
    {
        _instance = this;
        playerHp = maxPlayerHp;
        score = 0;
        isGameOver = false;
    }

    public void TakePlayerDamage(int amount)
    {
        if (isGameOver) return;
        playerHp -= amount;
        if (playerHp < 0) playerHp = 0;
        OnHpChanged?.Invoke(playerHp);
        if (playerHp <= 0)
        {
            isGameOver = true;
            OnGameOver?.Invoke();
        }
    }

    public void AddScore(int amount)
    {
        if (isGameOver) return;
        score += amount;
        OnScoreChanged?.Invoke(score);
    }

    public void RestartGame()
    {
        var scene = UnityEngine.SceneManagement.SceneManager.GetActiveScene();
        UnityEngine.SceneManagement.SceneManager.LoadScene(scene.name);
    }
}
