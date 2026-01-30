using UnityEngine;
using System.Collections;

public class WinScreen : MonoBehaviour
{
    public static WinScreen instance;

    public CanvasGroup winPanel;
    public CanvasGroup gameOverPanel;
    public float fadeDuration = 1.5f;
    public GameObject DeathCamera;

    // 🔊 AUDIO
    public AudioSource winAudioSource;
    public AudioSource loseAudioSource;

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
        if (winAudioSource != null)
            winAudioSource.Play();

        StartCoroutine(FadeIn(winPanel));
        Dead = true;
    }

    // ---------- GAME OVER ----------
    public void ShowGameOverScreen()
    {
        if (loseAudioSource != null)
            loseAudioSource.Play();

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
        if (Dead && Input.GetKeyDown(KeyCode.Space))
        {
            Time.timeScale = 1f;
            UnityEngine.SceneManagement.SceneManager.LoadScene(
                UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex);
        }
    }
}