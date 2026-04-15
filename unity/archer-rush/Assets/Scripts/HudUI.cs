using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem.UI;

public class HudUI : MonoBehaviour
{
    Text hpText;
    Text scoreText;
    GameObject gameOverPanel;
    Text gameOverText;
    Font uiFont;

    void Start()
    {
        uiFont = Resources.GetBuiltinResource<Font>("LegacyRuntime.ttf");
        BuildUI();

        var gm = GameManager.Instance;
        if (gm != null)
        {
            gm.OnHpChanged += UpdateHp;
            gm.OnScoreChanged += UpdateScore;
            gm.OnGameOver += ShowGameOver;
            UpdateHp(gm.playerHp);
            UpdateScore(gm.score);
        }
    }

    void OnDestroy()
    {
        var gm = GameManager.Instance;
        if (gm != null)
        {
            gm.OnHpChanged -= UpdateHp;
            gm.OnScoreChanged -= UpdateScore;
            gm.OnGameOver -= ShowGameOver;
        }
    }

    void BuildUI()
    {
        var canvasGo = new GameObject("HudCanvas");
        var canvas = canvasGo.AddComponent<Canvas>();
        canvas.renderMode = RenderMode.ScreenSpaceOverlay;
        canvas.sortingOrder = 100;
        var scaler = canvasGo.AddComponent<CanvasScaler>();
        scaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
        scaler.referenceResolution = new Vector2(1080, 1920);
        scaler.screenMatchMode = CanvasScaler.ScreenMatchMode.MatchWidthOrHeight;
        scaler.matchWidthOrHeight = 0.5f;
        canvasGo.AddComponent<GraphicRaycaster>();

        hpText = MakeText("HPText", canvasGo.transform, new Vector2(0, 1), new Vector2(0, 1),
                          new Vector2(60, -60), new Vector2(360, 72), TextAnchor.MiddleLeft, 48, "HP: 5");
        scoreText = MakeText("ScoreText", canvasGo.transform, new Vector2(1, 1), new Vector2(1, 1),
                             new Vector2(-60, -60), new Vector2(420, 72), TextAnchor.MiddleRight, 48, "Score: 0");

        gameOverPanel = new GameObject("GameOverPanel");
        gameOverPanel.transform.SetParent(canvasGo.transform, false);
        var img = gameOverPanel.AddComponent<Image>();
        img.color = new Color(0f, 0f, 0f, 0.75f);
        var rt = gameOverPanel.GetComponent<RectTransform>();
        rt.anchorMin = Vector2.zero;
        rt.anchorMax = Vector2.one;
        rt.offsetMin = Vector2.zero;
        rt.offsetMax = Vector2.zero;

        gameOverText = MakeText("GameOverText", gameOverPanel.transform, new Vector2(0.5f, 0.5f), new Vector2(0.5f, 0.5f),
                                new Vector2(0, 120), new Vector2(900, 300), TextAnchor.MiddleCenter, 90, "GAME OVER");

        var btnGo = new GameObject("RestartButton");
        btnGo.transform.SetParent(gameOverPanel.transform, false);
        var btnImg = btnGo.AddComponent<Image>();
        btnImg.color = new Color(1f, 1f, 1f, 0.95f);
        var btn = btnGo.AddComponent<Button>();
        var btnRt = btnGo.GetComponent<RectTransform>();
        btnRt.anchorMin = new Vector2(0.5f, 0.5f);
        btnRt.anchorMax = new Vector2(0.5f, 0.5f);
        btnRt.sizeDelta = new Vector2(420, 120);
        btnRt.anchoredPosition = new Vector2(0, -140);

        var btnTextGo = new GameObject("Text");
        btnTextGo.transform.SetParent(btnGo.transform, false);
        var btnText = btnTextGo.AddComponent<Text>();
        btnText.text = "RESTART";
        btnText.font = uiFont;
        btnText.fontSize = 60;
        btnText.color = Color.black;
        btnText.alignment = TextAnchor.MiddleCenter;
        var btnTextRt = btnTextGo.GetComponent<RectTransform>();
        btnTextRt.anchorMin = Vector2.zero;
        btnTextRt.anchorMax = Vector2.one;
        btnTextRt.offsetMin = Vector2.zero;
        btnTextRt.offsetMax = Vector2.zero;

        btn.onClick.AddListener(OnRestartClicked);

        gameOverPanel.SetActive(false);

        if (Object.FindAnyObjectByType<EventSystem>() == null)
        {
            var esGo = new GameObject("EventSystem");
            esGo.AddComponent<EventSystem>();
            esGo.AddComponent<InputSystemUIInputModule>();
        }
    }

    Text MakeText(string name, Transform parent, Vector2 anchorMin, Vector2 anchorMax,
                  Vector2 anchoredPos, Vector2 size, TextAnchor align, int fontSize, string initial)
    {
        var go = new GameObject(name);
        go.transform.SetParent(parent, false);
        var t = go.AddComponent<Text>();
        t.text = initial;
        t.font = uiFont;
        t.fontSize = fontSize;
        t.color = Color.white;
        t.alignment = align;
        t.horizontalOverflow = HorizontalWrapMode.Overflow;
        t.verticalOverflow = VerticalWrapMode.Overflow;
        var rt = go.GetComponent<RectTransform>();
        rt.anchorMin = anchorMin;
        rt.anchorMax = anchorMax;
        rt.pivot = new Vector2(0.5f, 0.5f);
        rt.sizeDelta = size;
        rt.anchoredPosition = anchoredPos;
        return t;
    }

    void UpdateHp(int hp)
    {
        if (hpText != null) hpText.text = $"HP: {hp}";
    }

    void UpdateScore(int s)
    {
        if (scoreText != null) scoreText.text = $"Score: {s}";
    }

    void ShowGameOver()
    {
        if (gameOverPanel != null) gameOverPanel.SetActive(true);
        if (gameOverText != null && GameManager.Instance != null)
        {
            gameOverText.text = $"GAME OVER\nScore: {GameManager.Instance.score}";
        }
    }

    void OnRestartClicked()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.RestartGame();
        }
    }
}
