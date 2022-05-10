using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum FishType
{
    Good,
    Bad
}

public class AIManager : MonoBehaviour
{
    public static AIManager Instance { get { return instance; } }

    [Header("Good Fish")]
    [Header("Character Settings")]
    public GameObject goodFishTemplate;
    public int numGoodFishToSpawn;
    public float goodFishSpawnRate;

    [Space]

    [Header("Bad Fish")]
    public GameObject badFishTemplate;
    public int numBadFishToSpawn;
    public float badFishSpawnRate;

    [Header("Scene Settings")]
    public float spawnBoundsHeight;
    public float spawnBoundsWidth;

    // Using two different pools because I want to have the option to control the frequency of each fish type
    [HideInInspector] public Queue<GameObject> goodFishPool = new Queue<GameObject>();
    [HideInInspector] public Queue<GameObject> badFishPool = new Queue<GameObject>();

    private static AIManager instance;
    private float goodFishSpawnTimer;
    private float badFishSpawnTimer;

    private void Awake()
    {
        if (instance != null && instance != this)
            Destroy(this.gameObject);
        else
            instance = this;
    }

    private void Start()
    {
        InitializeFishPool(FishType.Good, goodFishTemplate, numGoodFishToSpawn);
        InitializeFishPool(FishType.Bad, badFishTemplate, numBadFishToSpawn);
    }

    private void InitializeFishPool(FishType fishType, GameObject fishTemplate, int numToSpawn)
    {
        // Creating an GameObject pool 
        for (int i = 0; i < numToSpawn; i++)
        {
            GameObject fish = Instantiate(fishTemplate);

            FishController fishController = fish.GetComponent<FishController>();

            if (!fishController)
            {
                Debug.LogError("Not a fish");
                return;
            }

            fishController.fishType = fishType;

            DespawnFish(fish);
        }
    }

    private void Update()
    {
        CheckSpawnTimer(goodFishSpawnTimer, FishType.Good);
        CheckSpawnTimer(badFishSpawnTimer, FishType.Bad);
    }

    private void CheckSpawnTimer(float spawnTimer, FishType fishType)
    {
        spawnTimer -= Time.deltaTime;

        if (spawnTimer < 0)
        {
            // Making the spawn a little random 
            spawnTimer = Random.Range(0, goodFishSpawnRate);

            GameObject fish = SpawnFish(fishType);

            // Check if a fish was available in the pool
            if (!fish) return;

            // We want the fish to spawn off screen, so that would be outside the bounds width
            fish.transform.position = new Vector3(spawnBoundsWidth, Random.Range(-spawnBoundsHeight, spawnBoundsHeight));
        }
    }

    public GameObject SpawnFish(FishType fishType)
    {
        GameObject fish = null;

        switch (fishType)
        {
            case FishType.Good:
                if (goodFishPool.Count > 0)
                    fish = goodFishPool.Dequeue();

                else return null;

                break;

            case FishType.Bad:
                if (badFishPool.Count > 0)
                    fish = badFishPool.Dequeue();

                else
                    return null;

                break;
        }

        fish.SetActive(true);

        return fish;
    }

    public void DespawnFish(GameObject fish)
    {
        FishController fishController = fish.GetComponent<FishController>();

        if (!fishController)
        {
            Debug.LogError("Not a fish");

            return;
        }

        fish.SetActive(false);
        FishType fishType = fishController.fishType;

        switch (fishType)
        {
            case FishType.Good:
                goodFishPool.Enqueue(fish);

                break;

            case FishType.Bad:
                badFishPool.Enqueue(fish);

                break;
        }
    }
}
