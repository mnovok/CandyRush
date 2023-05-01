using Packages.Rider.Editor.UnitTesting;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour
{
    public static int numberOfCoins;
    public TextMeshProUGUI numberOfCoinsText;
    public static int currentHP = 100;
    public Slider healthBar;
    public static bool gameOver;
    public GameObject gameOverPanel;
    public GameObject pausePanel;


    void Start()
    {
        numberOfCoins = 0;
        gameOver = false;
    }


    void Update()
    {
        numberOfCoinsText.text = "CANDIES: " + numberOfCoins;

        healthBar.value = currentHP;

        if(Input.GetKeyDown("escape"))
        {

            pausePanel.SetActive(true);
            Time.timeScale = 0f;
        }

        if (currentHP <= 0)
        {
            gameOver = true;
            gameOverPanel.SetActive(true);
            currentHP = 100;
        }
    }
}
