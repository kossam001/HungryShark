using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointValue : MonoBehaviour
{
    [SerializeField] private int points;

    // Want to add the additional functionality of despawning the object when the points are retrieved
    public int GetPoints()
    {
        AIManager.Instance.DespawnFish(gameObject);

        return points;
    }
}
