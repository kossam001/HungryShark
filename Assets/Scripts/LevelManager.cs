using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public enum LevelState
{
    ReadyLevel,
    Play,
    Clear,
    GameOver
}

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance { get { return instance; } }

    public GameObject player;
    public float stateTransitionDelay;

    [SerializeField] private LevelState currentState = LevelState.ReadyLevel;
    private static LevelManager instance;

    [SerializeField] private int scoreGoal = 100;
    [SerializeField] private TMP_Text goalDisplay;

    // Ready state parameters
    private Coroutine readyDelayCoroutine;
    [SerializeField] private RectTransform readyDisplayPanel;

    // Clear state parameters
    private Coroutine clearDelayCoroutine;
    [SerializeField] private RectTransform clearDisplayPanel;

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

        clearDisplayPanel.gameObject.SetActive(false);

        goalDisplay.text = scoreGoal.ToString();
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

            case LevelState.Clear:
                UpdateClearState();

                break;
        }
    }

    public void UpdateReadyState()
    {
        if (readyDelayCoroutine == null)
            readyDelayCoroutine = StartCoroutine(ReadyDelayCountdown());
    }

    public void UpdateClearState()
    {
        if (clearDelayCoroutine == null)
            clearDelayCoroutine = StartCoroutine(ClearStateCountdown());
    }

    public IEnumerator ReadyDelayCountdown()
    {
        yield return new WaitForSeconds(stateTransitionDelay);

        currentState = LevelState.Play;
        readyDelayCoroutine = null;
        readyDisplayPanel.gameObject.SetActive(false);

        player.GetComponent<PointTracker>().ResetScore();
    }

    public IEnumerator ClearStateCountdown()
    {
        clearDisplayPanel.gameObject.SetActive(true);

        yield return new WaitForSeconds(stateTransitionDelay);

        currentState = LevelState.Play;
        clearDelayCoroutine = null;
        clearDisplayPanel.gameObject.SetActive(false);

        scoreGoal += scoreGoal;
        goalDisplay.text = scoreGoal.ToString();

        player.GetComponent<PointTracker>().ResetScore();
    }

    public LevelState GetLevelState()
    {
        return currentState;
    }

    public void CheckObjective(int score)
    {
        if (score >= scoreGoal)
        {
            currentState = LevelState.Clear;
        }
    }
}
