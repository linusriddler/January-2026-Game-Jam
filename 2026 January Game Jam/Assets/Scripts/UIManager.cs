using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    public TextMeshProUGUI collectibleText;
    private int collectibleCount = 0;

    public int beansToWin = 5; // 👈 win condition

    void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    void Start()
    {
        UpdateText();
    }

    public void AddCollectible()
    {
        collectibleCount++;
        UpdateText();

        // 🏆 CHECK FOR WIN
        if (collectibleCount >= beansToWin)
        {
            WinScreen.instance.ShowWinScreen();
        }
    }

    void UpdateText()
    {
        collectibleText.text = "Beans: " + collectibleCount;
    }
}
