using Packages.Rider.Editor.UnitTesting;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour
{
    private float lastY;
    public static int numberOfCoins;
    public TextMeshProUGUI numberOfCoinsText;
    public static int currentHP = 100;
    public Slider healthBar;
    public static bool gameOver;
    public GameObject gameOverPanel;
    public float fallingThreshold = -1;

    void Start()
    {
        lastY = transform.position.y;
        numberOfCoins = 0;
        gameOver = false;
    }


    void Update()
    {
        numberOfCoinsText.text = "CANDIES: " + numberOfCoins;

        healthBar.value = currentHP;

        float distancePerSecondSinceLastFrame = (transform.position.y - lastY) * Time.deltaTime;
        lastY = transform.position.y;  //set for next frame
        if (distancePerSecondSinceLastFrame < fallingThreshold)
        {
            gameOver = true;
            gameOverPanel.SetActive(true);
            currentHP = 100;
        }

        if (currentHP <= 0)
        {
            gameOver = true;
            gameOverPanel.SetActive(true);
            currentHP = 100;
        }
    }
}
