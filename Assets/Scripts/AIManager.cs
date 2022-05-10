using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIManager : MonoBehaviour
{
    public static AIManager Instance { get { return instance; } }

    [Header("Character Settings")]
    public GameObject fishTemplate;
    public int numFishToSpawn;
    public float spawnRate;

    [Header("Scene Settings")]
    public float spawnBoundsHeight;
    public float spawnBoundsWidth;

    [HideInInspector] public Queue<GameObject> fishPool = new Queue<GameObject>();

    private static AIManager instance;
    private float spawnTimer;

    private void Awake()
    {
        if (instance != null && instance != this)
            Destroy(this.gameObject);
        else
            instance = this;
    }

    private void Start()
    {
        // Creating an GameObject pool 
        for (int i = 0; i < numFishToSpawn; i++)
        {
            GameObject fish = Instantiate(fishTemplate);
            fish.SetActive(false);

            fishPool.Enqueue(fish);
        }
    }

    private void Update()
    {
        spawnTimer -= Time.deltaTime;

        if (spawnTimer < 0)
        {
            // Making the spawn a little random 
            spawnTimer = Random.Range(0, spawnRate);

            GameObject fish = fishPool.Dequeue();
            fish.SetActive(true);

            // We want the fish to spawn off screen, so that would be outside the bounds width
            fish.transform.position = new Vector3(spawnBoundsWidth, Random.Range(-spawnBoundsHeight, spawnBoundsHeight));
        }
    }
}
