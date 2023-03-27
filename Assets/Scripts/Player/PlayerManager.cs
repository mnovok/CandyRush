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

    void Start()
    {
        numberOfCoins = 0;
        gameOver = false;
    }


    void Update()
    {
        numberOfCoinsText.text = "CANDIES: " + numberOfCoins;

        healthBar.value = currentHP;

        if(currentHP <= 0)
        {
            gameOver = true;
        }
    }
}
