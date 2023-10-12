using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public GameObject Player;

    public GameType gameType = GameType.Normal;

    public float startDelay = 10f;

    public static int currentCoins = 0;
    public static int totalCoins = 0;

    static int totalBrokenItem = 0;
    static int totalBreakableItem = 0;

    public Transform spawnPoint;
    private bool haveStartup = false;
    public static bool isPaused, isFinished = false, isTimeRunning = true;

    [SerializeField] float startTime = 0f;
    float currentTime = 0f;

    public InfiniteBG bgtest;
    public Color warningColor;

    public string CurrentTimeProp { get => DevTools.TranslateSecondsToString(currentTime); }

    private void Awake()
    {
        instance = this;
        isPaused = false;
        isFinished = false;
        isTimeRunning = true;
        ResetTimer();
    }

    private void Start() { }

    private void Update()
    {
        if (!haveStartup) Startup();

        CheckGameOver();
    }

    public void FixedUpdate()
    {
        if (isTimeRunning)
            switch (gameType)
            {
                case GameType.Normal:
                    TimerCounter();
                    break;
                case GameType.Timeout:
                    TimeOutCounter();
                    break;
            }
    }

    void Startup()
    {
        StartCoroutine(StartUpDelay(startDelay));

        haveStartup = true;
    }

    #region Items

    public void AddCoin()
    {
        currentCoins++;
    }

    public void AddCoins(int value)
    {
        currentCoins += value;
    }

    public void AddTotalCoin()
    {
        totalCoins++;
    }

    public void AddBreakableItem()
    {
        totalBreakableItem++;
    }

    public void RemoveBreakableItem()
    {
        totalBreakableItem--;
    }

    public void AddBrokenItem()
    {
        totalBrokenItem++;
    }

    public void RemoveBrokenItem()
    {
        totalBreakableItem--;
    }

    #endregion

    #region GameState
    public void CheckGameOver()
    {
        if (currentCoins >= totalCoins)
        {
            CustomUIManager.instance.GameOver();
        }
    }

    //void ResetCoins() { currentCoins = 0; }
    public static void ResetGameValues()
    {
        currentCoins = 0;
        totalCoins = 0;
        totalBrokenItem = 0;
        totalBreakableItem = 0;
        isFinished = false;
        isPaused = false;
    }

    public void Pause(bool setPause)
    {
        isPaused = setPause;
        isTimeRunning = !setPause;

        if (isPaused)
        {
            PlayerMovement.instance.StopMovement();
        }
        else
        {
            PlayerMovement.instance.RestoreMovement();
        }
    }

    public void Pause()
    {
        Pause(!isPaused);
    }

    void TimerCounter()
    {
        currentTime += Time.fixedUnscaledDeltaTime;
    }
    void TimeOutCounter()
    {
        currentTime -= Time.fixedUnscaledDeltaTime;
        CheckTimerState();
    }

    void CheckTimerState()
    {
        if (currentTime < 5)
        {
            bgtest.MaterialColorUpdate(warningColor);
        }

        if (currentTime <= 0)
        {
            print("end game");
        }
    }

    public void ResetTimer()
    {
        currentTime = startTime;
    }

    #endregion

    IEnumerator StartUpDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        Player.SetActive(true);
        Player.transform.position = spawnPoint.position;
        MainCamera.instance.focus = Player.transform;
    }

    public enum GameType
    {
        Normal, Timeout
    }

}

