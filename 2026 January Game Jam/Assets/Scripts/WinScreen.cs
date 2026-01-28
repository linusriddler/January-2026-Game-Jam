using UnityEngine;
using System.Collections;
public class WinScreen : MonoBehaviour
{
    public static WinScreen instance;

    public CanvasGroup winPanel;
    public float fadeDuration = 1.5f;

    void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    public void ShowWinScreen()
    {
        StartCoroutine(FadeIn());
    }

    IEnumerator FadeIn()
    {
        float t = 0f;

        while (t < fadeDuration)
        {
            t += Time.deltaTime;
            winPanel.alpha = Mathf.Lerp(0, 1, t / fadeDuration);
            yield return null;
        }

        winPanel.alpha = 1;
        Time.timeScale = 0f; // freeze game
    }
}