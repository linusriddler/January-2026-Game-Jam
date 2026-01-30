using UnityEngine;
using System.Collections;
using System.Runtime.CompilerServices;
public class WinScreen : MonoBehaviour
{
    public static WinScreen instance;

    public CanvasGroup winPanel;
    public CanvasGroup gameOverPanel;
    public float fadeDuration = 1.5f;
    public GameObject DeathCamera;
    private bool Dead;
    private void Start()
    {
        DeathCamera.SetActive(false);
        Dead = false;
    }
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
        Dead = true;
    }

    // ---------- GAME OVER ----------
    public void ShowGameOverScreen()
    {
        StartCoroutine(FadeIn(gameOverPanel));
        Dead = true;
    }

    IEnumerator FadeIn(CanvasGroup panel)
    {
        float t = 0f;
        panel.alpha = 0f;
        panel.gameObject.SetActive(true);
        DeathCamera.SetActive(true);

        while (t < fadeDuration)
        {
            t += Time.deltaTime;
            panel.alpha = Mathf.Lerp(0, 1, t / fadeDuration);
            yield return null;
        }

        panel.alpha = 1f;
        Time.timeScale = 0f; // freeze game
    }
    private void Update()
    {
        if ((Dead) && Input.GetKeyDown(KeyCode.Space)) 
        {
            Time.timeScale = 1f; // just in case the game was frozen
            UnityEngine.SceneManagement.SceneManager.LoadScene(
                UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex);
        }
    }
}