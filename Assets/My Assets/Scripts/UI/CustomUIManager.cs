using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CustomUIManager : MonoBehaviour
{
    public static CustomUIManager instance;
    [SerializeField] GameObject GameOverUI, PauseUI;

    public List<GameObject> hideOnGameOver;

    //UI
    public TMP_Text countUILabel;
    public TMP_Text timeUILabel;

    public TMP_Text gameOverTimeLabel;

    private void Awake()
    {
        instance = this;
    }

    void Update()
    {
        UIInput();
        UpdateUI();
    }

    void UpdateUI()
    {
        string kiwiCountText = $"KIWI: {GameManager.currentCoins}/{GameManager.totalCoins}";
        countUILabel.text = kiwiCountText;

        timeUILabel.text = GameManager.instance.CurrentTimeProp;
    }

    void UIInput()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            GameManager.instance.Pause();
        }

        PauseUI.SetActive(GameManager.isPaused);
    }

    public void GameOver()
    {
        if (!GameManager.isFinished)
        {

            foreach (var go in hideOnGameOver)
            {
                go.SetActive(false);
            }

            GameManager.isTimeRunning = false;
            GameManager.isFinished = true;
            gameOverTimeLabel.text = GameManager.instance.CurrentTimeProp;
            GameOverUI.gameObject.SetActive(GameManager.isFinished);
            PlayerMovement.instance.StopMovement();
        }

        MainCamera.instance.GoToPoint(Vector3.zero);
    }
}