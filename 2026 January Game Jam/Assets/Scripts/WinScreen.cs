using UnityEngine;
using System.Collections;
public class WinScreen : MonoBehaviour
{
    public static WinScreen instance;

    public CanvasGroup winPanel;
    public CanvasGroup gameOverPanel;
    public float fadeDuration = 1.5f;

    void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    // ---------- WIN ----------
    public void ShowWinScreen()
    {
        StartCoroutine(FadeIn(winPanel));
    }

    // ---------- GAME OVER ----------
    public void ShowGameOverScreen()
    {
        StartCoroutine(FadeIn(gameOverPanel));
    }

    IEnumerator FadeIn(CanvasGroup panel)
    {
        float t = 0f;
        panel.alpha = 0f;
        panel.gameObject.SetActive(true);

        while (t < fadeDuration)
        {
            t += Time.deltaTime;
            panel.alpha = Mathf.Lerp(0, 1, t / fadeDuration);
            yield return null;
        }

        panel.alpha = 1f;
        Time.timeScale = 0f; // freeze game
    }
}