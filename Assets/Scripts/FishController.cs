using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishController : MonoBehaviour
{
    public float speed;
    public Vector3 direction;
    public FishType fishType;

    private void Update()
    {
        transform.position += direction * speed * Time.deltaTime;
    }

    private void OnBecameInvisible()
    {
        AIManager.Instance.DespawnFish(gameObject);
    }
}
