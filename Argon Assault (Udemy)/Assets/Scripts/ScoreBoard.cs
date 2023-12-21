using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreBoard : MonoBehaviour
{
    int score = 0;
    TMP_Text scoreText;
    private void Start()
    {
        scoreText = GetComponent<TMP_Text>();
        scoreText.text = "0";
    }

    public void IncreaseScore(int amountToIncrease)
    {
        score += amountToIncrease;
        scoreText.text = score.ToString();
        Debug.Log($" Score : {score}");
    }
}
