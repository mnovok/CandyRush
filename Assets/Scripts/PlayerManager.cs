using Packages.Rider.Editor.UnitTesting;
using TMPro;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static int numberOfCoins;
    public TextMeshProUGUI numberOfCoinsText;

    void Start()
    {
        numberOfCoins = 0;
    }


    void Update()
    {
        numberOfCoinsText.text = "COINS: " + numberOfCoins;
    }
}
