using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum LevelState
{
    ReadyLevel,
    Play,
    GoalReached,
    GameOver
}

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance { get { return instance; } }

    public float readyDelay;

    [SerializeField] private LevelState currentState = LevelState.ReadyLevel;
    private static LevelManager instance;

    // Ready state parameters
    private Coroutine readyDelayCoroutine;
    [SerializeField] private RectTransform readyDisplayPanel;

    private void Awake()
    {
        if (instance != null && instance != this)
            Destroy(this.gameObject);
        else
            instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        readyDisplayPanel.gameObject.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        switch (currentState)
        {
            case LevelState.ReadyLevel:
                UpdateReadyState();

                break;

            case LevelState.Play:
                break;
        }
    }

    public void UpdateReadyState()
    {
        if (readyDelayCoroutine == null)
            readyDelayCoroutine = StartCoroutine(ReadyDelayCountdown());
    }

    public IEnumerator ReadyDelayCountdown()
    {
        yield return new WaitForSeconds(readyDelay);

        currentState = LevelState.Play;
        readyDelayCoroutine = null;
        readyDisplayPanel.gameObject.SetActive(false);
    }

    public LevelState GetLevelState()
    {
        return currentState;
    }
}
