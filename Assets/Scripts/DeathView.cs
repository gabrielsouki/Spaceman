using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeathView : MonoBehaviour
{
    public Text coinsText, scoreText;

    public void SetDeathViewValues(float score)
    {
        SetCoinsValue();
        SetScoreValue(score);
    }

    public void ResetDeathViewValues()
    {
        ResetCoinsValue();
        ResetScoreValue();
    }

    public void SetCoinsValue()
    {
        coinsText.text = GameManager.sharedInstance.collectedCoins.ToString();
    }

    public void SetScoreValue(float score)
    {
        scoreText.text = score.ToString("f1");
    }

    public void ResetCoinsValue()
    {
        coinsText.text = "0";
    }

    public void ResetScoreValue()
    {
        scoreText.text = "0";
    }
}
