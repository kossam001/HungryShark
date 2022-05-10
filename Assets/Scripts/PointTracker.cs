using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PointTracker : MonoBehaviour
{
    public int score;
    public TMP_Text scoreUI;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        PointValue pointValue = collision.GetComponent<PointValue>();

        if (pointValue)
        {
            score += pointValue.GetPoints();
            scoreUI.text = score.ToString();

            LevelManager.Instance.CheckObjective(score);
        }
    }

    public void ResetScore()
    {
        score = 0;
        scoreUI.text = score.ToString();
    }    
}
