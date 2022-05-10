using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointTracker : MonoBehaviour
{
    public int score;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        PointValue pointValue = collision.GetComponent<PointValue>();

        if (pointValue)
        {
            score += pointValue.GetPoints();
        }
    }
}
